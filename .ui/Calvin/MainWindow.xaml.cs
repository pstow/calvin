// Carbon Capture - Calvin
// MainWindow.xaml.cs
// Rev 1.00 - July 22, 2023

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Calvin.ConfigManager;
using Calvin.AdminManager;

namespace Calvin
{
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;
        public static DispatcherTimer AdsTimer;
        public Button btn;
        private short iLogCount;
        private bool bAdsTimer;
        private bool bReady;

        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;
            Config config = new Config();
            _ = config.XConfig_Load();
            if (!ADS.TcAdsServices() && !ADS.bRunWOAds)
            {
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                StartTimer();
            }
        }

        private void StartTimer()
        {
            LoadConfiguration();
            bAdsTimer = true;
            AdsTimer = new DispatcherTimer();
            AdsTimer.Tick += new EventHandler(AdsTimer_Tick);
            AdsTimer.Interval = TimeSpan.FromMilliseconds(App.iCommCheckTime);
            AdsTimer.IsEnabled = true;
        }

        private void AdsTimer_Tick(object sender, EventArgs e)
        {
            if (!ADS.bRunWOAds)
            {
                _ = ADS.AdsCommDown();
                if (ADS.bAdsOffline)
                {
                    if (App.WPFMessageBoxYesNo("No communication with TwinCAT program - Retry", App.bgRed))
                    {
                        lblError.Content = "Restarting ADS - please wait";
                        ADS.tcClient.Dispose();
                        ADS.bTCStarted = false;
                        ADS.bCartridgeSaved = true;
                        _ = !ADS.TcAdsServices();
                    }
                    else
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }
        }

        private void btnAlarms_Click(object sender, RoutedEventArgs e)
        {
            ADS.SetAdsValue(ADS.GVTtgb + "Alarm_Reset", true);
            lblError.Content = "";
            ADS.bAlarmReset = true;
        }

        #region Direct ADS Control

        public void UpdateValues()
        {
            bReady = !ADS.bInProcess && !ADS.bAdsDown && ADS.bLoggedOn;                                                     // 07/20/23 PS
            if (ADS.bActiveAlarm)
            {
                ADS.bActiveAlarm = false;
                Logger.LogMachineError(ADS.strActiveAlarm);                                                                 // 6/22/23 - Moved to log/email alarm before alarm acknowledge
                if (App.WPFMessageBox_OK(ADS.strActiveAlarm, App.bgRed))
                {
                    ADS.SetAdsValue(ADS.GVTtgb + "Active_Alarm", false);
                }
            }

            if (ADS.bAlarmReset)
            {
                lblError.Content = "";
                ADS.bAlarmReset = false;
            }
            else
            {
                lblError.Foreground = ADS.bErrorOn || ADS.bAdsDown ? Brushes.Red : Brushes.Green;
                lblError.Content = ADS.bAdsDown ? "TwinCAT offline" : ADS.strAlarm;
            }

            string strLocked = ADS.bLocked ? " (Locked)" : "";                                                              // 07/22/23 PS
            lblUser.Content = "User: " + ADS.strCurrentUser + strLocked;                                                    // 07/22/23 PS

            UpdateManualValues();
            UpdateAutoValues();
            UpdateSetupValues();
            UpdatePoints();
            LogValues();
            GraphTimer();
        }

        #endregion Direct ADS Control

        private void btnLogOnOff_Click(object sender, RoutedEventArgs e)                                                    // 07/22/23 PS - Added Log On/Off function
        {
            Window winLogOn = new LogOnWindow();
            _ = winLogOn.ShowDialog();
        }

        private void LoadConfiguration()
        {
            ColorList = new List<Brush>()
            {
                Brushes.Red,
                Brushes.Green,
                Brushes.Orange,
                Brushes.Blue,
                Brushes.Gray,
                Brushes.HotPink,
                Brushes.Black,
                Brushes.BlueViolet,
                Brushes.Brown,
                Brushes.Coral,
                Brushes.Crimson,
                Brushes.Yellow
            };
            DataContext = this;

            bConfigOn = true;
            LoadConfiguration_Manual();
            LoadConfiguration_Auto();
            LoadConfiguration_Graph();
            LoadPointValues();
            LoadPointsConfig();
            LoadSetupConfiguration();
            bConfigOn = false;

            scPFactor = ADS.sPFactor;
            scIFactor = ADS.sIFactor;
            scIRange = ADS.sIRange;

            if (ADS.bAdsDown)
            {
                UpdateValues();
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        { _ = labFocus.Focus(); }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ADS.bTCStarted)
            {
                ADS.SetAdsValue(ADS.GVTtgb + "ADS_Active", false);
                ADS.TcAdsClose();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (bAdsTimer)
            {
                AdsTimer.Stop();
                AdsTimer.Tick -= AdsTimer_Tick;
                bAdsTimer = false;
                ADS.bTCStarted = false;
            }
        }

    }
}
