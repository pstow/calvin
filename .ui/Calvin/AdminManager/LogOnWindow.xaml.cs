// Carbon Capture - Calvin
// LogOnWindow.xaml.cs
// Rev 1.00 - July 22, 2023

using System;
using System.Windows;
using System.Windows.Media;

namespace Calvin.AdminManager
{
    public partial class LogOnWindow : Window
    {
        private bool bLocked;

        public LogOnWindow()
        {
            InitializeComponent();
            lblCurrent.Content = ADS.strCurrentUser;
            lblDate.Content = ADS.strLogOnTime;
            bLocked = ADS.bLocked;
            App.ButtonEnable(ref btnLogOff, ADS.bLoggedOn);
            App.ButtonEnable(ref btnLogOn, !ADS.bLoggedOn);
            App.ButtonEnable(ref btnLock, ADS.bLoggedOn);
            SetLockButton();
        }

        private void btnLock_Click(object sender, RoutedEventArgs e)
        {
            bLocked = !bLocked;
            ADS.SetAdsValue(ADS.GVTtgb + "Hmi_Locked", bLocked);
            SetLockButton();
        }

        private void btnLogOn_Click(object sender, RoutedEventArgs e)
        {
            ADS.SetAdsString(ADS.GVTtgs + "Current_User", ADS.strUser);
            ADS.SetAdsValue(ADS.GVTtgi + "Current_User_Level", ADS.iLevel);
            ADS.SetAdsString(ADS.GVTtgs + "Hmi_Log_Date_Time", App.DateAndTime());
            Close();
        }

        private void btnLogOff_Click(object sender, RoutedEventArgs e)
        {
            bLocked = false;
            ADS.SetAdsString(ADS.GVTtgs + "Current_User", "");
            ADS.SetAdsValue(ADS.GVTtgi + "Current_User_Level", (short)0);
            ADS.SetAdsString(ADS.GVTtgs + "Hmi_Log_Date_Time", "");
            ADS.SetAdsValue(ADS.GVTtgb + "Hmi_Locked", bLocked);
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetLockButton()
        {
            btnLock.Content = bLocked ? "Unlock" : "Lock";
            btnLock.Background = bLocked ? Brushes.Orange : Brushes.LightGreen;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            _ = lblFocus.Focus();
        }

    }
}
