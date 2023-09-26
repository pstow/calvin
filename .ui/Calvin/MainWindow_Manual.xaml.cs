// Carbon Capture - Calvin
// MainWindow_Manual.xaml.cs
// Rev 1.00 - September 20, 2023

using Calvin.ConfigManager;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Calvin
{
    public partial class MainWindow
    {
        #region Variables

        private float sAOut000;
        private float sAOut007;
        private float sAOut008;
        private float sAOut009;
        private float sAOut010;
        private readonly short iMax = 32767;
        private readonly string strSetpoint = " Setpoint";
        private readonly string strManualOutput = " Manual Out";
        private readonly string strOutput = " Output";
        private readonly string strLoggingOn = "Logging On";
        private readonly string strLoggingOff = "Logging Off";
        private readonly string strDrainsOn = "Auto Drains On";
        private readonly string strDrainsOff = "Auto Drains Off";
        private readonly string strAuto = "Auto";
        private readonly string strManual = "Manual";
        private string strText;
        private bool bButtonsEnabled;
        private bool bDrainsOn;

        #endregion Variables

        public void UpdateManualValues()
        {
            lblStatus_copy.Content = ADS.strStatusLabel;
            lblStatus_copy.Foreground = App.LabelStatus(ADS.iAlarm > 0, bReady);

            lblHT_1.Content = Units.UnitDisplay2(ADS.sTemp011, ADS.Temp011);
            //lblHT_1_Out.Content = ADS.sAOut001_Out.ToString("F0");
            lblHT_2.Content = Units.UnitDisplay2(ADS.sTemp012, ADS.Temp012);
            //lblHT_2_Out.Content = ADS.sAOut002_Out.ToString("F0");
            lblHT_3.Content = Units.UnitDisplay2(ADS.sTemp013, ADS.Temp013);
            //lblHT_3_Out.Content = ADS.sAOut003_Out.ToString("F0");
            lblHT_4.Content = Units.UnitDisplay2(ADS.sTemp014, ADS.Temp014);
            //lblHT_4_Out.Content = ADS.sAOut004_Out.ToString("F0");
            lblHT_5.Content = Units.UnitDisplay2(ADS.sTemp015, ADS.Temp015);
            //lblHT_5_Out.Content = ADS.sAOut005_Out.ToString("F0");
            lblHT_6.Content = Units.UnitDisplay2(ADS.sTemp016, ADS.Temp016);
            //lblHT_6_Out.Content = ADS.sAOut006_Out.ToString("F0");
            lblHT_7.Content = Units.UnitDisplay2(ADS.sTemp017, ADS.Temp017);
            lblHT_8.Content = Units.UnitDisplay2(ADS.sTemp018, ADS.Temp018);
            lblHT_9.Content = Units.UnitDisplay2(ADS.sTemp019, ADS.Temp019);
            lblHT_10.Content = Units.UnitDisplay2(ADS.sTemp020, ADS.Temp020);
            lblHT_11.Content = Units.UnitDisplay2(ADS.sTemp021, ADS.Temp021);
            lblHT_12.Content = Units.UnitDisplay2(ADS.sTemp022, ADS.Temp022);

            tbkHT_1_Out.Foreground = App.ForegroundOnOff(ADS.bAOut001_On);
            tbkHT_2_Out.Foreground = App.ForegroundOnOff(ADS.bAOut002_On);
            tbkHT_3_Out.Foreground = App.ForegroundOnOff(ADS.bAOut003_On);
            tbkHT_4_Out.Foreground = App.ForegroundOnOff(ADS.bAOut004_On);
            tbkHT_5_Out.Foreground = App.ForegroundOnOff(ADS.bAOut005_On);
            tbkHT_6_Out.Foreground = App.ForegroundOnOff(ADS.bAOut006_On);
            tbkHT_1_Manual_Output.Foreground = App.ForegroundOnOff(ADS.bManualHeat);
            tbkHT_2_Manual_Output.Foreground = App.ForegroundOnOff(ADS.bManualHeat);
            tbkHT_3_Manual_Output.Foreground = App.ForegroundOnOff(ADS.bManualHeat);
            tbkHT_4_Manual_Output.Foreground = App.ForegroundOnOff(ADS.bManualHeat);
            tbkHT_5_Manual_Output.Foreground = App.ForegroundOnOff(ADS.bManualHeat);
            tbkHT_6_Manual_Output.Foreground = App.ForegroundOnOff(ADS.bManualHeat);

            tbkAOut008.Foreground = App.ValveStatus(ADS.bDI000_Open, ADS.bDI000_Closed);
            tbkAOut009.Foreground = App.ValveStatus(ADS.bDI001_Open, ADS.bDI001_Closed);

            lblAIn000.Content = Units.UnitDisplay2(ADS.sAIn000, ADS.AIn000);
            lblAIn001.Content = Units.UnitDisplay2(ADS.sAIn001, ADS.AIn001);
            lblAIn002.Content = Units.UnitDisplay2(ADS.sAIn002, ADS.AIn002);
            lblAIn003.Content = Units.UnitDisplay2(ADS.sAIn003, ADS.AIn003);
            lblAIn004.Content = Units.UnitDisplay2(ADS.sAIn004, ADS.AIn004);
            lblAIn005.Content = Units.UnitDisplay2(ADS.sAIn005, ADS.AIn005);
            lblAIn006.Content = Units.UnitDisplay2(ADS.sAIn006, ADS.AIn006);
            lblAIn007.Content = Units.UnitDisplay2(ADS.sAIn007, ADS.AIn007);
            lblAIn008.Content = Units.UnitDisplay2(ADS.sAIn008, ADS.AIn008);
            lblAIn009.Content = Units.UnitDisplay2(ADS.sAIn009, ADS.AIn009);
            lblAIn010.Content = Units.UnitDisplay2(ADS.sAIn010, ADS.AIn010);
            lblAIn011.Content = Units.UnitDisplay2(ADS.sAIn011, ADS.AIn011);
            lblAIn012.Content = Units.UnitDisplay2(ADS.sAIn012, ADS.AIn012);
            lblAIn013.Content = Units.UnitDisplay2(ADS.sAIn013, ADS.AIn013);
            lblAIn014.Content = Units.UnitDisplay2(ADS.sAIn014, ADS.AIn014);
            lblAIn015.Content = Units.UnitDisplay2(ADS.sAIn015, ADS.AIn015);
            lblAIn016.Content = Units.UnitDisplay2(ADS.sAIn016, ADS.AIn016);
            lblAIn017.Content = Units.UnitDisplay2(ADS.sAIn017, ADS.AIn017);
            lblAIn018.Content = Units.UnitDisplay2(ADS.sAIn018, ADS.AIn018);
            lblAIn019.Content = Units.UnitDisplay2(ADS.sAIn019, ADS.AIn019);
            lblAIn020.Content = Units.UnitDisplay2(ADS.sAIn020, ADS.AIn020);
            lblAIn021.Content = Units.UnitDisplay2(ADS.sAIn021, ADS.AIn021);
            lblAIn022.Content = Units.UnitDisplay2(ADS.sAIn022, ADS.AIn022);
            lblAIn023.Content = Units.UnitDisplay2(ADS.sAIn023, ADS.AIn023);
            lblAIn024.Content = Units.UnitDisplay2(ADS.sAIn024, ADS.AIn024);
            lblAIn025.Content = Units.UnitDisplay2(ADS.sAIn025, ADS.AIn025);
            lblAIn026.Content = Units.UnitDisplay2(ADS.sAIn026, ADS.AIn026);
            lblAIn027.Content = Units.UnitDisplay2(ADS.sAIn027, ADS.AIn027);
            lblAIn028.Content = Units.UnitDisplay2(ADS.sAIn028, ADS.AIn028);
            lblAIn040.Content = Units.UnitDisplay2(ADS.sAIn040, ADS.AIn040);
            lblAIn041.Content = Units.UnitDisplay2(ADS.sAIn041, ADS.AIn041);
            lblAIn042.Content = Units.UnitDisplay2(ADS.sAIn042, ADS.AIn042);
            lblAIn043.Content = Units.UnitDisplay2(ADS.sAIn043, ADS.AIn043);
            lblAIn044.Content = Units.UnitDisplay2(ADS.sAIn044, ADS.AIn044);
            lblAIn045.Content = Units.UnitDisplay2(ADS.sAIn045, ADS.AIn045);
            lblAIn046.Content = Units.UnitDisplay2(ADS.sAIn046, ADS.AIn046);
            lblAIn050.Content = Units.UnitDisplay2(ADS.sAIn050, ADS.AIn050);
            lblAIn051.Content = Units.UnitDisplay2(ADS.sAIn051, ADS.AIn051);
            lblAIn052.Content = Units.UnitDisplay2(ADS.sAIn052, ADS.AIn052);
            lblAIn053.Content = Units.UnitDisplay2(ADS.sAIn053, ADS.AIn053);
            lblAIn054.Content = Units.UnitDisplay2(ADS.sAIn054, ADS.AIn054);
            lblAIn055.Content = Units.UnitDisplay2(ADS.sAIn055, ADS.AIn055);

            lblPower1.Content = Units.UnitDisplay3(ADS.sAIn050 * ADS.sAIn053, 1, 21);
            lblPower2.Content = Units.UnitDisplay3(ADS.sAIn051 * ADS.sAIn054, 1, 21);
            lblPower3.Content = Units.UnitDisplay3(ADS.sAIn052 * ADS.sAIn055, 1, 21);

            lblTemp000.Content = Units.UnitDisplay2(ADS.sTemp000, ADS.Temp000);
            tbkTemp000.Foreground = App.TempError(ADS.bTemp000_Error);
            tbkTemp000.Visibility = App.ButtonVisibility(ADS.bTemp000_Error && ADS.bHalfSecPulse);
            lblTemp001.Content = Units.UnitDisplay2(ADS.sTemp001, ADS.Temp001);
            tbkTemp001.Foreground = App.TempError(ADS.bTemp001_Error);
            tbkTemp001.Visibility = App.ButtonVisibility(ADS.bTemp001_Error && ADS.bHalfSecPulse);
            lblTemp002.Content = Units.UnitDisplay2(ADS.sTemp002, ADS.Temp002);
            tbkTemp002.Foreground = App.TempError(ADS.bTemp002_Error);
            tbkTemp002.Visibility = App.ButtonVisibility(ADS.bTemp002_Error && ADS.bHalfSecPulse);
            lblTemp003.Content = Units.UnitDisplay2(ADS.sTemp003, ADS.Temp003);
            tbkTemp003.Foreground = App.TempError(ADS.bTemp003_Error);
            tbkTemp003.Visibility = App.ButtonVisibility(ADS.bTemp003_Error && ADS.bHalfSecPulse);
            lblTemp004.Content = Units.UnitDisplay2(ADS.sTemp004, ADS.Temp004);
            tbkTemp004.Foreground = App.TempError(ADS.bTemp004_Error);
            tbkTemp004.Visibility = App.ButtonVisibility(ADS.bTemp004_Error && ADS.bHalfSecPulse);
            lblTemp005.Content = Units.UnitDisplay2(ADS.sTemp005, ADS.Temp005);
            tbkTemp005.Foreground = App.TempError(ADS.bTemp005_Error);
            tbkTemp005.Visibility = App.ButtonVisibility(ADS.bTemp005_Error && ADS.bHalfSecPulse);
            lblTemp006.Content = Units.UnitDisplay2(ADS.sTemp006, ADS.Temp006);
            tbkTemp006.Foreground = App.TempError(ADS.bTemp006_Error);
            tbkTemp006.Visibility = App.ButtonVisibility(ADS.bTemp006_Error && ADS.bHalfSecPulse);
            lblTemp007.Content = Units.UnitDisplay2(ADS.sTemp007, ADS.Temp007);
            tbkTemp007.Foreground = App.TempError(ADS.bTemp007_Error);
            tbkTemp007.Visibility = App.ButtonVisibility(ADS.bTemp007_Error && ADS.bHalfSecPulse);
            lblTemp008.Content = Units.UnitDisplay2(ADS.sTemp008, ADS.Temp008);
            tbkTemp008.Foreground = App.TempError(ADS.bTemp008_Error);
            tbkTemp008.Visibility = App.ButtonVisibility(ADS.bTemp008_Error && ADS.bHalfSecPulse);
            lblTemp009.Content = Units.UnitDisplay2(ADS.sTemp009, ADS.Temp009);
            tbkTemp009.Foreground = App.TempError(ADS.bTemp009_Error);
            tbkTemp009.Visibility = App.ButtonVisibility(ADS.bTemp009_Error && ADS.bHalfSecPulse);
            lblTemp010.Content = Units.UnitDisplay2(ADS.sTemp010, ADS.Temp010);
            tbkTemp010.Foreground = App.TempError(ADS.bTemp010_Error);
            tbkTemp010.Visibility = App.ButtonVisibility(ADS.bTemp010_Error && ADS.bHalfSecPulse);
            tbkHT_1_SP.Foreground = App.ActiveState(ADS.bAOut001_On);
            tbkTemp011.Foreground = App.TempError(ADS.bTemp011_Error);
            tbkTemp011.Visibility = App.ButtonVisibility(ADS.bTemp011_Error && ADS.bHalfSecPulse);
            tbkHT_2_SP.Foreground = App.ActiveState(ADS.bAOut002_On);
            tbkTemp012.Foreground = App.TempError(ADS.bTemp012_Error);
            tbkTemp012.Visibility = App.ButtonVisibility(ADS.bTemp012_Error && ADS.bHalfSecPulse);
            tbkHT_3_SP.Foreground = App.ActiveState(ADS.bAOut003_On);
            tbkTemp013.Foreground = App.TempError(ADS.bTemp013_Error);
            tbkTemp013.Visibility = App.ButtonVisibility(ADS.bTemp013_Error && ADS.bHalfSecPulse);
            tbkHT_4_SP.Foreground = App.ActiveState(ADS.bAOut004_On);
            tbkTemp014.Foreground = App.TempError(ADS.bTemp014_Error);
            tbkTemp014.Visibility = App.ButtonVisibility(ADS.bTemp014_Error && ADS.bHalfSecPulse);
            tbkHT_5_SP.Foreground = App.ActiveState(ADS.bAOut005_On);
            tbkTemp015.Foreground = App.TempError(ADS.bTemp015_Error);
            tbkTemp015.Visibility = App.ButtonVisibility(ADS.bTemp015_Error && ADS.bHalfSecPulse);
            tbkHT_6_SP.Foreground = App.ActiveState(ADS.bAOut006_On);
            tbkTemp016.Foreground = App.TempError(ADS.bTemp016_Error);
            tbkTemp016.Visibility = App.ButtonVisibility(ADS.bTemp016_Error && ADS.bHalfSecPulse);

            //lblTemp017.Content = Units.UnitDisplay2(ADS.sTemp017, ADS.Temp017);
            tbkTemp017.Foreground = App.TempError(ADS.bTemp017_Error);
            tbkTemp017.Visibility = App.ButtonVisibility(ADS.bTemp017_Error && ADS.bHalfSecPulse);
            //lblTemp018.Content = Units.UnitDisplay2(ADS.sTemp018, ADS.Temp018);
            tbkTemp018.Foreground = App.TempError(ADS.bTemp018_Error);
            tbkTemp018.Visibility = App.ButtonVisibility(ADS.bTemp018_Error && ADS.bHalfSecPulse);
            //lblTemp019.Content = Units.UnitDisplay2(ADS.sTemp019, ADS.Temp019);
            tbkTemp019.Foreground = App.TempError(ADS.bTemp019_Error);
            tbkTemp019.Visibility = App.ButtonVisibility(ADS.bTemp019_Error && ADS.bHalfSecPulse);
            //lblTemp020.Content = Units.UnitDisplay2(ADS.sTemp020, ADS.Temp020);
            tbkTemp020.Foreground = App.TempError(ADS.bTemp020_Error);
            tbkTemp020.Visibility = App.ButtonVisibility(ADS.bTemp020_Error && ADS.bHalfSecPulse);
            //lblTemp021.Content = Units.UnitDisplay2(ADS.sTemp021, ADS.Temp021);
            tbkTemp021.Foreground = App.TempError(ADS.bTemp021_Error);
            tbkTemp021.Visibility = App.ButtonVisibility(ADS.bTemp021_Error && ADS.bHalfSecPulse);
            //lblTemp022.Content = Units.UnitDisplay2(ADS.sTemp022, ADS.Temp022);
            tbkTemp022.Foreground = App.TempError(ADS.bTemp022_Error);
            tbkTemp022.Visibility = App.ButtonVisibility(ADS.bTemp022_Error && ADS.bHalfSecPulse);

            lblTemp023.Content = Units.UnitDisplay2(ADS.sTemp023, ADS.Temp023);
            tbkTemp023.Foreground = App.TempError(ADS.bTemp023_Error);
            tbkTemp023.Visibility = App.ButtonVisibility(ADS.bTemp023_Error && ADS.bHalfSecPulse);
            lblTemp024.Content = Units.UnitDisplay2(ADS.sTemp024, ADS.Temp024);
            tbkTemp024.Foreground = App.TempError(ADS.bTemp024_Error);
            tbkTemp024.Visibility = App.ButtonVisibility(ADS.bTemp024_Error && ADS.bHalfSecPulse);
            lblTemp025.Content = Units.UnitDisplay2(ADS.sTemp025, ADS.Temp025);
            tbkTemp025.Foreground = App.TempError(ADS.bTemp025_Error);
            tbkTemp025.Visibility = App.ButtonVisibility(ADS.bTemp025_Error && ADS.bHalfSecPulse);
            lblTemp026.Content = Units.UnitDisplay2(ADS.sTemp026, ADS.Temp026);
            tbkTemp026.Foreground = App.TempError(ADS.bTemp026_Error);
            tbkTemp026.Visibility = App.ButtonVisibility(ADS.bTemp026_Error && ADS.bHalfSecPulse);

            App.ButtonEnable(ref btnConfigGraphs, true);                // 08/14/23 PS - App.ButtonEnable(ref btnConfigGraphs, bReady);
            App.ButtonContentEnable(ref btnManualHTOut, bReady, ADS.bManualHeat, strAuto, strManual);
            App.ButtonOffOnEnable(ref btnHT_1_On, bReady, ADS.bAOut001_On);
            App.ButtonOffOnEnable(ref btnHT_1_On_copy, bReady, ADS.bAOut001_On);
            App.ButtonOffOnEnable(ref btnHT_2_On, bReady, ADS.bAOut002_On);
            App.ButtonOffOnEnable(ref btnHT_2_On_copy, bReady, ADS.bAOut002_On);
            App.ButtonOffOnEnable(ref btnHT_3_On, bReady, ADS.bAOut003_On);
            App.ButtonOffOnEnable(ref btnHT_3_On_copy, bReady, ADS.bAOut003_On);
            App.ButtonOffOnEnable(ref btnHT_4_On, bReady, ADS.bAOut004_On);
            App.ButtonOffOnEnable(ref btnHT_4_On_copy, bReady, ADS.bAOut004_On);
            App.ButtonOffOnEnable(ref btnHT_5_On, bReady, ADS.bAOut005_On);
            App.ButtonOffOnEnable(ref btnHT_5_On_copy, bReady, ADS.bAOut005_On);
            App.ButtonOffOnEnable(ref btnHT_6_On, bReady, ADS.bAOut006_On);
            App.ButtonOffOnEnable(ref btnHT_6_On_copy, bReady, ADS.bAOut006_On);
            bButtonsEnabled = true;     // bReady && !ADS.bTestRunning;                                                     // 09/19/23 PS per Steve
            kpdAOut000.IsEnabled = bButtonsEnabled && ADS.bDI000_Open && ADS.bDI001_Open;                                   // 07/21/23 PS
            kpdAOut007.IsEnabled = bButtonsEnabled;                                                                         // 07/21/23 PS
            kpdAOut008.IsEnabled = bButtonsEnabled;                                                                         // 07/21/23 PS
            kpdAOut009.IsEnabled = bButtonsEnabled;                                                                         // 07/21/23 PS
            kpdAOut010.IsEnabled = bButtonsEnabled;                                                                         // 07/21/23 PS

            labFanTest.Foreground = App.ForegroundOnOff(ADS.bTestRunning);
            App.ButtonStopStartEnable(ref btnFanTest, bReady, ADS.bTestRunning);

            tbkAOut000.Foreground = App.ForegroundOnOff(ADS.bHS_1102_Enabled);
            lblAOut000RPM.Content = Units.UnitDisplay2(ADS.sAOut000RPM, ADS.AOut000RPM);

            tbkDO000.Foreground = App.ForegroundOnOff(ADS.bDO000);
            App.ButtonStopStartEnable(ref btnDO000, bButtonsEnabled, ADS.bDO000);

            tbkDO001.Foreground = App.ForegroundOnOff(ADS.bDO001);
            App.ButtonOpenCloseEnable(ref btnDO001, bButtonsEnabled, ADS.bDO001);

            tbkDO002.Foreground = App.ForegroundOnOff(ADS.bDO002);
            App.ButtonStopStartEnable(ref btnDO002, bButtonsEnabled, ADS.bDO002);

            tbkDO003.Foreground = App.ForegroundOnOff(ADS.bDO003);
            App.ButtonOpenCloseEnable(ref btnDO003, bButtonsEnabled, ADS.bDO003);

            tbkDO004.Foreground = App.ForegroundOnOff(ADS.bDO004);
            App.ButtonOpenCloseEnable(ref btnDO004, bButtonsEnabled, ADS.bDO004);
            elpDO004_Status.Fill = App.ElpStatus(ADS.bDO004_Open, ADS.bDO004_Closed);

            tbkDO005.Foreground = App.ForegroundOnOff(ADS.bDO005);
            App.ButtonOpenCloseEnable(ref btnDO005, bButtonsEnabled, ADS.bDO005);
            elpDO005_Status.Fill = App.ElpStatus(ADS.bDO005_Open, ADS.bDO005_Closed);

            tbkDO006.Foreground = App.ForegroundOnOff(ADS.bDO006);
            App.ButtonOpenCloseEnable(ref btnDO006, bButtonsEnabled, ADS.bDO006);
            elpDO006_Status.Fill = App.ElpStatus(ADS.bDO006_Open, ADS.bDO006_Closed);

            tbkDO007.Foreground = App.ForegroundOnOff(ADS.bDO007);
            App.ButtonStopStartEnable(ref btnDO007, bButtonsEnabled && !bDrainsOn, ADS.bDO007);
            elpDO007_High.Fill = App.ElpFill(ADS.bDO007_LSH);
            elpDO007_Low.Fill = App.ElpFill(ADS.bDO007_LSL);

            tbkDO008.Foreground = App.ForegroundOnOff(ADS.bDO008);
            App.ButtonOpenCloseEnable(ref btnDO008, bButtonsEnabled, ADS.bDO008);
            elpDO008_Status.Fill = App.ElpStatus(ADS.bDO008_Open, ADS.bDO008_Closed);

            tbkDO009.Foreground = App.ForegroundOnOff(ADS.bDO009);
            App.ButtonOpenCloseEnable(ref btnDO009, bButtonsEnabled, ADS.bDO009);
            elpDO009_Status.Fill = App.ElpStatus(ADS.bDO009_Open, ADS.bDO009_Closed);

            tbkDO010.Foreground = App.ForegroundOnOff(ADS.bDO010);
            App.ButtonStopStartEnable(ref btnDO010, bButtonsEnabled, ADS.bDO010);

            tbkDO011.Foreground = App.ForegroundOnOff(ADS.bDO011);
            App.ButtonStopStartEnable(ref btnDO011, bButtonsEnabled && !bDrainsOn, ADS.bDO011);
            elpDO011_High.Fill = App.ElpFill(ADS.bDO011_LSH);
            elpDO011_Low.Fill = App.ElpFill(ADS.bDO011_LSL);

            tbkDO012.Foreground = App.ForegroundOnOff(ADS.bDO012);
            App.ButtonStopStartEnable(ref btnDO012, bButtonsEnabled && !bDrainsOn, ADS.bDO012);

            tbkDO013.Foreground = App.ForegroundOnOff(ADS.bDO013);
            App.ButtonOpenCloseEnable(ref btnDO013, bButtonsEnabled, ADS.bDO013);
            elpDO013_Status.Fill = App.ElpStatus(ADS.bDO013_Open, ADS.bDO013_Closed);

            tbkDO014.Foreground = App.ForegroundOnOff(ADS.bDO014);
            App.ButtonOpenCloseEnable(ref btnDO014, bButtonsEnabled, ADS.bDO014);
            elpDO014_Status.Fill = App.ElpStatus(ADS.bDO014_Open, ADS.bDO014_Closed);

            tbkDO015.Foreground = App.ForegroundOnOff(ADS.bDO015);
            App.ButtonStopStartEnable(ref btnDO015, bButtonsEnabled, ADS.bDO015);

            tbkDO016.Foreground = App.ForegroundOnOff(ADS.bDO016);
            App.ButtonOpenCloseEnable(ref btnDO016, bButtonsEnabled, ADS.bDO016);
            elpDO016_Status.Fill = App.ElpStatus(ADS.bDO016_Open, ADS.bDO016_Closed);

            tbkDO017.Foreground = App.ForegroundOnOff(ADS.bDO017);
            App.ButtonOpenCloseEnable(ref btnDO017, bButtonsEnabled, ADS.bDO017);
            elpDO017_Status.Fill = App.ElpStatus(ADS.bDO017_Open, ADS.bDO017_Closed);

            tbkDO018.Foreground = App.ForegroundOnOff(ADS.bDO018);
            App.ButtonStopStartEnable(ref btnDO018, bButtonsEnabled, ADS.bDO018);
            elpDO018_High.Fill = App.ElpFill(ADS.bDO018_LSH);
            elpDO018_Low.Fill = App.ElpFill(ADS.bDO018_LSL);

            tbkDO019.Foreground = App.ForegroundOnOff(ADS.bDO019);
            App.ButtonStopStartEnable(ref btnDO019, bButtonsEnabled, ADS.bDO019);

            tbkDO020.Foreground = App.ForegroundOnOff(ADS.bDO020);
            App.ButtonStopStartEnable(ref btnDO020, bButtonsEnabled, ADS.bDO020);

            tbkDO021.Foreground = App.ForegroundOnOff(ADS.bDO021);
            App.ButtonOpenCloseEnable(ref btnDO021, bButtonsEnabled, ADS.bDO021);
            elpDO021_Status.Fill = App.ElpStatus(ADS.bDO021_Open, ADS.bDO021_Closed);

            tbkDO022.Foreground = App.ForegroundOnOff(ADS.bDO022);                                              // 09/20/23 PS
            App.ButtonOpenCloseEnable(ref btnDO022, bButtonsEnabled, ADS.bDO022);                               // 09/20/23 PS

            if (ADS.bAOutChange)
            {
                if (ADS.sAOut000_Out != Convert.ToSingle(kpdAOut000.Text))
                {
                    kpdAOut000.Text = ADS.sAOut000_Out.ToString("F1");
                }
                if (ADS.sAOut007_Out != Convert.ToSingle(kpdAOut007.Text))
                {
                    kpdAOut007.Text = ADS.sAOut007_Out.ToString("F1");
                }
                if (ADS.sAOut008_Out != Convert.ToSingle(kpdAOut008.Text))
                {
                    kpdAOut008.Text = ADS.sAOut008_Out.ToString("F1");
                }
                if (ADS.sAOut009_Out != Convert.ToSingle(kpdAOut009.Text))
                {
                    kpdAOut009.Text = ADS.sAOut009_Out.ToString("F1");
                }
                if (ADS.sAOut010_Out != Convert.ToSingle(kpdAOut010.Text))
                {
                    kpdAOut010.Text = ADS.sAOut010_Out.ToString("F1");
                }
                ADS.bAOutChange = false;
            }

            App.ButtonContentEnable(ref btnDrainOnOff, bButtonsEnabled, bDrainsOn, strDrainsOff, strDrainsOn);
        }

        #region Routed Events

        private void cbxPressureDrop_Checked(object sender, RoutedEventArgs e)                                              // 08/20/23 PS
        {
            ADS.bPressureDropTest = (bool)cbxPressureDrop.IsChecked;
        }

        private void kpdHT_1_SP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (App.Test_MinMax(ADS.AOut001[1] + strSetpoint, ADS.Temp011[2], strText, ADS.AOut001[4], ADS.AOut001[5], ADS.Temp011[3]))
                {
                    ADS.sAOut001_SP = Convert.ToSingle(strText);
                    ADS.SetAdsValue(ADS.GVTtgr + "HT_1_SP", ADS.sAOut001_SP);
                }
                else
                {
                    strText = Units.strFormat(ADS.sAOut001_SP, ADS.Temp011[3]);
                }
                App.bKpdCheckOn = false;
                kpdHT_1_SP.Text = strText;
                App.bKpdCheckOn = true;
            }
        }

        private void kpdHT_1_Manual_Output_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (Convert.ToInt32(strText) > iMax)
                {
                    strText = iMax.ToString();
                }
                App.bKpdCheckOn = false;
                kpdHT_1_Manual_Output.Text = strText;
                App.bKpdCheckOn = true;
                ADS.SetAdsValue(ADS.GVTtgr + "HT_1_Manual_Output", Convert.ToSingle(strText));
            }
        }

        private void kpdHT_2_SP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (App.Test_MinMax(ADS.AOut002[1] + strSetpoint, ADS.Temp012[2], strText, ADS.AOut002[4], ADS.AOut002[5], ADS.Temp012[3]))
                {
                    ADS.sAOut002_SP = Convert.ToSingle(strText);
                    ADS.SetAdsValue(ADS.GVTtgr + "HT_2_SP", ADS.sAOut002_SP);
                }
                else
                {
                    strText = Units.strFormat(ADS.sAOut002_SP, ADS.Temp012[3]);
                }
                App.bKpdCheckOn = false;
                kpdHT_2_SP.Text = strText;
                App.bKpdCheckOn = true;
            }
        }

        private void kpdHT_2_Manual_Output_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (Convert.ToInt32(strText) > iMax)
                {
                    strText = iMax.ToString();
                }
                App.bKpdCheckOn = false;
                kpdHT_2_Manual_Output.Text = strText;
                App.bKpdCheckOn = true;
                ADS.SetAdsValue(ADS.GVTtgr + "HT_2_Manual_Output", Convert.ToSingle(strText));
            }
        }

        private void kpdHT_3_SP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (App.Test_MinMax(ADS.Temp013[1] + strSetpoint, ADS.Temp013[2], strText, ADS.AOut003[4], ADS.AOut003[5], ADS.Temp013[3]))
                {
                    ADS.sAOut003_SP = Convert.ToSingle(strText);
                    ADS.SetAdsValue(ADS.GVTtgr + "HT_3_SP", ADS.sAOut003_SP);
                }
                else
                {
                    strText = Units.strFormat(ADS.sAOut003_SP, ADS.Temp013[3]);
                }
                App.bKpdCheckOn = false;
                kpdHT_3_SP.Text = strText;
                App.bKpdCheckOn = true;
            }
        }

        private void kpdHT_3_Manual_Output_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (Convert.ToInt32(strText) > iMax)
                {
                    strText = iMax.ToString();
                }
                App.bKpdCheckOn = false;
                kpdHT_3_Manual_Output.Text = strText;
                App.bKpdCheckOn = true;
                ADS.SetAdsValue(ADS.GVTtgr + "HT_3_Manual_Output", Convert.ToSingle(strText));
            }
        }

        private void kpdHT_4_SP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (App.Test_MinMax(ADS.AOut004[1] + strSetpoint, ADS.Temp014[2], strText, ADS.AOut004[4], ADS.AOut004[5], ADS.Temp014[3]))
                {
                    ADS.sAOut004_SP = Convert.ToSingle(strText);
                    ADS.SetAdsValue(ADS.GVTtgr + "HT_4_SP", ADS.sAOut004_SP);
                }
                else
                {
                    strText = Units.strFormat(ADS.sAOut004_SP, ADS.Temp014[3]);
                }
                App.bKpdCheckOn = false;
                kpdHT_4_SP.Text = strText;
                App.bKpdCheckOn = true;
            }
        }

        private void kpdHT_4_Manual_Output_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (Convert.ToInt32(strText) > iMax)
                {
                    strText = iMax.ToString();
                }
                App.bKpdCheckOn = false;
                kpdHT_4_Manual_Output.Text = strText;
                App.bKpdCheckOn = true;
                ADS.SetAdsValue(ADS.GVTtgr + "HT_4_Manual_Output", Convert.ToSingle(strText));
            }
        }

        private void kpdHT_5_SP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (App.Test_MinMax(ADS.AOut005[1] + strSetpoint, ADS.Temp015[2], strText, ADS.AOut005[4], ADS.AOut005[5], ADS.Temp015[3]))
                {
                    ADS.sAOut005_SP = Convert.ToSingle(strText);
                    ADS.SetAdsValue(ADS.GVTtgr + "HT_5_SP", ADS.sAOut005_SP);
                }
                else
                {
                    strText = Units.strFormat(ADS.sAOut005_SP, ADS.Temp015[3]);
                }
                App.bKpdCheckOn = false;
                kpdHT_5_SP.Text = strText;
                App.bKpdCheckOn = true;
            }
        }

        private void kpdHT_5_Manual_Output_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (Convert.ToInt32(strText) > iMax)
                {
                    strText = iMax.ToString();
                }
                App.bKpdCheckOn = false;
                kpdHT_5_Manual_Output.Text = strText;
                App.bKpdCheckOn = true;
                ADS.SetAdsValue(ADS.GVTtgr + "HT_5_Manual_Output", Convert.ToSingle(strText));
            }
        }

        private void kpdHT_6_SP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (App.Test_MinMax(ADS.AOut006[1] + strSetpoint, ADS.Temp016[2], strText, ADS.AOut006[4], ADS.AOut006[5], ADS.Temp016[3]))
                {
                    ADS.sAOut006_SP = Convert.ToSingle(strText);
                    ADS.SetAdsValue(ADS.GVTtgr + "HT_6_SP", ADS.sAOut006_SP);
                }
                else
                {
                    strText = Units.strFormat(ADS.sAOut006_SP, ADS.Temp016[3]);
                }
                App.bKpdCheckOn = false;
                kpdHT_6_SP.Text = strText;
                App.bKpdCheckOn = true;
            }
        }

        private void kpdHT_6_Manual_Output_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                tbx = sender as TextBox;
                strText = tbx.Text;
                if (Convert.ToInt32(strText) > iMax)
                {
                    strText = iMax.ToString();
                }
                App.bKpdCheckOn = false;
                kpdHT_6_Manual_Output.Text = strText;
                App.bKpdCheckOn = true;
                ADS.SetAdsValue(ADS.GVTtgr + "HT_6_Manual_Output", Convert.ToSingle(strText));
            }
        }

        private void btnManualHTOut_Click(object sender, RoutedEventArgs e)
        {
            ADS.SetAdsValue(ADS.GVTtgb + "Manual_Heater_Mode", !ADS.bManualHeat);
        }

        private void btnHTOnOff_Click(object sender, RoutedEventArgs e)
        {
            btn = sender as Button;
            if (btn.Name == "btnHT_1_On" || btn.Name == "btnHT_1_On_copy")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.AOut001[0] + "_On", !ADS.bAOut001_On);
            }
            else if (btn.Name == "btnHT_2_On" || btn.Name == "btnHT_2_On_copy")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.AOut002[0] + "_On", !ADS.bAOut002_On);
            }
            else if (btn.Name == "btnHT_3_On" || btn.Name == "btnHT_3_On_copy")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.AOut003[0] + "_On", !ADS.bAOut003_On);
            }
            else if (btn.Name == "btnHT_4_On" || btn.Name == "btnHT_4_On_copy")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.AOut004[0] + "_On", !ADS.bAOut004_On);
            }
            else if (btn.Name == "btnHT_5_On" || btn.Name == "btnHT_5_On_copy")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.AOut005[0] + "_On", !ADS.bAOut005_On);
            }
            else if (btn.Name == "btnHT_6_On" || btn.Name == "btnHT_6_On_copy")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.AOut006[0] + "_On", !ADS.bAOut006_On);
            }
        }

        private void btnFanTest_Click(object sender, RoutedEventArgs e)
        {
            ADS.SetAdsValue(ADS.GVTtgb + "Test_Mode", !ADS.bTestRunning);
        }

        private void btnLoggingStopStart_Click(object sender, RoutedEventArgs e)                                            // 08/20/23 PS
        {
            if (ADS.iLogging > (short)LogType.No_Log)
            {
                lblTime.Content = "0:00:00";
                ADS.iLogging = (short)LogType.No_Log;
            }
            else
            {
                int response = App.WPFMessageBoxYesNoCancel(strProcessAWS, App.bgGray);
                if (response != (int)MsgResponse.Cancel)
                {
                    ADS.bProcessData = response == (int)MsgResponse.Yes;
                    ADS.iLogging = (short)LogType.Manual;
                }
            }
        }

        private void btnConfigGraphs_Click(object sender, RoutedEventArgs e)
        {
            Window GraphConfigure = new GraphConfig();
            _ = GraphConfigure.ShowDialog();
        }


        private void kpdAOut000_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ADS.bTCStarted && App.bKpdCheckOn)
            {
                if (App.Test_MinMax(ADS.AOut000[1], ADS.AOut000[2], kpdAOut000.Text, ADS.AOut000[4], ADS.AOut000[5], ADS.AOut000[3]))
                {
                    sAOut000 = (float)Convert.ToSingle(kpdAOut000.Text);
                    ADS.SetAdsValue(ADS.GVTtgr + ADS.AOut000[0], Convert.ToSingle(sAOut000));
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdAOut000.Text = Units.strFormat(sAOut000, ADS.AOut000[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdAOut007_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ADS.bTCStarted && App.bKpdCheckOn)
            {
                if (App.Test_MinMax(ADS.AOut007[1], ADS.AOut007[2], kpdAOut007.Text, ADS.AOut007[4], ADS.AOut007[5], ADS.AOut007[3]))
                {
                    sAOut007 = (float)Convert.ToSingle(kpdAOut007.Text);
                    ADS.SetAdsValue(ADS.GVTtgr + ADS.AOut007[0], Convert.ToSingle(sAOut007));
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdAOut007.Text = Units.strFormat(sAOut007, ADS.AOut007[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdAOut008_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ADS.bTCStarted && App.bKpdCheckOn)
            {
                if (App.Test_MinMax(ADS.AOut008[1], ADS.AOut008[2], kpdAOut008.Text, ADS.AOut008[4], ADS.AOut008[5], ADS.AOut008[3]))
                {
                    sAOut008 = (float)Convert.ToSingle(kpdAOut008.Text);
                    ADS.SetAdsValue(ADS.GVTtgr + ADS.AOut008[0], Convert.ToSingle(sAOut008));
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdAOut008.Text = Units.strFormat(sAOut008, ADS.AOut008[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdAOut009_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ADS.bTCStarted && App.bKpdCheckOn)
            {
                if (App.Test_MinMax(ADS.AOut009[1], ADS.AOut009[2], kpdAOut009.Text, ADS.AOut009[4], ADS.AOut009[5], ADS.AOut009[3]))
                {
                    sAOut009 = (float)Convert.ToSingle(kpdAOut009.Text);
                    ADS.SetAdsValue(ADS.GVTtgr + ADS.AOut009[0], Convert.ToSingle(sAOut009));
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdAOut009.Text = Units.strFormat(sAOut009, ADS.AOut009[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdAOut010_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ADS.bTCStarted && App.bKpdCheckOn)
            {
                if (App.Test_MinMax(ADS.AOut010[1], ADS.AOut010[2], kpdAOut010.Text, ADS.AOut010[4], ADS.AOut010[5], ADS.AOut010[3]))
                {
                    sAOut010 = (float)Convert.ToSingle(kpdAOut010.Text);
                    ADS.SetAdsValue(ADS.GVTtgr + ADS.AOut010[0], Convert.ToSingle(sAOut010));
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdAOut010.Text = Units.strFormat(sAOut010, ADS.AOut010[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void btnDO_Click(object sender, RoutedEventArgs e)
        {
            btn = sender as Button;
            if (btn.Name == "btnDO000")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO000[0], !ADS.bDO000);
            }
            else if (btn.Name == "btnDO001")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO001[0], !ADS.bDO001);
            }
            else if (btn.Name == "btnDO002")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO002[0], !ADS.bDO002);
            }
            else if (btn.Name == "btnDO003")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO003[0], !ADS.bDO003);
            }
            else if (btn.Name == "btnDO004")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO004[0], !ADS.bDO004);
            }
            else if (btn.Name == "btnDO005")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO005[0], !ADS.bDO005);
            }
            else if (btn.Name == "btnDO006")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO006[0], !ADS.bDO006);
            }
            else if (btn.Name == "btnDO007")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO007[0], !ADS.bDO007);
            }
            else if (btn.Name == "btnDO008")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO008[0], !ADS.bDO008);
            }
            else if (btn.Name == "btnDO009")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO009[0], !ADS.bDO009);
            }
            else if (btn.Name == "btnDO010")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO010[0], !ADS.bDO010);
            }
            else if (btn.Name == "btnDO011")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO011[0], !ADS.bDO011);
            }
            else if (btn.Name == "btnDO012")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO012[0], !ADS.bDO012);
            }
            else if (btn.Name == "btnDO013")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO013[0], !ADS.bDO013);
            }
            else if (btn.Name == "btnDO014")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO014[0], !ADS.bDO014);
            }
            else if (btn.Name == "btnDO015")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO015[0], !ADS.bDO015);
            }
            else if (btn.Name == "btnDO016")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO016[0], !ADS.bDO016);
            }
            else if (btn.Name == "btnDO017")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO017[0], !ADS.bDO017);
            }
            else if (btn.Name == "btnDO018")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO018[0], !ADS.bDO018);
            }
            else if (btn.Name == "btnDO019")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO019[0], !ADS.bDO019);
            }
            else if (btn.Name == "btnDO020")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO020[0], !ADS.bDO020);
            }
            else if (btn.Name == "btnDO021")
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO021[0], !ADS.bDO021);
            }
            else if (btn.Name == "btnDO022")                                                                        // 09/20/23 PS
            {
                ADS.SetAdsValue(ADS.GVTtgb + ADS.DO022[0], !ADS.bDO022);                                            // 09/20/23 PS
            }
        }

        private void btnDrainOnOff_Click(object sender, RoutedEventArgs e)
        {
            bDrainsOn = !bDrainsOn;
            ADS.SetAdsValue(ADS.GVTtgb + "Drains_On", bDrainsOn);
        }

        #endregion

        #region Initial configuration

        public void LoadConfiguration_Manual()
        {
            App.bKpdCheckOn = false;
            kpdHT_1_SP.Text = Units.strFormat(ADS.sAOut001_SP, ADS.AOut001[3]);
            kpdHT_2_SP.Text = Units.strFormat(ADS.sAOut002_SP, ADS.AOut002[3]);
            kpdHT_3_SP.Text = Units.strFormat(ADS.sAOut003_SP, ADS.AOut003[3]);
            kpdHT_4_SP.Text = Units.strFormat(ADS.sAOut004_SP, ADS.AOut004[3]);
            kpdHT_5_SP.Text = Units.strFormat(ADS.sAOut005_SP, ADS.AOut005[3]);
            kpdHT_6_SP.Text = Units.strFormat(ADS.sAOut006_SP, ADS.AOut006[3]);

            tbkAIn000.Text = ADS.AIn000[1];
            tbkAIn001.Text = ADS.AIn001[1];
            tbkAIn002.Text = ADS.AIn002[1];
            tbkAIn003.Text = ADS.AIn003[1];
            tbkAIn004.Text = ADS.AIn004[1];
            tbkAIn005.Text = ADS.AIn005[1];
            tbkAIn006.Text = ADS.AIn006[1];
            tbkAIn007.Text = ADS.AIn007[1];
            tbkAIn008.Text = ADS.AIn008[1];
            tbkAIn009.Text = ADS.AIn009[1];
            tbkAIn010.Text = ADS.AIn010[1];
            tbkAIn011.Text = ADS.AIn011[1];
            tbkAIn012.Text = ADS.AIn012[1];
            tbkAIn013.Text = ADS.AIn013[1];
            tbkAIn014.Text = ADS.AIn014[1];
            tbkAIn015.Text = ADS.AIn015[1];
            tbkAIn016.Text = ADS.AIn016[1];
            tbkAIn017.Text = ADS.AIn017[1];
            tbkAIn018.Text = ADS.AIn018[1];
            tbkAIn019.Text = ADS.AIn019[1];
            tbkAIn020.Text = ADS.AIn020[1];
            tbkAIn021.Text = ADS.AIn021[1];
            tbkAIn022.Text = ADS.AIn022[1];
            tbkAIn023.Text = ADS.AIn023[1];
            tbkAIn024.Text = ADS.AIn024[1];
            tbkAIn025.Text = ADS.AIn025[1];
            tbkAIn026.Text = ADS.AIn026[1];
            tbkAIn027.Text = ADS.AIn027[1];
            tbkAIn028.Text = ADS.AIn028[1];
            tbkAIn040.Text = ADS.AIn040[1];
            tbkAIn041.Text = ADS.AIn041[1];
            tbkAIn042.Text = ADS.AIn042[1];
            tbkAIn043.Text = ADS.AIn043[1];
            tbkAIn044.Text = ADS.AIn044[1];
            tbkAIn045.Text = ADS.AIn045[1];
            tbkAIn046.Text = ADS.AIn046[1];
            //tbkAIn050.Text = ADS.AIn050[1];
            //tbkAIn051.Text = ADS.AIn051[1];
            //tbkAIn052.Text = ADS.AIn052[1];
            //tbkAIn053.Text = ADS.AIn053[1];
            //tbkAIn054.Text = ADS.AIn054[1];
            //tbkAIn055.Text = ADS.AIn055[1];
            tbkTemp000.Text = ADS.Temp000[1];
            tbkTemp001.Text = ADS.Temp001[1];
            tbkTemp002.Text = ADS.Temp002[1];
            tbkTemp003.Text = ADS.Temp003[1];
            tbkTemp004.Text = ADS.Temp004[1];
            tbkTemp005.Text = ADS.Temp005[1];
            tbkTemp006.Text = ADS.Temp006[1];
            tbkTemp007.Text = ADS.Temp007[1];
            tbkTemp008.Text = ADS.Temp008[1];
            tbkTemp009.Text = ADS.Temp009[1];
            tbkTemp010.Text = ADS.Temp010[1];
            tbkTemp011.Text = ADS.Temp011[1];
            tbkTemp012.Text = ADS.Temp012[1];
            tbkTemp013.Text = ADS.Temp013[1];
            tbkTemp014.Text = ADS.Temp014[1];
            tbkTemp015.Text = ADS.Temp015[1];
            tbkTemp016.Text = ADS.Temp016[1];
            tbkTemp017.Text = ADS.Temp017[1];
            tbkTemp018.Text = ADS.Temp018[1];
            tbkTemp019.Text = ADS.Temp019[1];
            tbkTemp020.Text = ADS.Temp020[1];
            tbkTemp021.Text = ADS.Temp021[1];
            tbkTemp022.Text = ADS.Temp022[1];
            tbkTemp023.Text = ADS.Temp023[1];
            tbkTemp024.Text = ADS.Temp024[1];
            tbkTemp025.Text = ADS.Temp025[1];
            tbkTemp026.Text = ADS.Temp026[1];
            tbkAOut007.Text = ADS.AOut007[1];
            tbkAOut008.Text = ADS.AOut008[1];
            tbkAOut009.Text = ADS.AOut009[1];
            tbkAOut010.Text = ADS.AOut010[1];
            tbkAOut000.Text = ADS.AOut000[1];
            tbkAOut000RPM.Text = ADS.AOut000RPM[1];
            tbkDO000.Text = ADS.DO000[1];
            tbkDO001.Text = ADS.DO001[1];
            tbkDO002.Text = ADS.DO002[1];
            tbkDO003.Text = ADS.DO003[1];
            tbkDO004.Text = ADS.DO004[1];
            tbkDO005.Text = ADS.DO005[1];
            tbkDO006.Text = ADS.DO006[1];
            tbkDO007.Text = ADS.DO007[1];
            tbkDO008.Text = ADS.DO008[1];
            tbkDO009.Text = ADS.DO009[1];
            tbkDO010.Text = ADS.DO010[1];
            tbkDO011.Text = ADS.DO011[1];
            tbkDO012.Text = ADS.DO012[1];
            tbkDO013.Text = ADS.DO013[1];
            tbkDO014.Text = ADS.DO014[1];
            tbkDO015.Text = ADS.DO015[1];
            tbkDO016.Text = ADS.DO016[1];
            tbkDO017.Text = ADS.DO017[1];
            tbkDO018.Text = ADS.DO018[1];
            tbkDO019.Text = ADS.DO019[1];
            tbkDO020.Text = ADS.DO020[1];
            tbkDO021.Text = ADS.DO021[1];
            tbkDO022.Text = ADS.DO022[1];                                            // 09/20/23 PS

            tbkHT_1_SP.Text = ADS.AOut001[1] + strSetpoint;
            tbkHT_1_Manual_Output.Text = ADS.AOut001[1] + strManualOutput;
            tbkTemp001.Text = ADS.Temp001[1];
            tbkHT_1_Out.Text = ADS.AOut001[1] + strOutput;
            kpdHT_1_SP.Title = Units.KeyboardTitle(ADS.AOut001[1] + strSetpoint, ADS.AOut001[4], ADS.AOut001[5], ADS.Temp011[3], ADS.Temp011[2]);
            kpdHT_1_SP.Decimals = 1;
            kpdHT_1_SP.MaxStringLength = 3;
            kpdHT_1_Manual_Output.Title = Units.KeyboardTitle(ADS.AOut001[1] + " Manual Output", 0, iMax, "0", "0");

            tbkHT_2_SP.Text = ADS.AOut002[1] + strSetpoint;
            tbkHT_2_Manual_Output.Text = ADS.AOut002[1] + strManualOutput;
            tbkTemp002.Text = ADS.Temp002[1];
            tbkHT_2_Out.Text = ADS.AOut002[1] + strOutput;
            kpdHT_2_SP.Title = Units.KeyboardTitle(ADS.AOut002[1] + strSetpoint, ADS.AOut002[4], ADS.AOut002[5], ADS.Temp012[3], ADS.Temp012[2]);
            kpdHT_2_SP.Decimals = 1;
            kpdHT_2_SP.MaxStringLength = 3;
            kpdHT_2_Manual_Output.Title = Units.KeyboardTitle(ADS.AOut002[1], 0, iMax, "0", "0");

            tbkHT_3_SP.Text = ADS.AOut003[1] + strSetpoint;
            tbkHT_3_Manual_Output.Text = ADS.AOut003[1] + strManualOutput;
            tbkTemp003.Text = ADS.Temp003[1];
            tbkHT_3_Out.Text = ADS.AOut003[1] + strOutput;
            kpdHT_3_SP.Title = Units.KeyboardTitle(ADS.AOut003[1] + strSetpoint, ADS.AOut003[4], ADS.AOut003[5], ADS.Temp013[3], ADS.Temp013[2]);
            kpdHT_3_SP.Decimals = 1;
            kpdHT_3_SP.MaxStringLength = 3;
            kpdHT_3_Manual_Output.Title = Units.KeyboardTitle(ADS.AOut003[1], 0, iMax, "0", "0");

            tbkHT_4_SP.Text = ADS.AOut004[1] + strSetpoint;
            tbkHT_4_Manual_Output.Text = ADS.AOut004[1] + strManualOutput;
            tbkTemp004.Text = ADS.Temp004[1];
            tbkHT_4_Out.Text = ADS.AOut004[1] + strOutput;
            kpdHT_4_SP.Title = Units.KeyboardTitle(ADS.AOut004[1] + strSetpoint, ADS.AOut004[4], ADS.AOut004[5], ADS.Temp014[3], ADS.Temp014[2]);
            kpdHT_4_SP.Decimals = 1;
            kpdHT_4_SP.MaxStringLength = 3;
            kpdHT_4_Manual_Output.Title = Units.KeyboardTitle(ADS.AOut004[1], 0, iMax, "0", "0");

            tbkHT_5_SP.Text = ADS.AOut005[1] + strSetpoint;
            tbkHT_5_Manual_Output.Text = ADS.AOut005[1] + strManualOutput;
            tbkTemp005.Text = ADS.Temp005[1];
            tbkHT_5_Out.Text = ADS.AOut005[1] + strOutput;
            kpdHT_5_SP.Title = Units.KeyboardTitle(ADS.AOut005[1] + strSetpoint, ADS.AOut005[4], ADS.AOut005[5], ADS.Temp015[3], ADS.Temp015[2]);
            kpdHT_5_SP.Decimals = 1;
            kpdHT_5_SP.MaxStringLength = 3;
            kpdHT_5_Manual_Output.Title = Units.KeyboardTitle(ADS.AOut005[1], 0, iMax, "0", "0");

            tbkHT_6_SP.Text = ADS.AOut006[1] + strSetpoint;
            tbkHT_6_Manual_Output.Text = ADS.AOut006[1] + strManualOutput;
            tbkTemp006.Text = ADS.Temp006[1];
            tbkHT_6_Out.Text = ADS.AOut006[1] + strOutput;
            kpdHT_6_SP.Title = Units.KeyboardTitle(ADS.AOut006[1] + strSetpoint, ADS.AOut006[4], ADS.AOut006[5], ADS.Temp016[3], ADS.Temp016[2]);
            kpdHT_6_SP.Decimals = 1;
            kpdHT_6_SP.MaxStringLength = 3;
            kpdHT_6_Manual_Output.Title = Units.KeyboardTitle(ADS.AOut006[1], 0, iMax, "0", "0");

            kpdAOut000.Title = Units.KeyboardTitle(ADS.AOut000[1], ADS.AOut000[4], ADS.AOut000[5], ADS.AOut000[3], ADS.AOut000[2]);
            kpdAOut000.KBMode = Touch.KeyboardMode.Real;
            kpdAOut000.Decimals = 1;
            kpdAOut000.MaxStringLength = 3;

            kpdAOut007.Title = Units.KeyboardTitle(ADS.AOut007[1], ADS.AOut007[4], ADS.AOut007[5], ADS.AOut007[3], ADS.AOut007[2]);
            kpdAOut007.KBMode = Touch.KeyboardMode.Real;
            kpdAOut007.Decimals = 1;
            kpdAOut007.MaxStringLength = 3;

            kpdAOut008.Title = Units.KeyboardTitle(ADS.AOut008[1], ADS.AOut008[4], ADS.AOut008[5], ADS.AOut008[3], ADS.AOut008[2]);
            kpdAOut008.KBMode = Touch.KeyboardMode.Real;
            kpdAOut008.Decimals = 1;
            kpdAOut008.MaxStringLength = 3;

            kpdAOut009.Title = Units.KeyboardTitle(ADS.AOut009[1], ADS.AOut009[4], ADS.AOut009[5], ADS.AOut009[3], ADS.AOut009[2]);
            kpdAOut009.KBMode = Touch.KeyboardMode.Real;
            kpdAOut009.Decimals = 1;
            kpdAOut009.MaxStringLength = 3;

            kpdAOut010.Title = Units.KeyboardTitle(ADS.AOut010[1], ADS.AOut010[4], ADS.AOut010[5], ADS.AOut010[3], ADS.AOut010[2]);
            kpdAOut010.KBMode = Touch.KeyboardMode.Real;
            kpdAOut010.Decimals = 1;
            kpdAOut010.MaxStringLength = 3;

            App.bKpdCheckOn = true;
        }

        #endregion Initial configuration

    }
}
