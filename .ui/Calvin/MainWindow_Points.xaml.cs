// Carbon Capture - Calvin
// MainWindow_Points.xaml.cs
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

        private TextBox tbx;
        private ComboBox cmb;
        private bool bConfigOn;                                         // Turn off change actions during configuration
        private bool btnSavePoints_Enabled;
        private bool bSaving;
        private int iDec;
        private readonly int iMinDec = 2;
        private readonly string strAlias = " Alias";
        private readonly string strLowValue = " Low Value";
        private readonly string strHighValue = " High Value";
        private readonly string strMinimumValue = " Minimum Value";
        private readonly string strOffsetValue = " Offset Value";
        private string[] pt000 = new string[9];
        private string[] pt001 = new string[9];
        private string[] pt002 = new string[9];
        private string[] pt003 = new string[9];
        private string[] pt004 = new string[9];
        private string[] pt005 = new string[9];
        private string[] pt006 = new string[9];
        private string[] pt007 = new string[9];
        private string[] pt008 = new string[9];
        private string[] pt009 = new string[9];
        private string[] pt010 = new string[9];
        private string[] pt011 = new string[9];
        private string[] pt012 = new string[9];
        private string[] pt013 = new string[9];
        private string[] pt014 = new string[9];
        private string[] pt015 = new string[9];
        private string[] pt016 = new string[9];
        private string[] pt017 = new string[9];
        private string[] pt018 = new string[9];
        private string[] pt019 = new string[9];
        private string[] pt020 = new string[9];
        private string[] pt021 = new string[9];
        private string[] pt022 = new string[9];
        private string[] pt023 = new string[9];
        private string[] pt024 = new string[9];
        private string[] pt025 = new string[9];
        private string[] pt026 = new string[9];
        private string[] pt027 = new string[9];
        private string[] pt028 = new string[9];

        private string[] pt040 = new string[9];
        private string[] pt041 = new string[9];
        private string[] pt042 = new string[9];
        private string[] pt043 = new string[9];
        private string[] pt044 = new string[9];
        private string[] pt045 = new string[9];
        private string[] pt046 = new string[9];

        private string[] pt050 = new string[9];
        private string[] pt051 = new string[9];
        private string[] pt052 = new string[9];
        private string[] pt053 = new string[9];
        private string[] pt054 = new string[9];
        private string[] pt055 = new string[9];

        private string[] tmp000 = new string[8];
        private string[] tmp001 = new string[8];
        private string[] tmp002 = new string[8];
        private string[] tmp003 = new string[8];
        private string[] tmp004 = new string[8];
        private string[] tmp005 = new string[8];
        private string[] tmp006 = new string[8];
        private string[] tmp007 = new string[8];
        private string[] tmp008 = new string[8];
        private string[] tmp009 = new string[8];
        private string[] tmp010 = new string[8];
        private string[] tmp011 = new string[8];
        private string[] tmp012 = new string[8];
        private string[] tmp013 = new string[8];
        private string[] tmp014 = new string[8];
        private string[] tmp015 = new string[8];
        private string[] tmp016 = new string[8];
        private string[] tmp017 = new string[8];
        private string[] tmp018 = new string[8];
        private string[] tmp019 = new string[8];
        private string[] tmp020 = new string[8];
        private string[] tmp021 = new string[8];
        private string[] tmp022 = new string[8];
        private string[] tmp023 = new string[8];
        private string[] tmp024 = new string[8];
        private string[] tmp025 = new string[8];
        private string[] tmp026 = new string[8];

        private string[] aout000 = new string[8];
        private string[] aout000RPM = new string[8];
        private string[] aout001 = new string[8];
        private string[] aout002 = new string[8];
        private string[] aout003 = new string[8];
        private string[] aout004 = new string[8];
        private string[] aout005 = new string[8];
        private string[] aout006 = new string[8];
        private string[] aout007 = new string[8];
        private string[] aout008 = new string[8];
        private string[] aout009 = new string[8];
        private string[] aout010 = new string[8];

        private string[] do000 = new string[2];
        private string[] do001 = new string[2];
        private string[] do002 = new string[2];
        private string[] do003 = new string[2];
        private string[] do004 = new string[2];
        private string[] do005 = new string[2];
        private string[] do006 = new string[2];
        private string[] do007 = new string[2];
        private string[] do008 = new string[2];
        private string[] do009 = new string[2];
        private string[] do010 = new string[2];
        private string[] do011 = new string[2];
        private string[] do012 = new string[2];
        private string[] do013 = new string[2];
        private string[] do014 = new string[2];
        private string[] do015 = new string[2];
        private string[] do016 = new string[2];
        private string[] do017 = new string[2];
        private string[] do018 = new string[2];
        private string[] do019 = new string[2];
        private string[] do020 = new string[2];
        private string[] do021 = new string[2];
        private string[] do022 = new string[2];

        #endregion Variables

        #region Buttons

        public void UpdatePoints()
        {
            App.ButtonBlinkEnable(ref btnSavePoints, btnSavePoints_Enabled);
            App.ButtonEnable(ref btnResetPoints, btnSavePoints_Enabled);
            btnAIName.IsEnabled = !btnSavePoints_Enabled;
            btnAIAlias.IsEnabled = !btnSavePoints_Enabled;
            btnDOName.IsEnabled = !btnSavePoints_Enabled;
            btnDOAlias.IsEnabled = !btnSavePoints_Enabled;
            kpdTemp000_Alias.IsEnabled = true;              // 08/14/23 PS - bReady;                                            // This point enables/disables all points
            if (lblSaving.IsVisible && bSaving)
            {
                SavePoints();
            }
        }

        private void btnAIName_Click(object sender, RoutedEventArgs e)
        {
            btnAINameClick();
        }

        public void btnAINameClick()
        {
            Array.Sort(ADS.strAINames);
            SortAIPoints(true);
            App.TbkBoldBlue(ref tbkAIPoint, true);
            App.TbkBoldBlue(ref tbkAIAlias, false);
        }

        private void btnAIAlias_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(ADS.strAIAliases);
            SortAIPoints(false);
            App.TbkBoldBlue(ref tbkAIAlias, true);
            App.TbkBoldBlue(ref tbkAIPoint, false);
        }

        private void btnDOName_Click(object sender, RoutedEventArgs e)
        {
            btnDONameClick();
        }

        public void btnDONameClick()
        {
            SortDOPoints(true);
            App.TbkBoldBlue(ref tbkDOPoint, true);
            App.TbkBoldBlue(ref tbkDOAlias, false);
        }

        private void btnDOAlias_Click(object sender, RoutedEventArgs e)
        {
            SortDOPoints(false);
            App.TbkBoldBlue(ref tbkDOAlias, true);
            App.TbkBoldBlue(ref tbkDOPoint, false);
        }

        private void btnSavePoints_Click(object sender, RoutedEventArgs e)
        {
            lblSaving.Visibility = Visibility.Visible;
            bSaving = true;
            btnSavePoints_Enabled = false;
            App.ButtonBlinkEnable(ref btnSavePoints, false);
            App.ButtonEnable(ref btnResetPoints, false);
        }

        private void btnResetPoints_Click(object sender, RoutedEventArgs e)
        {
            btnSavePoints_Enabled = ValuesChanged();
            LoadPointValues();
            btnSavePoints_Enabled = false;
            App.ButtonBlinkEnable(ref btnSavePoints, false);
            App.ButtonEnable(ref btnResetPoints, false);
        }

        private void SavePoints()
        {
            for (int i = 0; i < 8; i++)
            {
                ADS.AIn000[8] = i >= 4 && ADS.AIn000[i] != pt000[i] ? "x" : ADS.AIn000[8];
                ADS.AIn000[i] = pt000[i];
                ADS.AIn001[8] = i >= 4 && ADS.AIn001[i] != pt001[i] ? "x" : ADS.AIn001[8];
                ADS.AIn001[i] = pt001[i];
                ADS.AIn002[8] = i >= 4 && ADS.AIn002[i] != pt002[i] ? "x" : ADS.AIn002[8];
                ADS.AIn002[i] = pt002[i];
                ADS.AIn003[8] = i >= 4 && ADS.AIn003[i] != pt003[i] ? "x" : ADS.AIn003[8];
                ADS.AIn003[i] = pt003[i];
                ADS.AIn004[8] = i >= 4 && ADS.AIn004[i] != pt004[i] ? "x" : ADS.AIn004[8];
                ADS.AIn004[i] = pt004[i];
                ADS.AIn005[8] = i >= 4 && ADS.AIn005[i] != pt005[i] ? "x" : ADS.AIn005[8];
                ADS.AIn005[i] = pt005[i];
                ADS.AIn006[8] = i >= 4 && ADS.AIn006[i] != pt006[i] ? "x" : ADS.AIn006[8];
                ADS.AIn006[i] = pt006[i];
                ADS.AIn007[8] = i >= 4 && ADS.AIn007[i] != pt007[i] ? "x" : ADS.AIn007[8];
                ADS.AIn007[i] = pt007[i];
                ADS.AIn008[8] = i >= 4 && ADS.AIn008[i] != pt008[i] ? "x" : ADS.AIn008[8];
                ADS.AIn008[i] = pt008[i];
                ADS.AIn009[8] = i >= 4 && ADS.AIn009[i] != pt009[i] ? "x" : ADS.AIn009[8];
                ADS.AIn009[i] = pt009[i];
                ADS.AIn010[8] = i >= 4 && ADS.AIn010[i] != pt010[i] ? "x" : ADS.AIn010[8];
                ADS.AIn010[i] = pt010[i];
                ADS.AIn011[8] = i >= 4 && ADS.AIn011[i] != pt011[i] ? "x" : ADS.AIn011[8];
                ADS.AIn011[i] = pt011[i];
                ADS.AIn012[8] = i >= 4 && ADS.AIn012[i] != pt012[i] ? "x" : ADS.AIn012[8];
                ADS.AIn012[i] = pt012[i];
                ADS.AIn013[8] = i >= 4 && ADS.AIn013[i] != pt013[i] ? "x" : ADS.AIn013[8];
                ADS.AIn013[i] = pt013[i];
                ADS.AIn014[8] = i >= 4 && ADS.AIn014[i] != pt014[i] ? "x" : ADS.AIn014[8];
                ADS.AIn014[i] = pt014[i];
                ADS.AIn015[8] = i >= 4 && ADS.AIn015[i] != pt015[i] ? "x" : ADS.AIn015[8];
                ADS.AIn015[i] = pt015[i];
                ADS.AIn016[8] = i >= 4 && ADS.AIn016[i] != pt016[i] ? "x" : ADS.AIn016[8];
                ADS.AIn016[i] = pt016[i];
                ADS.AIn017[8] = i >= 4 && ADS.AIn017[i] != pt017[i] ? "x" : ADS.AIn017[8];
                ADS.AIn017[i] = pt017[i];
                ADS.AIn018[8] = i >= 4 && ADS.AIn018[i] != pt018[i] ? "x" : ADS.AIn018[8];
                ADS.AIn018[i] = pt018[i];
                ADS.AIn019[8] = i >= 4 && ADS.AIn019[i] != pt019[i] ? "x" : ADS.AIn019[8];
                ADS.AIn019[i] = pt019[i];
                ADS.AIn020[8] = i >= 4 && ADS.AIn020[i] != pt020[i] ? "x" : ADS.AIn020[8];
                ADS.AIn020[i] = pt020[i];
                ADS.AIn021[8] = i >= 4 && ADS.AIn021[i] != pt021[i] ? "x" : ADS.AIn021[8];
                ADS.AIn021[i] = pt021[i];
                ADS.AIn022[8] = i >= 4 && ADS.AIn022[i] != pt022[i] ? "x" : ADS.AIn022[8];
                ADS.AIn022[i] = pt022[i];
                ADS.AIn023[8] = i >= 4 && ADS.AIn023[i] != pt023[i] ? "x" : ADS.AIn023[8];
                ADS.AIn023[i] = pt023[i];
                ADS.AIn024[8] = i >= 4 && ADS.AIn024[i] != pt024[i] ? "x" : ADS.AIn024[8];
                ADS.AIn024[i] = pt024[i];
                ADS.AIn025[8] = i >= 4 && ADS.AIn025[i] != pt025[i] ? "x" : ADS.AIn025[8];
                ADS.AIn025[i] = pt025[i];
                ADS.AIn026[8] = i >= 4 && ADS.AIn026[i] != pt026[i] ? "x" : ADS.AIn026[8];
                ADS.AIn026[i] = pt026[i];
                ADS.AIn027[8] = i >= 4 && ADS.AIn027[i] != pt027[i] ? "x" : ADS.AIn027[8];
                ADS.AIn027[i] = pt027[i];
                ADS.AIn028[8] = i >= 4 && ADS.AIn028[i] != pt028[i] ? "x" : ADS.AIn028[8];
                ADS.AIn028[i] = pt028[i];

                ADS.AIn040[8] = i >= 4 && ADS.AIn040[i] != pt040[i] ? "x" : ADS.AIn040[8];
                ADS.AIn040[i] = pt040[i];
                ADS.AIn041[8] = i >= 4 && ADS.AIn041[i] != pt041[i] ? "x" : ADS.AIn041[8];
                ADS.AIn041[i] = pt041[i];
                ADS.AIn042[8] = i >= 4 && ADS.AIn042[i] != pt042[i] ? "x" : ADS.AIn042[8];
                ADS.AIn042[i] = pt042[i];
                ADS.AIn043[8] = i >= 4 && ADS.AIn043[i] != pt043[i] ? "x" : ADS.AIn043[8];
                ADS.AIn043[i] = pt043[i];
                ADS.AIn044[8] = i >= 4 && ADS.AIn044[i] != pt044[i] ? "x" : ADS.AIn044[8];
                ADS.AIn044[i] = pt044[i];
                ADS.AIn045[8] = i >= 4 && ADS.AIn045[i] != pt045[i] ? "x" : ADS.AIn045[8];
                ADS.AIn045[i] = pt045[i];
                ADS.AIn046[8] = i >= 4 && ADS.AIn046[i] != pt046[i] ? "x" : ADS.AIn046[8];
                ADS.AIn046[i] = pt046[i];

                ADS.AIn050[i] = pt050[i];
                ADS.AIn051[i] = pt051[i];
                ADS.AIn052[i] = pt052[i];
                ADS.AIn053[i] = pt053[i];
                ADS.AIn054[i] = pt054[i];
                ADS.AIn055[i] = pt055[i];
                ADS.Temp000[i] = tmp000[i];
                ADS.Temp001[i] = tmp001[i];
                ADS.Temp002[i] = tmp002[i];
                ADS.Temp003[i] = tmp003[i];
                ADS.Temp004[i] = tmp004[i];
                ADS.Temp005[i] = tmp005[i];
                ADS.Temp006[i] = tmp006[i];
                ADS.Temp007[i] = tmp007[i];
                ADS.Temp008[i] = tmp008[i];
                ADS.Temp009[i] = tmp009[i];
                ADS.Temp010[i] = tmp010[i];
                ADS.Temp011[i] = tmp011[i];
                ADS.Temp012[i] = tmp012[i];
                ADS.Temp013[i] = tmp013[i];
                ADS.Temp014[i] = tmp014[i];
                ADS.Temp015[i] = tmp015[i];
                ADS.Temp016[i] = tmp016[i];
                ADS.Temp017[i] = tmp017[i];
                ADS.Temp018[i] = tmp018[i];
                ADS.Temp019[i] = tmp019[i];
                ADS.Temp020[i] = tmp020[i];
                ADS.Temp021[i] = tmp021[i];
                ADS.Temp022[i] = tmp022[i];
                ADS.Temp023[i] = tmp023[i];
                ADS.Temp024[i] = tmp024[i];
                ADS.Temp025[i] = tmp025[i];
                ADS.Temp026[i] = tmp026[i];
                ADS.AOut000[i] = aout000[i];
                ADS.AOut000RPM[i] = aout000RPM[i];
                ADS.AOut001[i] = aout001[i];
                ADS.AOut002[i] = aout002[i];
                ADS.AOut003[i] = aout003[i];
                ADS.AOut004[i] = aout004[i];
                ADS.AOut005[i] = aout005[i];
                ADS.AOut006[i] = aout006[i];
                ADS.AOut007[i] = aout007[i];
                ADS.AOut008[i] = aout008[i];
                ADS.AOut009[i] = aout009[i];
                ADS.AOut010[i] = aout010[i];
            }
            for (int i = 0; i < 2; i++)
            {
                ADS.DO000[i] = do000[i];
                ADS.DO001[i] = do001[i];
                ADS.DO002[i] = do002[i];
                ADS.DO003[i] = do003[i];
                ADS.DO004[i] = do004[i];
                ADS.DO005[i] = do005[i];
                ADS.DO006[i] = do006[i];
                ADS.DO007[i] = do007[i];
                ADS.DO008[i] = do008[i];
                ADS.DO009[i] = do009[i];
                ADS.DO010[i] = do010[i];
                ADS.DO011[i] = do011[i];
                ADS.DO012[i] = do012[i];
                ADS.DO013[i] = do013[i];
                ADS.DO014[i] = do014[i];
                ADS.DO015[i] = do015[i];
                ADS.DO016[i] = do016[i];
                ADS.DO017[i] = do017[i];
                ADS.DO018[i] = do018[i];
                ADS.DO019[i] = do019[i];
                ADS.DO020[i] = do020[i];
                ADS.DO021[i] = do021[i];
                ADS.DO022[i] = do022[i];                                            // 09/20/23 PS
            }
            Config.SavePointFile();
            ADS.LoadPointCollection();
            ADS.LoadGraphCombobox();
            ADS.SetTCPointValues();
            LoadConfiguration_Manual();
            ADS.bResetGraph = true;
            lblSaving.Visibility = Visibility.Hidden;
            bSaving = false;
        }

        #endregion Buttons

        #region SortPoints

        public void SortDOPoints(bool Names)
        {
            string point;
            int pos = Names ? 0 : 1;
            for (int i = 0; i < ADS.ciDOCount; i++)
            {
                ADS.bUsed[i] = false;
            }
            for (int i = 0; i < ADS.ciDOCount; i++)
            {
                point = Names ? ADS.strDONames[i] : ADS.strDOAliases[i];

                if (point == do000[pos] && !ADS.bUsed[0])
                {
                    ADS.bUsed[0] = true;
                    Grid.SetRow(tbkDO000_Name, i + 1);
                    Grid.SetRow(kpdDO000_Alias, i + 1);
                }
                else if (point == do001[pos] && !ADS.bUsed[1])
                {
                    ADS.bUsed[1] = true;
                    Grid.SetRow(tbkDO001_Name, i + 1);
                    Grid.SetRow(kpdDO001_Alias, i + 1);
                }
                else if (point == do002[pos] && !ADS.bUsed[2])
                {
                    ADS.bUsed[2] = true;
                    Grid.SetRow(tbkDO002_Name, i + 1);
                    Grid.SetRow(kpdDO002_Alias, i + 1);
                }
                else if (point == do003[pos] && !ADS.bUsed[3])
                {
                    ADS.bUsed[3] = true;
                    Grid.SetRow(tbkDO003_Name, i + 1);
                    Grid.SetRow(kpdDO003_Alias, i + 1);
                }
                else if (point == do004[pos] && !ADS.bUsed[4])
                {
                    ADS.bUsed[4] = true;
                    Grid.SetRow(tbkDO004_Name, i + 1);
                    Grid.SetRow(kpdDO004_Alias, i + 1);
                }
                else if (point == do005[pos] && !ADS.bUsed[5])
                {
                    ADS.bUsed[5] = true;
                    Grid.SetRow(tbkDO005_Name, i + 1);
                    Grid.SetRow(kpdDO005_Alias, i + 1);
                }
                else if (point == do006[pos] && !ADS.bUsed[6])
                {
                    ADS.bUsed[6] = true;
                    Grid.SetRow(tbkDO006_Name, i + 1);
                    Grid.SetRow(kpdDO006_Alias, i + 1);
                }
                else if (point == do007[pos] && !ADS.bUsed[7])
                {
                    ADS.bUsed[7] = true;
                    Grid.SetRow(tbkDO007_Name, i + 1);
                    Grid.SetRow(kpdDO007_Alias, i + 1);
                }
                else if (point == do008[pos] && !ADS.bUsed[8])
                {
                    ADS.bUsed[8] = true;
                    Grid.SetRow(tbkDO008_Name, i + 1);
                    Grid.SetRow(kpdDO008_Alias, i + 1);
                }
                else if (point == do009[pos] && !ADS.bUsed[9])
                {
                    ADS.bUsed[9] = true;
                    Grid.SetRow(tbkDO009_Name, i + 1);
                    Grid.SetRow(kpdDO009_Alias, i + 1);
                }
                else if (point == do010[pos] && !ADS.bUsed[10])
                {
                    ADS.bUsed[10] = true;
                    Grid.SetRow(tbkDO010_Name, i + 1);
                    Grid.SetRow(kpdDO010_Alias, i + 1);
                }
                else if (point == do011[pos] && !ADS.bUsed[11])
                {
                    ADS.bUsed[11] = true;
                    Grid.SetRow(tbkDO011_Name, i + 1);
                    Grid.SetRow(kpdDO011_Alias, i + 1);
                }
                else if (point == do012[pos] && !ADS.bUsed[12])
                {
                    ADS.bUsed[12] = true;
                    Grid.SetRow(tbkDO012_Name, i + 1);
                    Grid.SetRow(kpdDO012_Alias, i + 1);
                }
                else if (point == do013[pos] && !ADS.bUsed[13])
                {
                    ADS.bUsed[13] = true;
                    Grid.SetRow(tbkDO013_Name, i + 1);
                    Grid.SetRow(kpdDO013_Alias, i + 1);
                }
                else if (point == do014[pos] && !ADS.bUsed[14])
                {
                    ADS.bUsed[14] = true;
                    Grid.SetRow(tbkDO014_Name, i + 1);
                    Grid.SetRow(kpdDO014_Alias, i + 1);
                }
                else if (point == do015[pos] && !ADS.bUsed[15])
                {
                    ADS.bUsed[15] = true;
                    Grid.SetRow(tbkDO015_Name, i + 1);
                    Grid.SetRow(kpdDO015_Alias, i + 1);
                }
                else if (point == do016[pos] && !ADS.bUsed[16])
                {
                    ADS.bUsed[16] = true;
                    Grid.SetRow(tbkDO016_Name, i + 1);
                    Grid.SetRow(kpdDO016_Alias, i + 1);
                }
                else if (point == do017[pos] && !ADS.bUsed[17])
                {
                    ADS.bUsed[17] = true;
                    Grid.SetRow(tbkDO017_Name, i + 1);
                    Grid.SetRow(kpdDO017_Alias, i + 1);
                }
                else if (point == do018[pos] && !ADS.bUsed[18])
                {
                    ADS.bUsed[18] = true;
                    Grid.SetRow(tbkDO018_Name, i + 1);
                    Grid.SetRow(kpdDO018_Alias, i + 1);
                }
                else if (point == do019[pos] && !ADS.bUsed[19])
                {
                    ADS.bUsed[19] = true;
                    Grid.SetRow(tbkDO019_Name, i + 1);
                    Grid.SetRow(kpdDO019_Alias, i + 1);
                }
                else if (point == do020[pos] && !ADS.bUsed[20])
                {
                    ADS.bUsed[20] = true;
                    Grid.SetRow(tbkDO020_Name, i + 1);
                    Grid.SetRow(kpdDO020_Alias, i + 1);
                }
                else if (point == do021[pos] && !ADS.bUsed[21])                                            // 09/20/23 PS
                {
                    ADS.bUsed[21] = true;
                    Grid.SetRow(tbkDO021_Name, i + 1);
                    Grid.SetRow(kpdDO021_Alias, i + 1);
                }
                else if (point == do022[pos] && !ADS.bUsed[22])                                            // 09/20/23 PS
                {
                    ADS.bUsed[22] = true;
                    Grid.SetRow(tbkDO022_Name, i + 1);
                    Grid.SetRow(kpdDO022_Alias, i + 1);
                }
            }
        }

        public void SortAIPoints(bool Names)
        {
            string point;
            int pos = Names ? 0 : 1;
            for (int i = 0; i < ADS.ciAICount; i++)
            {
                ADS.bUsed[i] = false;
            }
            for (int i = 0; i < ADS.ciAICount; i++)
            {
                point = Names ? ADS.strAINames[i] : ADS.strAIAliases[i];

                if (point == pt000[pos] && !ADS.bUsed[0])
                {
                    ADS.bUsed[0] = true;
                    Grid.SetRow(tbkAIn000_Name, i + 1);
                    Grid.SetRow(kpdAIn000_Alias, i + 1);
                    Grid.SetRow(cmbAIn000_Units, i + 1);
                    Grid.SetRow(cmbAIn000_Decimals, i + 1);
                    Grid.SetRow(kpdAIn000_Low, i + 1);
                    Grid.SetRow(kpdAIn000_High, i + 1);
                    Grid.SetRow(kpdAIn000_Min, i + 1);
                    Grid.SetRow(kpdAIn000_Offset, i + 1);
                }
                else if (point == pt001[pos] && !ADS.bUsed[1])
                {
                    ADS.bUsed[1] = true;
                    Grid.SetRow(tbkAIn001_Name, i + 1);
                    Grid.SetRow(kpdAIn001_Alias, i + 1);
                    Grid.SetRow(cmbAIn001_Units, i + 1);
                    Grid.SetRow(cmbAIn001_Decimals, i + 1);
                    Grid.SetRow(kpdAIn001_Low, i + 1);
                    Grid.SetRow(kpdAIn001_High, i + 1);
                    Grid.SetRow(kpdAIn001_Min, i + 1);
                    Grid.SetRow(kpdAIn001_Offset, i + 1);
                }
                else if (point == pt002[pos] && !ADS.bUsed[2])
                {
                    ADS.bUsed[2] = true;
                    Grid.SetRow(tbkAIn002_Name, i + 1);
                    Grid.SetRow(kpdAIn002_Alias, i + 1);
                    Grid.SetRow(cmbAIn002_Units, i + 1);
                    Grid.SetRow(cmbAIn002_Decimals, i + 1);
                    Grid.SetRow(kpdAIn002_Low, i + 1);
                    Grid.SetRow(kpdAIn002_High, i + 1);
                    Grid.SetRow(kpdAIn002_Min, i + 1);
                    Grid.SetRow(kpdAIn002_Offset, i + 1);
                }
                else if (point == pt003[pos] && !ADS.bUsed[3])
                {
                    ADS.bUsed[3] = true;
                    Grid.SetRow(tbkAIn003_Name, i + 1);
                    Grid.SetRow(kpdAIn003_Alias, i + 1);
                    Grid.SetRow(cmbAIn003_Units, i + 1);
                    Grid.SetRow(cmbAIn003_Decimals, i + 1);
                    Grid.SetRow(kpdAIn003_Low, i + 1);
                    Grid.SetRow(kpdAIn003_High, i + 1);
                    Grid.SetRow(kpdAIn003_Min, i + 1);
                    Grid.SetRow(kpdAIn003_Offset, i + 1);
                }
                else if (point == pt004[pos] && !ADS.bUsed[4])
                {
                    ADS.bUsed[4] = true;
                    Grid.SetRow(tbkAIn004_Name, i + 1);
                    Grid.SetRow(kpdAIn004_Alias, i + 1);
                    Grid.SetRow(cmbAIn004_Units, i + 1);
                    Grid.SetRow(cmbAIn004_Decimals, i + 1);
                    Grid.SetRow(kpdAIn004_Low, i + 1);
                    Grid.SetRow(kpdAIn004_High, i + 1);
                    Grid.SetRow(kpdAIn004_Min, i + 1);
                    Grid.SetRow(kpdAIn004_Offset, i + 1);
                }
                else if (point == pt005[pos] && !ADS.bUsed[5])
                {
                    ADS.bUsed[5] = true;
                    Grid.SetRow(tbkAIn005_Name, i + 1);
                    Grid.SetRow(kpdAIn005_Alias, i + 1);
                    Grid.SetRow(cmbAIn005_Units, i + 1);
                    Grid.SetRow(cmbAIn005_Decimals, i + 1);
                    Grid.SetRow(kpdAIn005_Low, i + 1);
                    Grid.SetRow(kpdAIn005_High, i + 1);
                    Grid.SetRow(kpdAIn005_Min, i + 1);
                    Grid.SetRow(kpdAIn005_Offset, i + 1);
                }
                else if (point == pt006[pos] && !ADS.bUsed[6])
                {
                    ADS.bUsed[6] = true;
                    Grid.SetRow(tbkAIn006_Name, i + 1);
                    Grid.SetRow(kpdAIn006_Alias, i + 1);
                    Grid.SetRow(cmbAIn006_Units, i + 1);
                    Grid.SetRow(cmbAIn006_Decimals, i + 1);
                    Grid.SetRow(kpdAIn006_Low, i + 1);
                    Grid.SetRow(kpdAIn006_High, i + 1);
                    Grid.SetRow(kpdAIn006_Min, i + 1);
                    Grid.SetRow(kpdAIn006_Offset, i + 1);
                }
                else if (point == pt007[pos] && !ADS.bUsed[7])
                {
                    ADS.bUsed[7] = true;
                    Grid.SetRow(tbkAIn007_Name, i + 1);
                    Grid.SetRow(kpdAIn007_Alias, i + 1);
                    Grid.SetRow(cmbAIn007_Units, i + 1);
                    Grid.SetRow(cmbAIn007_Decimals, i + 1);
                    Grid.SetRow(kpdAIn007_Low, i + 1);
                    Grid.SetRow(kpdAIn007_High, i + 1);
                    Grid.SetRow(kpdAIn007_Min, i + 1);
                    Grid.SetRow(kpdAIn007_Offset, i + 1);
                }
                else if (point == pt008[pos] && !ADS.bUsed[8])
                {
                    ADS.bUsed[8] = true;
                    Grid.SetRow(tbkAIn008_Name, i + 1);
                    Grid.SetRow(kpdAIn008_Alias, i + 1);
                    Grid.SetRow(cmbAIn008_Units, i + 1);
                    Grid.SetRow(cmbAIn008_Decimals, i + 1);
                    Grid.SetRow(kpdAIn008_Low, i + 1);
                    Grid.SetRow(kpdAIn008_High, i + 1);
                    Grid.SetRow(kpdAIn008_Min, i + 1);
                    Grid.SetRow(kpdAIn008_Offset, i + 1);
                }
                else if (point == pt009[pos] && !ADS.bUsed[9])
                {
                    ADS.bUsed[9] = true;
                    Grid.SetRow(tbkAIn009_Name, i + 1);
                    Grid.SetRow(kpdAIn009_Alias, i + 1);
                    Grid.SetRow(cmbAIn009_Units, i + 1);
                    Grid.SetRow(cmbAIn009_Decimals, i + 1);
                    Grid.SetRow(kpdAIn009_Low, i + 1);
                    Grid.SetRow(kpdAIn009_High, i + 1);
                    Grid.SetRow(kpdAIn009_Min, i + 1);
                    Grid.SetRow(kpdAIn009_Offset, i + 1);
                }
                else if (point == pt010[pos] && !ADS.bUsed[10])
                {
                    ADS.bUsed[10] = true;
                    Grid.SetRow(tbkAIn010_Name, i + 1);
                    Grid.SetRow(kpdAIn010_Alias, i + 1);
                    Grid.SetRow(cmbAIn010_Units, i + 1);
                    Grid.SetRow(cmbAIn010_Decimals, i + 1);
                    Grid.SetRow(kpdAIn010_Low, i + 1);
                    Grid.SetRow(kpdAIn010_High, i + 1);
                    Grid.SetRow(kpdAIn010_Min, i + 1);
                    Grid.SetRow(kpdAIn010_Offset, i + 1);
                }
                else if (point == pt011[pos] && !ADS.bUsed[11])
                {
                    ADS.bUsed[11] = true;
                    Grid.SetRow(tbkAIn011_Name, i + 1);
                    Grid.SetRow(kpdAIn011_Alias, i + 1);
                    Grid.SetRow(cmbAIn011_Units, i + 1);
                    Grid.SetRow(cmbAIn011_Decimals, i + 1);
                    Grid.SetRow(kpdAIn011_Low, i + 1);
                    Grid.SetRow(kpdAIn011_High, i + 1);
                    Grid.SetRow(kpdAIn011_Min, i + 1);
                    Grid.SetRow(kpdAIn011_Offset, i + 1);
                }
                else if (point == pt012[pos] && !ADS.bUsed[12])
                {
                    ADS.bUsed[12] = true;
                    Grid.SetRow(tbkAIn012_Name, i + 1);
                    Grid.SetRow(kpdAIn012_Alias, i + 1);
                    Grid.SetRow(cmbAIn012_Units, i + 1);
                    Grid.SetRow(cmbAIn012_Decimals, i + 1);
                    Grid.SetRow(kpdAIn012_Low, i + 1);
                    Grid.SetRow(kpdAIn012_High, i + 1);
                    Grid.SetRow(kpdAIn012_Min, i + 1);
                    Grid.SetRow(kpdAIn012_Offset, i + 1);
                }
                else if (point == pt013[pos] && !ADS.bUsed[13])
                {
                    ADS.bUsed[13] = true;
                    Grid.SetRow(tbkAIn013_Name, i + 1);
                    Grid.SetRow(kpdAIn013_Alias, i + 1);
                    Grid.SetRow(cmbAIn013_Units, i + 1);
                    Grid.SetRow(cmbAIn013_Decimals, i + 1);
                    Grid.SetRow(kpdAIn013_Low, i + 1);
                    Grid.SetRow(kpdAIn013_High, i + 1);
                    Grid.SetRow(kpdAIn013_Min, i + 1);
                    Grid.SetRow(kpdAIn013_Offset, i + 1);
                }
                else if (point == pt014[pos] && !ADS.bUsed[14])
                {
                    ADS.bUsed[14] = true;
                    Grid.SetRow(tbkAIn014_Name, i + 1);
                    Grid.SetRow(kpdAIn014_Alias, i + 1);
                    Grid.SetRow(cmbAIn014_Units, i + 1);
                    Grid.SetRow(cmbAIn014_Decimals, i + 1);
                    Grid.SetRow(kpdAIn014_Low, i + 1);
                    Grid.SetRow(kpdAIn014_High, i + 1);
                    Grid.SetRow(kpdAIn014_Min, i + 1);
                    Grid.SetRow(kpdAIn014_Offset, i + 1);
                }
                else if (point == pt015[pos] && !ADS.bUsed[15])
                {
                    ADS.bUsed[15] = true;
                    Grid.SetRow(tbkAIn015_Name, i + 1);
                    Grid.SetRow(kpdAIn015_Alias, i + 1);
                    Grid.SetRow(cmbAIn015_Units, i + 1);
                    Grid.SetRow(cmbAIn015_Decimals, i + 1);
                    Grid.SetRow(kpdAIn015_Low, i + 1);
                    Grid.SetRow(kpdAIn015_High, i + 1);
                    Grid.SetRow(kpdAIn015_Min, i + 1);
                    Grid.SetRow(kpdAIn015_Offset, i + 1);
                }
                else if (point == pt016[pos] && !ADS.bUsed[16])
                {
                    ADS.bUsed[16] = true;
                    Grid.SetRow(tbkAIn016_Name, i + 1);
                    Grid.SetRow(kpdAIn016_Alias, i + 1);
                    Grid.SetRow(cmbAIn016_Units, i + 1);
                    Grid.SetRow(cmbAIn016_Decimals, i + 1);
                    Grid.SetRow(kpdAIn016_Low, i + 1);
                    Grid.SetRow(kpdAIn016_High, i + 1);
                    Grid.SetRow(kpdAIn016_Min, i + 1);
                    Grid.SetRow(kpdAIn016_Offset, i + 1);
                }
                else if (point == pt017[pos] && !ADS.bUsed[17])
                {
                    ADS.bUsed[17] = true;
                    Grid.SetRow(tbkAIn017_Name, i + 1);
                    Grid.SetRow(kpdAIn017_Alias, i + 1);
                    Grid.SetRow(cmbAIn017_Units, i + 1);
                    Grid.SetRow(cmbAIn017_Decimals, i + 1);
                    Grid.SetRow(kpdAIn017_Low, i + 1);
                    Grid.SetRow(kpdAIn017_High, i + 1);
                    Grid.SetRow(kpdAIn017_Min, i + 1);
                    Grid.SetRow(kpdAIn017_Offset, i + 1);
                }
                else if (point == pt018[pos] && !ADS.bUsed[18])
                {
                    ADS.bUsed[18] = true;
                    Grid.SetRow(tbkAIn018_Name, i + 1);
                    Grid.SetRow(kpdAIn018_Alias, i + 1);
                    Grid.SetRow(cmbAIn018_Units, i + 1);
                    Grid.SetRow(cmbAIn018_Decimals, i + 1);
                    Grid.SetRow(kpdAIn018_Low, i + 1);
                    Grid.SetRow(kpdAIn018_High, i + 1);
                    Grid.SetRow(kpdAIn018_Min, i + 1);
                    Grid.SetRow(kpdAIn018_Offset, i + 1);
                }
                else if (point == pt019[pos] && !ADS.bUsed[19])
                {
                    ADS.bUsed[19] = true;
                    Grid.SetRow(tbkAIn019_Name, i + 1);
                    Grid.SetRow(kpdAIn019_Alias, i + 1);
                    Grid.SetRow(cmbAIn019_Units, i + 1);
                    Grid.SetRow(cmbAIn019_Decimals, i + 1);
                    Grid.SetRow(kpdAIn019_Low, i + 1);
                    Grid.SetRow(kpdAIn019_High, i + 1);
                    Grid.SetRow(kpdAIn019_Min, i + 1);
                    Grid.SetRow(kpdAIn019_Offset, i + 1);
                }
                else if (point == pt020[pos] && !ADS.bUsed[20])
                {
                    ADS.bUsed[20] = true;
                    Grid.SetRow(tbkAIn020_Name, i + 1);
                    Grid.SetRow(kpdAIn020_Alias, i + 1);
                    Grid.SetRow(cmbAIn020_Units, i + 1);
                    Grid.SetRow(cmbAIn020_Decimals, i + 1);
                    Grid.SetRow(kpdAIn020_Low, i + 1);
                    Grid.SetRow(kpdAIn020_High, i + 1);
                    Grid.SetRow(kpdAIn020_Min, i + 1);
                    Grid.SetRow(kpdAIn020_Offset, i + 1);
                }
                else if (point == pt021[pos] && !ADS.bUsed[21])
                {
                    ADS.bUsed[21] = true;
                    Grid.SetRow(tbkAIn021_Name, i + 1);
                    Grid.SetRow(kpdAIn021_Alias, i + 1);
                    Grid.SetRow(cmbAIn021_Units, i + 1);
                    Grid.SetRow(cmbAIn021_Decimals, i + 1);
                    Grid.SetRow(kpdAIn021_Low, i + 1);
                    Grid.SetRow(kpdAIn021_High, i + 1);
                    Grid.SetRow(kpdAIn021_Min, i + 1);
                    Grid.SetRow(kpdAIn021_Offset, i + 1);
                }
                else if (point == pt022[pos] && !ADS.bUsed[22])
                {
                    ADS.bUsed[22] = true;
                    Grid.SetRow(tbkAIn022_Name, i + 1);
                    Grid.SetRow(kpdAIn022_Alias, i + 1);
                    Grid.SetRow(cmbAIn022_Units, i + 1);
                    Grid.SetRow(cmbAIn022_Decimals, i + 1);
                    Grid.SetRow(kpdAIn022_Low, i + 1);
                    Grid.SetRow(kpdAIn022_High, i + 1);
                    Grid.SetRow(kpdAIn022_Min, i + 1);
                    Grid.SetRow(kpdAIn022_Offset, i + 1);
                }
                else if (point == pt023[pos] && !ADS.bUsed[23])
                {
                    ADS.bUsed[23] = true;
                    Grid.SetRow(tbkAIn023_Name, i + 1);
                    Grid.SetRow(kpdAIn023_Alias, i + 1);
                    Grid.SetRow(cmbAIn023_Units, i + 1);
                    Grid.SetRow(cmbAIn023_Decimals, i + 1);
                    Grid.SetRow(kpdAIn023_Low, i + 1);
                    Grid.SetRow(kpdAIn023_High, i + 1);
                    Grid.SetRow(kpdAIn023_Min, i + 1);
                    Grid.SetRow(kpdAIn023_Offset, i + 1);
                }
                else if (point == pt024[pos] && !ADS.bUsed[24])
                {
                    ADS.bUsed[24] = true;
                    Grid.SetRow(tbkAIn024_Name, i + 1);
                    Grid.SetRow(kpdAIn024_Alias, i + 1);
                    Grid.SetRow(cmbAIn024_Units, i + 1);
                    Grid.SetRow(cmbAIn024_Decimals, i + 1);
                    Grid.SetRow(kpdAIn024_Low, i + 1);
                    Grid.SetRow(kpdAIn024_High, i + 1);
                    Grid.SetRow(kpdAIn024_Min, i + 1);
                    Grid.SetRow(kpdAIn024_Offset, i + 1);
                }
                else if (point == pt025[pos] && !ADS.bUsed[25])
                {
                    ADS.bUsed[25] = true;
                    Grid.SetRow(tbkAIn025_Name, i + 1);
                    Grid.SetRow(kpdAIn025_Alias, i + 1);
                    Grid.SetRow(cmbAIn025_Units, i + 1);
                    Grid.SetRow(cmbAIn025_Decimals, i + 1);
                    Grid.SetRow(kpdAIn025_Low, i + 1);
                    Grid.SetRow(kpdAIn025_High, i + 1);
                    Grid.SetRow(kpdAIn025_Min, i + 1);
                    Grid.SetRow(kpdAIn025_Offset, i + 1);
                }
                else if (point == pt026[pos] && !ADS.bUsed[26])
                {
                    ADS.bUsed[26] = true;
                    Grid.SetRow(tbkAIn026_Name, i + 1);
                    Grid.SetRow(kpdAIn026_Alias, i + 1);
                    Grid.SetRow(cmbAIn026_Units, i + 1);
                    Grid.SetRow(cmbAIn026_Decimals, i + 1);
                    Grid.SetRow(kpdAIn026_Low, i + 1);
                    Grid.SetRow(kpdAIn026_High, i + 1);
                    Grid.SetRow(kpdAIn026_Min, i + 1);
                    Grid.SetRow(kpdAIn026_Offset, i + 1);
                }
                else if (point == pt027[pos] && !ADS.bUsed[27])
                {
                    ADS.bUsed[27] = true;
                    Grid.SetRow(tbkAIn027_Name, i + 1);
                    Grid.SetRow(kpdAIn027_Alias, i + 1);
                    Grid.SetRow(cmbAIn027_Units, i + 1);
                    Grid.SetRow(cmbAIn027_Decimals, i + 1);
                    Grid.SetRow(kpdAIn027_Low, i + 1);
                    Grid.SetRow(kpdAIn027_High, i + 1);
                    Grid.SetRow(kpdAIn027_Min, i + 1);
                    Grid.SetRow(kpdAIn027_Offset, i + 1);
                }
                else if (point == pt028[pos] && !ADS.bUsed[28])
                {
                    ADS.bUsed[28] = true;
                    Grid.SetRow(tbkAIn028_Name, i + 1);
                    Grid.SetRow(kpdAIn028_Alias, i + 1);
                    Grid.SetRow(cmbAIn028_Units, i + 1);
                    Grid.SetRow(cmbAIn028_Decimals, i + 1);
                    Grid.SetRow(kpdAIn028_Low, i + 1);
                    Grid.SetRow(kpdAIn028_High, i + 1);
                    Grid.SetRow(kpdAIn028_Min, i + 1);
                    Grid.SetRow(kpdAIn028_Offset, i + 1);
                }
            }
        }

        #endregion SortAIPoints

        #region Point Entries

        private void kpdAlias_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!bConfigOn)
            {
                tbx = sender as TextBox;
                if (tbx.Name == "kpdAIn000_Alias") { pt000[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn001_Alias") { pt001[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn002_Alias") { pt002[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn003_Alias") { pt003[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn004_Alias") { pt004[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn005_Alias") { pt005[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn006_Alias") { pt006[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn007_Alias") { pt007[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn008_Alias") { pt008[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn009_Alias") { pt009[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn010_Alias") { pt010[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn011_Alias") { pt011[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn012_Alias") { pt012[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn013_Alias") { pt013[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn014_Alias") { pt014[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn015_Alias") { pt015[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn016_Alias") { pt016[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn017_Alias") { pt017[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn018_Alias") { pt018[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn019_Alias") { pt019[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn020_Alias") { pt020[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn021_Alias") { pt021[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn022_Alias") { pt022[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn023_Alias") { pt023[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn024_Alias") { pt024[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn025_Alias") { pt025[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn026_Alias") { pt026[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn027_Alias") { pt027[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn028_Alias") { pt028[1] = tbx.Text; }

                else if (tbx.Name == "kpdAIn040_Alias") { pt040[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn041_Alias") { pt041[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn042_Alias") { pt042[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn043_Alias") { pt043[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn044_Alias") { pt044[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn045_Alias") { pt045[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn046_Alias") { pt046[1] = tbx.Text; }

                else if (tbx.Name == "kpdAIn050_Alias") { pt050[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn051_Alias") { pt051[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn052_Alias") { pt052[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn053_Alias") { pt053[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn054_Alias") { pt054[1] = tbx.Text; }
                else if (tbx.Name == "kpdAIn055_Alias") { pt055[1] = tbx.Text; }

                else if (tbx.Name == "kpdTemp000_Alias") { tmp000[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp001_Alias") { tmp001[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp002_Alias") { tmp002[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp003_Alias") { tmp003[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp004_Alias") { tmp004[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp005_Alias") { tmp005[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp006_Alias") { tmp006[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp007_Alias") { tmp007[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp008_Alias") { tmp008[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp009_Alias") { tmp009[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp010_Alias") { tmp010[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp011_Alias") { tmp011[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp012_Alias") { tmp012[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp013_Alias") { tmp013[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp014_Alias") { tmp014[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp015_Alias") { tmp015[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp016_Alias") { tmp016[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp017_Alias") { tmp017[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp018_Alias") { tmp018[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp019_Alias") { tmp019[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp020_Alias") { tmp020[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp021_Alias") { tmp021[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp022_Alias") { tmp022[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp023_Alias") { tmp023[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp024_Alias") { tmp024[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp025_Alias") { tmp025[1] = tbx.Text; }
                else if (tbx.Name == "kpdTemp026_Alias") { tmp026[1] = tbx.Text; }

                else if (tbx.Name == "kpdAOut000_Alias") { aout000[1] = tbx.Text; }
                else if (tbx.Name == "kpdAOut000RPM_Alias") { aout000RPM[1] = tbx.Text; }
                else if (tbx.Name == "kpdAOut002_Alias") { aout002[1] = tbx.Text; }
                else if (tbx.Name == "kpdAOut003_Alias") { aout003[1] = tbx.Text; }
                else if (tbx.Name == "kpdAOut004_Alias") { aout004[1] = tbx.Text; }
                else if (tbx.Name == "kpdAOut005_Alias") { aout005[1] = tbx.Text; }
                else if (tbx.Name == "kpdAOut006_Alias") { aout006[1] = tbx.Text; }
                else if (tbx.Name == "kpdAOut007_Alias") { aout007[1] = tbx.Text; }
                else if (tbx.Name == "kpdAOut008_Alias") { aout008[1] = tbx.Text; }
                else if (tbx.Name == "kpdAOut009_Alias") { aout009[1] = tbx.Text; }
                else if (tbx.Name == "kpdAOut010_Alias") { aout010[1] = tbx.Text; }

                else if (tbx.Name == "kpdDO000_Alias") { do000[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO001_Alias") { do001[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO002_Alias") { do002[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO003_Alias") { do003[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO004_Alias") { do004[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO005_Alias") { do005[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO006_Alias") { do006[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO007_Alias") { do007[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO008_Alias") { do008[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO009_Alias") { do009[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO010_Alias") { do010[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO011_Alias") { do011[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO012_Alias") { do012[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO013_Alias") { do013[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO014_Alias") { do014[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO015_Alias") { do015[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO016_Alias") { do016[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO017_Alias") { do017[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO018_Alias") { do018[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO019_Alias") { do019[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO020_Alias") { do020[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO021_Alias") { do021[1] = tbx.Text; }
                else if (tbx.Name == "kpdDO022_Alias") { do022[1] = tbx.Text; }                                 // 09/20/23 PS

                btnSavePoints_Enabled = ValuesChanged();
            }
        }

        private void kpdAnalog_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!bConfigOn && App.bKpdCheckOn)
            {
                tbx = sender as TextBox;

                if (tbx.Name == "kpdAIn000_Low") { UpdateLow(ref kpdAIn000_Low, ref pt000, kpdAIn000_High.Text); }
                else if (tbx.Name == "kpdAIn000_High") { UpdateHigh(ref kpdAIn000_High, ref pt000, kpdAIn000_Low.Text); }
                else if (tbx.Name == "kpdAIn000_Min") { pt000[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn000_Offset") { pt000[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn001_Low") { UpdateLow(ref kpdAIn001_Low, ref pt001, kpdAIn001_High.Text); }
                else if (tbx.Name == "kpdAIn001_High") { UpdateHigh(ref kpdAIn001_High, ref pt001, kpdAIn001_Low.Text); }
                else if (tbx.Name == "kpdAIn001_Min") { pt001[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn001_Offset") { pt001[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn002_Low") { UpdateLow(ref kpdAIn002_Low, ref pt002, kpdAIn002_High.Text); }
                else if (tbx.Name == "kpdAIn002_High") { UpdateHigh(ref kpdAIn002_High, ref pt002, kpdAIn002_Low.Text); }
                else if (tbx.Name == "kpdAIn002_Min") { pt002[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn002_Offset") { pt002[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn003_Low") { UpdateLow(ref kpdAIn003_Low, ref pt003, kpdAIn003_High.Text); }
                else if (tbx.Name == "kpdAIn003_High") { UpdateHigh(ref kpdAIn003_High, ref pt003, kpdAIn003_Low.Text); }
                else if (tbx.Name == "kpdAIn003_Min") { pt003[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn003_Offset") { pt003[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn004_Low") { UpdateLow(ref kpdAIn004_Low, ref pt004, kpdAIn004_High.Text); }
                else if (tbx.Name == "kpdAIn004_High") { UpdateHigh(ref kpdAIn004_High, ref pt004, kpdAIn004_Low.Text); }
                else if (tbx.Name == "kpdAIn004_Min") { pt004[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn004_Offset") { pt004[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn005_Low") { UpdateLow(ref kpdAIn005_Low, ref pt005, kpdAIn005_High.Text); }
                else if (tbx.Name == "kpdAIn005_High") { UpdateHigh(ref kpdAIn005_High, ref pt005, kpdAIn005_Low.Text); }
                else if (tbx.Name == "kpdAIn005_Min") { pt005[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn005_Offset") { pt005[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn006_Low") { UpdateLow(ref kpdAIn006_Low, ref pt006, kpdAIn006_High.Text); }
                else if (tbx.Name == "kpdAIn006_High") { UpdateHigh(ref kpdAIn006_High, ref pt006, kpdAIn006_Low.Text); }
                else if (tbx.Name == "kpdAIn006_Min") { pt006[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn006_Offset") { pt006[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn007_Low") { UpdateLow(ref kpdAIn007_Low, ref pt007, kpdAIn007_High.Text); }
                else if (tbx.Name == "kpdAIn007_High") { UpdateHigh(ref kpdAIn007_High, ref pt007, kpdAIn007_Low.Text); }
                else if (tbx.Name == "kpdAIn007_Min") { pt007[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn007_Offset") { pt007[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn008_Low") { UpdateLow(ref kpdAIn008_Low, ref pt008, kpdAIn008_High.Text); }
                else if (tbx.Name == "kpdAIn008_High") { UpdateHigh(ref kpdAIn008_High, ref pt008, kpdAIn008_Low.Text); }
                else if (tbx.Name == "kpdAIn008_Min") { pt008[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn008_Offset") { pt008[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn009_Low") { UpdateLow(ref kpdAIn009_Low, ref pt009, kpdAIn009_High.Text); }
                else if (tbx.Name == "kpdAIn009_High") { UpdateHigh(ref kpdAIn009_High, ref pt009, kpdAIn009_Low.Text); }
                else if (tbx.Name == "kpdAIn009_Min") { pt009[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn009_Offset") { pt009[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn010_Low") { UpdateLow(ref kpdAIn010_Low, ref pt010, kpdAIn010_High.Text); }
                else if (tbx.Name == "kpdAIn010_High") { UpdateHigh(ref kpdAIn010_High, ref pt010, kpdAIn010_Low.Text); }
                else if (tbx.Name == "kpdAIn010_Min") { pt010[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn010_Offset") { pt010[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn011_Low") { UpdateLow(ref kpdAIn011_Low, ref pt011, kpdAIn011_High.Text); }
                else if (tbx.Name == "kpdAIn011_High") { UpdateHigh(ref kpdAIn011_High, ref pt011, kpdAIn011_Low.Text); }
                else if (tbx.Name == "kpdAIn011_Min") { pt011[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn011_Offset") { pt011[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn012_Low") { UpdateLow(ref kpdAIn012_Low, ref pt012, kpdAIn012_High.Text); }
                else if (tbx.Name == "kpdAIn012_High") { UpdateHigh(ref kpdAIn012_High, ref pt012, kpdAIn012_Low.Text); }
                else if (tbx.Name == "kpdAIn012_Min") { pt012[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn012_Offset") { pt012[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn013_Low") { UpdateLow(ref kpdAIn013_Low, ref pt013, kpdAIn013_High.Text); }
                else if (tbx.Name == "kpdAIn013_High") { UpdateHigh(ref kpdAIn013_High, ref pt013, kpdAIn013_Low.Text); }
                else if (tbx.Name == "kpdAIn013_Min") { pt013[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn013_Offset") { pt013[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn014_Low") { UpdateLow(ref kpdAIn014_Low, ref pt014, kpdAIn014_High.Text); }
                else if (tbx.Name == "kpdAIn014_High") { UpdateHigh(ref kpdAIn014_High, ref pt014, kpdAIn014_Low.Text); }
                else if (tbx.Name == "kpdAIn014_Min") { pt014[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn014_Offset") { pt014[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn015_Low") { UpdateLow(ref kpdAIn015_Low, ref pt015, kpdAIn015_High.Text); }
                else if (tbx.Name == "kpdAIn015_High") { UpdateHigh(ref kpdAIn015_High, ref pt015, kpdAIn015_Low.Text); }
                else if (tbx.Name == "kpdAIn015_Min") { pt015[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn015_Offset") { pt015[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn016_Low") { UpdateLow(ref kpdAIn016_Low, ref pt016, kpdAIn016_High.Text); }
                else if (tbx.Name == "kpdAIn016_High") { UpdateHigh(ref kpdAIn016_High, ref pt016, kpdAIn016_Low.Text); }
                else if (tbx.Name == "kpdAIn016_Min") { pt016[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn016_Offset") { pt016[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn017_Low") { UpdateLow(ref kpdAIn017_Low, ref pt017, kpdAIn017_High.Text); }
                else if (tbx.Name == "kpdAIn017_High") { UpdateHigh(ref kpdAIn017_High, ref pt017, kpdAIn017_Low.Text); }
                else if (tbx.Name == "kpdAIn017_Min") { pt017[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn017_Offset") { pt017[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn018_Low") { UpdateLow(ref kpdAIn018_Low, ref pt018, kpdAIn018_High.Text); }
                else if (tbx.Name == "kpdAIn018_High") { UpdateHigh(ref kpdAIn018_High, ref pt018, kpdAIn018_Low.Text); }
                else if (tbx.Name == "kpdAIn018_Min") { pt018[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn018_Offset") { pt018[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn019_Low") { UpdateLow(ref kpdAIn019_Low, ref pt019, kpdAIn019_High.Text); }
                else if (tbx.Name == "kpdAIn019_High") { UpdateHigh(ref kpdAIn019_High, ref pt019, kpdAIn019_Low.Text); }
                else if (tbx.Name == "kpdAIn019_Min") { pt019[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn019_Offset") { pt019[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn020_Low") { UpdateLow(ref kpdAIn020_Low, ref pt020, kpdAIn020_High.Text); }
                else if (tbx.Name == "kpdAIn020_High") { UpdateHigh(ref kpdAIn020_High, ref pt020, kpdAIn020_Low.Text); }
                else if (tbx.Name == "kpdAIn020_Min") { pt020[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn020_Offset") { pt020[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn021_Low") { UpdateLow(ref kpdAIn021_Low, ref pt021, kpdAIn021_High.Text); }
                else if (tbx.Name == "kpdAIn021_High") { UpdateHigh(ref kpdAIn021_High, ref pt021, kpdAIn021_Low.Text); }
                else if (tbx.Name == "kpdAIn021_Min") { pt021[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn021_Offset") { pt021[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn022_Low") { UpdateLow(ref kpdAIn022_Low, ref pt022, kpdAIn022_High.Text); }
                else if (tbx.Name == "kpdAIn022_High") { UpdateHigh(ref kpdAIn022_High, ref pt022, kpdAIn022_Low.Text); }
                else if (tbx.Name == "kpdAIn022_Min") { pt022[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn022_Offset") { pt022[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn023_Low") { UpdateLow(ref kpdAIn023_Low, ref pt023, kpdAIn023_High.Text); }
                else if (tbx.Name == "kpdAIn023_High") { UpdateHigh(ref kpdAIn023_High, ref pt023, kpdAIn023_Low.Text); }
                else if (tbx.Name == "kpdAIn023_Min") { pt023[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn023_Offset") { pt023[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn024_Low") { UpdateLow(ref kpdAIn024_Low, ref pt024, kpdAIn024_High.Text); }
                else if (tbx.Name == "kpdAIn024_High") { UpdateHigh(ref kpdAIn024_High, ref pt024, kpdAIn024_Low.Text); }
                else if (tbx.Name == "kpdAIn024_Min") { pt024[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn024_Offset") { pt024[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn025_Low") { UpdateLow(ref kpdAIn025_Low, ref pt025, kpdAIn025_High.Text); }
                else if (tbx.Name == "kpdAIn025_High") { UpdateHigh(ref kpdAIn025_High, ref pt025, kpdAIn025_Low.Text); }
                else if (tbx.Name == "kpdAIn025_Min") { pt025[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn025_Offset") { pt025[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn026_Low") { UpdateLow(ref kpdAIn026_Low, ref pt026, kpdAIn026_High.Text); }
                else if (tbx.Name == "kpdAIn026_High") { UpdateHigh(ref kpdAIn026_High, ref pt026, kpdAIn026_Low.Text); }
                else if (tbx.Name == "kpdAIn026_Min") { pt026[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn026_Offset") { pt026[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn027_Low") { UpdateLow(ref kpdAIn027_Low, ref pt027, kpdAIn027_High.Text); }
                else if (tbx.Name == "kpdAIn027_High") { UpdateHigh(ref kpdAIn027_High, ref pt027, kpdAIn027_Low.Text); }
                else if (tbx.Name == "kpdAIn027_Min") { pt027[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn027_Offset") { pt027[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn028_Low") { UpdateLow(ref kpdAIn028_Low, ref pt028, kpdAIn028_High.Text); }
                else if (tbx.Name == "kpdAIn028_High") { UpdateHigh(ref kpdAIn028_High, ref pt028, kpdAIn028_Low.Text); }
                else if (tbx.Name == "kpdAIn028_Min") { pt028[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn028_Offset") { pt028[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn040_Low") { UpdateLow(ref kpdAIn040_Low, ref pt040, kpdAIn040_High.Text); }
                else if (tbx.Name == "kpdAIn040_High") { UpdateHigh(ref kpdAIn040_High, ref pt040, kpdAIn040_Low.Text); }
                else if (tbx.Name == "kpdAIn040_Min") { pt040[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn040_Offset") { pt040[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn041_Low") { UpdateLow(ref kpdAIn041_Low, ref pt041, kpdAIn041_High.Text); }
                else if (tbx.Name == "kpdAIn041_High") { UpdateHigh(ref kpdAIn041_High, ref pt041, kpdAIn041_Low.Text); }
                else if (tbx.Name == "kpdAIn041_Min") { pt041[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn041_Offset") { pt041[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn042_Low") { UpdateLow(ref kpdAIn042_Low, ref pt042, kpdAIn042_High.Text); }
                else if (tbx.Name == "kpdAIn042_High") { UpdateHigh(ref kpdAIn042_High, ref pt042, kpdAIn042_Low.Text); }
                else if (tbx.Name == "kpdAIn042_Min") { pt042[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn042_Offset") { pt042[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn043_Low") { UpdateLow(ref kpdAIn043_Low, ref pt043, kpdAIn043_High.Text); }
                else if (tbx.Name == "kpdAIn043_High") { UpdateHigh(ref kpdAIn043_High, ref pt043, kpdAIn043_Low.Text); }
                else if (tbx.Name == "kpdAIn043_Min") { pt043[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn043_Offset") { pt043[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn044_Low") { UpdateLow(ref kpdAIn044_Low, ref pt044, kpdAIn044_High.Text); }
                else if (tbx.Name == "kpdAIn044_High") { UpdateHigh(ref kpdAIn044_High, ref pt044, kpdAIn044_Low.Text); }
                else if (tbx.Name == "kpdAIn044_Min") { pt044[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn044_Offset") { pt044[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn045_Low") { UpdateLow(ref kpdAIn045_Low, ref pt045, kpdAIn045_High.Text); }
                else if (tbx.Name == "kpdAIn045_High") { UpdateHigh(ref kpdAIn045_High, ref pt045, kpdAIn045_Low.Text); }
                else if (tbx.Name == "kpdAIn045_Min") { pt045[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn045_Offset") { pt045[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn046_Low") { UpdateLow(ref kpdAIn046_Low, ref pt046, kpdAIn046_High.Text); }
                else if (tbx.Name == "kpdAIn046_High") { UpdateHigh(ref kpdAIn046_High, ref pt046, kpdAIn046_Low.Text); }
                else if (tbx.Name == "kpdAIn046_Min") { pt046[6] = tbx.Text; }
                else if (tbx.Name == "kpdAIn046_Offset") { pt046[7] = tbx.Text; }

                else if (tbx.Name == "kpdAIn050_Low") { UpdateLow(ref kpdAIn050_Low, ref pt050, kpdAIn050_High.Text); }
                else if (tbx.Name == "kpdAIn050_High") { UpdateHigh(ref kpdAIn050_High, ref pt050, kpdAIn050_Low.Text); }

                else if (tbx.Name == "kpdAIn051_Low") { UpdateLow(ref kpdAIn051_Low, ref pt051, kpdAIn051_High.Text); }
                else if (tbx.Name == "kpdAIn051_High") { UpdateHigh(ref kpdAIn051_High, ref pt051, kpdAIn051_Low.Text); }

                else if (tbx.Name == "kpdAIn052_Low") { UpdateLow(ref kpdAIn052_Low, ref pt052, kpdAIn052_High.Text); }
                else if (tbx.Name == "kpdAIn052_High") { UpdateHigh(ref kpdAIn052_High, ref pt052, kpdAIn052_Low.Text); }

                else if (tbx.Name == "kpdAIn053_Low") { UpdateLow(ref kpdAIn053_Low, ref pt053, kpdAIn053_High.Text); }
                else if (tbx.Name == "kpdAIn053_High") { UpdateHigh(ref kpdAIn053_High, ref pt053, kpdAIn053_Low.Text); }

                else if (tbx.Name == "kpdAIn054_Low") { UpdateLow(ref kpdAIn054_Low, ref pt054, kpdAIn054_High.Text); }
                else if (tbx.Name == "kpdAIn054_High") { UpdateHigh(ref kpdAIn054_High, ref pt054, kpdAIn054_Low.Text); }

                else if (tbx.Name == "kpdAIn055_Low") { UpdateLow(ref kpdAIn055_Low, ref pt055, kpdAIn055_High.Text); }
                else if (tbx.Name == "kpdAIn055_High") { UpdateHigh(ref kpdAIn055_High, ref pt055, kpdAIn055_Low.Text); }

                else if (tbx.Name == "kpdTemp000_Low") { UpdateLow(ref kpdTemp000_Low, ref tmp000, kpdTemp000_High.Text); }
                else if (tbx.Name == "kpdTemp000_High") { UpdateHigh(ref kpdTemp000_High, ref tmp000, kpdTemp000_Low.Text); }
                else if (tbx.Name == "kpdTemp000_Offset") { tmp000[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp001_Low") { UpdateLow(ref kpdTemp001_Low, ref tmp001, kpdTemp001_High.Text); }
                else if (tbx.Name == "kpdTemp001_High") { UpdateHigh(ref kpdTemp001_High, ref tmp001, kpdTemp001_Low.Text); }
                else if (tbx.Name == "kpdTemp001_Offset") { tmp001[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp002_Low") { UpdateLow(ref kpdTemp002_Low, ref tmp002, kpdTemp002_High.Text); }
                else if (tbx.Name == "kpdTemp002_High") { UpdateHigh(ref kpdTemp002_High, ref tmp002, kpdTemp002_Low.Text); }
                else if (tbx.Name == "kpdTemp002_Offset") { tmp002[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp003_Low") { UpdateLow(ref kpdTemp003_Low, ref tmp003, kpdTemp003_High.Text); }
                else if (tbx.Name == "kpdTemp003_High") { UpdateHigh(ref kpdTemp003_High, ref tmp003, kpdTemp003_Low.Text); }
                else if (tbx.Name == "kpdTemp003_Offset") { tmp003[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp004_Low") { UpdateLow(ref kpdTemp004_Low, ref tmp004, kpdTemp004_High.Text); }
                else if (tbx.Name == "kpdTemp004_High") { UpdateHigh(ref kpdTemp004_High, ref tmp004, kpdTemp004_Low.Text); }
                else if (tbx.Name == "kpdTemp004_Offset") { tmp004[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp005_Low") { UpdateLow(ref kpdTemp005_Low, ref tmp005, kpdTemp005_High.Text); }
                else if (tbx.Name == "kpdTemp005_High") { UpdateHigh(ref kpdTemp005_High, ref tmp005, kpdTemp005_Low.Text); }
                else if (tbx.Name == "kpdTemp005_Offset") { tmp005[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp006_Low") { UpdateLow(ref kpdTemp006_Low, ref tmp006, kpdTemp006_High.Text); }
                else if (tbx.Name == "kpdTemp006_High") { UpdateHigh(ref kpdTemp006_High, ref tmp006, kpdTemp006_Low.Text); }
                else if (tbx.Name == "kpdTemp006_Offset") { tmp006[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp007_Low") { UpdateLow(ref kpdTemp007_Low, ref tmp007, kpdTemp007_High.Text); }
                else if (tbx.Name == "kpdTemp007_High") { UpdateHigh(ref kpdTemp007_High, ref tmp007, kpdTemp007_Low.Text); }
                else if (tbx.Name == "kpdTemp007_Offset") { tmp007[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp008_Low") { UpdateLow(ref kpdTemp008_Low, ref tmp008, kpdTemp008_High.Text); }
                else if (tbx.Name == "kpdTemp008_High") { UpdateHigh(ref kpdTemp008_High, ref tmp008, kpdTemp008_Low.Text); }
                else if (tbx.Name == "kpdTemp008_Offset") { tmp008[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp009_Low") { UpdateLow(ref kpdTemp009_Low, ref tmp009, kpdTemp009_High.Text); }
                else if (tbx.Name == "kpdTemp009_High") { UpdateHigh(ref kpdTemp009_High, ref tmp009, kpdTemp009_Low.Text); }
                else if (tbx.Name == "kpdTemp009_Offset") { tmp009[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp010_Low") { UpdateLow(ref kpdTemp010_Low, ref tmp010, kpdTemp010_High.Text); }
                else if (tbx.Name == "kpdTemp010_High") { UpdateHigh(ref kpdTemp010_High, ref tmp010, kpdTemp010_Low.Text); }
                else if (tbx.Name == "kpdTemp010_Offset") { tmp010[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp011_Low") { UpdateLow(ref kpdTemp011_Low, ref tmp011, kpdTemp011_High.Text); }
                else if (tbx.Name == "kpdTemp011_High") { UpdateHigh(ref kpdTemp011_High, ref tmp011, kpdTemp011_Low.Text); }
                else if (tbx.Name == "kpdTemp011_Offset") { tmp011[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp012_Low") { UpdateLow(ref kpdTemp012_Low, ref tmp012, kpdTemp012_High.Text); }
                else if (tbx.Name == "kpdTemp012_High") { UpdateHigh(ref kpdTemp012_High, ref tmp012, kpdTemp012_Low.Text); }
                else if (tbx.Name == "kpdTemp012_Offset") { tmp012[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp013_Low") { UpdateLow(ref kpdTemp013_Low, ref tmp013, kpdTemp013_High.Text); }
                else if (tbx.Name == "kpdTemp013_High") { UpdateHigh(ref kpdTemp013_High, ref tmp013, kpdTemp013_Low.Text); }
                else if (tbx.Name == "kpdTemp013_Offset") { tmp013[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp014_Low") { UpdateLow(ref kpdTemp014_Low, ref tmp014, kpdTemp014_High.Text); }
                else if (tbx.Name == "kpdTemp014_High") { UpdateHigh(ref kpdTemp014_High, ref tmp014, kpdTemp014_Low.Text); }
                else if (tbx.Name == "kpdTemp014_Offset") { tmp014[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp015_Low") { UpdateLow(ref kpdTemp015_Low, ref tmp015, kpdTemp015_High.Text); }
                else if (tbx.Name == "kpdTemp015_High") { UpdateHigh(ref kpdTemp015_High, ref tmp015, kpdTemp015_Low.Text); }
                else if (tbx.Name == "kpdTemp015_Offset") { tmp015[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp016_Low") { UpdateLow(ref kpdTemp016_Low, ref tmp016, kpdTemp016_High.Text); }
                else if (tbx.Name == "kpdTemp016_High") { UpdateHigh(ref kpdTemp016_High, ref tmp016, kpdTemp016_Low.Text); }
                else if (tbx.Name == "kpdTemp016_Offset") { tmp016[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp017_Low") { UpdateLow(ref kpdTemp017_Low, ref tmp017, kpdTemp017_High.Text); }
                else if (tbx.Name == "kpdTemp017_High") { UpdateHigh(ref kpdTemp017_High, ref tmp017, kpdTemp017_Low.Text); }
                else if (tbx.Name == "kpdTemp017_Offset") { tmp017[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp018_Low") { UpdateLow(ref kpdTemp018_Low, ref tmp018, kpdTemp018_High.Text); }
                else if (tbx.Name == "kpdTemp018_High") { UpdateHigh(ref kpdTemp018_High, ref tmp018, kpdTemp018_Low.Text); }
                else if (tbx.Name == "kpdTemp018_Offset") { tmp018[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp019_Low") { UpdateLow(ref kpdTemp019_Low, ref tmp019, kpdTemp019_High.Text); }
                else if (tbx.Name == "kpdTemp019_High") { UpdateHigh(ref kpdTemp019_High, ref tmp019, kpdTemp019_Low.Text); }
                else if (tbx.Name == "kpdTemp019_Offset") { tmp019[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp020_Low") { UpdateLow(ref kpdTemp020_Low, ref tmp020, kpdTemp020_High.Text); }
                else if (tbx.Name == "kpdTemp020_High") { UpdateHigh(ref kpdTemp020_High, ref tmp020, kpdTemp020_Low.Text); }
                else if (tbx.Name == "kpdTemp020_Offset") { tmp020[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp021_Low") { UpdateLow(ref kpdTemp021_Low, ref tmp021, kpdTemp021_High.Text); }
                else if (tbx.Name == "kpdTemp021_High") { UpdateHigh(ref kpdTemp021_High, ref tmp021, kpdTemp021_Low.Text); }
                else if (tbx.Name == "kpdTemp021_Offset") { tmp021[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp022_Low") { UpdateLow(ref kpdTemp022_Low, ref tmp022, kpdTemp022_High.Text); }
                else if (tbx.Name == "kpdTemp022_High") { UpdateHigh(ref kpdTemp022_High, ref tmp022, kpdTemp022_Low.Text); }
                else if (tbx.Name == "kpdTemp022_Offset") { tmp022[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp023_Low") { UpdateLow(ref kpdTemp023_Low, ref tmp023, kpdTemp023_High.Text); }
                else if (tbx.Name == "kpdTemp023_High") { UpdateHigh(ref kpdTemp023_High, ref tmp023, kpdTemp023_Low.Text); }
                else if (tbx.Name == "kpdTemp023_Offset") { tmp023[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp024_Low") { UpdateLow(ref kpdTemp024_Low, ref tmp024, kpdTemp024_High.Text); }
                else if (tbx.Name == "kpdTemp024_High") { UpdateHigh(ref kpdTemp024_High, ref tmp024, kpdTemp024_Low.Text); }
                else if (tbx.Name == "kpdTemp024_Offset") { tmp024[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp025_Low") { UpdateLow(ref kpdTemp025_Low, ref tmp025, kpdTemp025_High.Text); }
                else if (tbx.Name == "kpdTemp025_High") { UpdateHigh(ref kpdTemp025_High, ref tmp025, kpdTemp025_Low.Text); }
                else if (tbx.Name == "kpdTemp025_Offset") { tmp025[7] = tbx.Text; }

                else if (tbx.Name == "kpdTemp026_Low") { UpdateLow(ref kpdTemp026_Low, ref tmp026, kpdTemp026_High.Text); }
                else if (tbx.Name == "kpdTemp026_High") { UpdateHigh(ref kpdTemp026_High, ref tmp026, kpdTemp026_Low.Text); }
                else if (tbx.Name == "kpdTemp026_Offset") { tmp026[7] = tbx.Text; }

                else if (tbx.Name == "kpdAOut001_Low") { UpdateLow(ref kpdAOut001_Low, ref aout001, kpdAOut001_High.Text); }
                else if (tbx.Name == "kpdAOut001_High") { UpdateHigh(ref kpdAOut001_High, ref aout001, kpdAOut001_Low.Text); }

                else if (tbx.Name == "kpdAOut002_Low") { UpdateLow(ref kpdAOut002_Low, ref aout002, kpdAOut002_High.Text); }
                else if (tbx.Name == "kpdAOut002_High") { UpdateHigh(ref kpdAOut002_High, ref aout002, kpdAOut002_Low.Text); }

                else if (tbx.Name == "kpdAOut003_Low") { UpdateLow(ref kpdAOut003_Low, ref aout003, kpdAOut003_High.Text); }
                else if (tbx.Name == "kpdAOut003_High") { UpdateHigh(ref kpdAOut003_High, ref aout003, kpdAOut003_Low.Text); }

                else if (tbx.Name == "kpdAOut004_Low") { UpdateLow(ref kpdAOut004_Low, ref aout004, kpdAOut004_High.Text); }
                else if (tbx.Name == "kpdAOut004_High") { UpdateHigh(ref kpdAOut004_High, ref aout004, kpdAOut004_Low.Text); }

                else if (tbx.Name == "kpdAOut005_Low") { UpdateLow(ref kpdAOut005_Low, ref aout005, kpdAOut005_High.Text); }
                else if (tbx.Name == "kpdAOut005_High") { UpdateHigh(ref kpdAOut005_High, ref aout005, kpdAOut005_Low.Text); }

                else if (tbx.Name == "kpdAOut006_Low") { UpdateLow(ref kpdAOut006_Low, ref aout006, kpdAOut006_High.Text); }
                else if (tbx.Name == "kpdAOut006_High") { UpdateHigh(ref kpdAOut006_High, ref aout006, kpdAOut006_Low.Text); }

                btnSavePoints_Enabled = ValuesChanged();
            }
        }

        private void UpdateLow(ref Calvin.Touch.TextBoxKeyboard low, ref string[] pt, string high)
        {
            if (Convert.ToSingle(low.Text) < Convert.ToSingle(high)) { pt[4] = low.Text; }
            App.bKpdCheckOn = false;
            low.Text = Units.strFormat(pt[4], pt[3]);
            App.bKpdCheckOn = true;
        }

        private void UpdateHigh(ref Calvin.Touch.TextBoxKeyboard high, ref string[] pt, string low)
        {
            if (Convert.ToSingle(high.Text) > Convert.ToSingle(low)) { pt[5] = high.Text; }
            App.bKpdCheckOn = false;
            high.Text = Units.strFormat(pt[5], pt[3]);
            App.bKpdCheckOn = true;
        }

        private void cmbUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!bConfigOn)
            {
                cmb = sender as ComboBox;
                string s = cmb.SelectedIndex.ToString();
                if (cmb.Name == "cmbAIn000_Units") { pt000[2] = s; }
                else if (cmb.Name == "cmbAIn001_Units") { pt001[2] = s; }
                else if (cmb.Name == "cmbAIn002_Units") { pt002[2] = s; }
                else if (cmb.Name == "cmbAIn003_Units") { pt003[2] = s; }
                else if (cmb.Name == "cmbAIn004_Units") { pt004[2] = s; }
                else if (cmb.Name == "cmbAIn005_Units") { pt005[2] = s; }
                else if (cmb.Name == "cmbAIn006_Units") { pt006[2] = s; }
                else if (cmb.Name == "cmbAIn007_Units") { pt007[2] = s; }
                else if (cmb.Name == "cmbAIn008_Units") { pt008[2] = s; }
                else if (cmb.Name == "cmbAIn009_Units") { pt009[2] = s; }
                else if (cmb.Name == "cmbAIn010_Units") { pt010[2] = s; }
                else if (cmb.Name == "cmbAIn011_Units") { pt011[2] = s; }
                else if (cmb.Name == "cmbAIn012_Units") { pt012[2] = s; }
                else if (cmb.Name == "cmbAIn013_Units") { pt013[2] = s; }
                else if (cmb.Name == "cmbAIn014_Units") { pt014[2] = s; }
                else if (cmb.Name == "cmbAIn015_Units") { pt015[2] = s; }
                else if (cmb.Name == "cmbAIn016_Units") { pt016[2] = s; }
                else if (cmb.Name == "cmbAIn017_Units") { pt017[2] = s; }
                else if (cmb.Name == "cmbAIn018_Units") { pt018[2] = s; }
                else if (cmb.Name == "cmbAIn019_Units") { pt019[2] = s; }
                else if (cmb.Name == "cmbAIn020_Units") { pt020[2] = s; }
                else if (cmb.Name == "cmbAIn021_Units") { pt021[2] = s; }
                else if (cmb.Name == "cmbAIn022_Units") { pt022[2] = s; }
                else if (cmb.Name == "cmbAIn023_Units") { pt023[2] = s; }
                else if (cmb.Name == "cmbAIn024_Units") { pt024[2] = s; }
                else if (cmb.Name == "cmbAIn025_Units") { pt025[2] = s; }
                else if (cmb.Name == "cmbAIn026_Units") { pt026[2] = s; }
                else if (cmb.Name == "cmbAIn027_Units") { pt027[2] = s; }
                else if (cmb.Name == "cmbAIn028_Units") { pt028[2] = s; }

                else if (cmb.Name == "cmbAIn040_Units") { pt040[2] = s; }
                else if (cmb.Name == "cmbAIn041_Units") { pt041[2] = s; }
                else if (cmb.Name == "cmbAIn042_Units") { pt042[2] = s; }
                else if (cmb.Name == "cmbAIn043_Units") { pt043[2] = s; }
                else if (cmb.Name == "cmbAIn044_Units") { pt044[2] = s; }
                else if (cmb.Name == "cmbAIn045_Units") { pt045[2] = s; }
                else if (cmb.Name == "cmbAIn046_Units") { pt046[2] = s; }

                btnSavePoints_Enabled = ValuesChanged();
            }
        }

        private void cmbDecimals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!bConfigOn)
            {
                cmb = sender as ComboBox;
                string s = cmb.SelectedItem.ToString();
                if (cmb.Name == "cmbAIn000_Decimals") { pt000[3] = s; }
                else if (cmb.Name == "cmbAIn001_Decimals") { pt001[3] = s; }
                else if (cmb.Name == "cmbAIn002_Decimals") { pt002[3] = s; }
                else if (cmb.Name == "cmbAIn003_Decimals") { pt003[3] = s; }
                else if (cmb.Name == "cmbAIn004_Decimals") { pt004[3] = s; }
                else if (cmb.Name == "cmbAIn005_Decimals") { pt005[3] = s; }
                else if (cmb.Name == "cmbAIn006_Decimals") { pt006[3] = s; }
                else if (cmb.Name == "cmbAIn007_Decimals") { pt007[3] = s; }
                else if (cmb.Name == "cmbAIn008_Decimals") { pt008[3] = s; }
                else if (cmb.Name == "cmbAIn009_Decimals") { pt009[3] = s; }
                else if (cmb.Name == "cmbAIn010_Decimals") { pt010[3] = s; }
                else if (cmb.Name == "cmbAIn011_Decimals") { pt011[3] = s; }
                else if (cmb.Name == "cmbAIn012_Decimals") { pt012[3] = s; }
                else if (cmb.Name == "cmbAIn013_Decimals") { pt013[3] = s; }
                else if (cmb.Name == "cmbAIn014_Decimals") { pt014[3] = s; }
                else if (cmb.Name == "cmbAIn015_Decimals") { pt015[3] = s; }
                else if (cmb.Name == "cmbAIn016_Decimals") { pt016[3] = s; }
                else if (cmb.Name == "cmbAIn017_Decimals") { pt017[3] = s; }
                else if (cmb.Name == "cmbAIn018_Decimals") { pt018[3] = s; }
                else if (cmb.Name == "cmbAIn019_Decimals") { pt019[3] = s; }
                else if (cmb.Name == "cmbAIn020_Decimals") { pt020[3] = s; }
                else if (cmb.Name == "cmbAIn021_Decimals") { pt021[3] = s; }
                else if (cmb.Name == "cmbAIn022_Decimals") { pt022[3] = s; }
                else if (cmb.Name == "cmbAIn023_Decimals") { pt023[3] = s; }
                else if (cmb.Name == "cmbAIn024_Decimals") { pt024[3] = s; }
                else if (cmb.Name == "cmbAIn025_Decimals") { pt025[3] = s; }
                else if (cmb.Name == "cmbAIn026_Decimals") { pt026[3] = s; }
                else if (cmb.Name == "cmbAIn027_Decimals") { pt027[3] = s; }
                else if (cmb.Name == "cmbAIn028_Decimals") { pt028[3] = s; }

                else if (cmb.Name == "cmbAIn040_Decimals") { pt040[3] = s; }
                else if (cmb.Name == "cmbAIn041_Decimals") { pt041[3] = s; }
                else if (cmb.Name == "cmbAIn042_Decimals") { pt042[3] = s; }
                else if (cmb.Name == "cmbAIn043_Decimals") { pt043[3] = s; }
                else if (cmb.Name == "cmbAIn044_Decimals") { pt044[3] = s; }
                else if (cmb.Name == "cmbAIn045_Decimals") { pt045[3] = s; }
                else if (cmb.Name == "cmbAIn046_Decimals") { pt046[3] = s; }
                ResetDecimals();
                btnSavePoints_Enabled = ValuesChanged();
            }
        }

        private bool ValuesChanged()
        {
            if (ArrayValuesChanged(pt000, ADS.AIn000)) { return true; }
            else if (ArrayValuesChanged(pt001, ADS.AIn001)) { return true; }
            else if (ArrayValuesChanged(pt002, ADS.AIn002)) { return true; }
            else if (ArrayValuesChanged(pt003, ADS.AIn003)) { return true; }
            else if (ArrayValuesChanged(pt004, ADS.AIn004)) { return true; }
            else if (ArrayValuesChanged(pt005, ADS.AIn005)) { return true; }
            else if (ArrayValuesChanged(pt006, ADS.AIn006)) { return true; }
            else if (ArrayValuesChanged(pt007, ADS.AIn007)) { return true; }
            else if (ArrayValuesChanged(pt008, ADS.AIn008)) { return true; }
            else if (ArrayValuesChanged(pt009, ADS.AIn009)) { return true; }
            else if (ArrayValuesChanged(pt010, ADS.AIn010)) { return true; }
            else if (ArrayValuesChanged(pt011, ADS.AIn011)) { return true; }
            else if (ArrayValuesChanged(pt012, ADS.AIn012)) { return true; }
            else if (ArrayValuesChanged(pt013, ADS.AIn013)) { return true; }
            else if (ArrayValuesChanged(pt014, ADS.AIn014)) { return true; }
            else if (ArrayValuesChanged(pt015, ADS.AIn015)) { return true; }
            else if (ArrayValuesChanged(pt016, ADS.AIn016)) { return true; }
            else if (ArrayValuesChanged(pt017, ADS.AIn017)) { return true; }
            else if (ArrayValuesChanged(pt018, ADS.AIn018)) { return true; }
            else if (ArrayValuesChanged(pt019, ADS.AIn019)) { return true; }
            else if (ArrayValuesChanged(pt020, ADS.AIn020)) { return true; }
            else if (ArrayValuesChanged(pt021, ADS.AIn021)) { return true; }
            else if (ArrayValuesChanged(pt022, ADS.AIn022)) { return true; }
            else if (ArrayValuesChanged(pt023, ADS.AIn023)) { return true; }
            else if (ArrayValuesChanged(pt024, ADS.AIn024)) { return true; }
            else if (ArrayValuesChanged(pt025, ADS.AIn025)) { return true; }
            else if (ArrayValuesChanged(pt026, ADS.AIn026)) { return true; }
            else if (ArrayValuesChanged(pt027, ADS.AIn027)) { return true; }
            else if (ArrayValuesChanged(pt028, ADS.AIn028)) { return true; }
            else if (ArrayValuesChanged(pt040, ADS.AIn040)) { return true; }
            else if (ArrayValuesChanged(pt041, ADS.AIn041)) { return true; }
            else if (ArrayValuesChanged(pt042, ADS.AIn042)) { return true; }
            else if (ArrayValuesChanged(pt043, ADS.AIn043)) { return true; }
            else if (ArrayValuesChanged(pt044, ADS.AIn044)) { return true; }
            else if (ArrayValuesChanged(pt045, ADS.AIn045)) { return true; }
            else if (ArrayValuesChanged(pt046, ADS.AIn046)) { return true; }
            else if (ArrayValuesChanged(pt050, ADS.AIn050)) { return true; }
            else if (ArrayValuesChanged(pt051, ADS.AIn051)) { return true; }
            else if (ArrayValuesChanged(pt052, ADS.AIn052)) { return true; }
            else if (ArrayValuesChanged(pt053, ADS.AIn053)) { return true; }
            else if (ArrayValuesChanged(pt054, ADS.AIn054)) { return true; }
            else if (ArrayValuesChanged(pt055, ADS.AIn055)) { return true; }

            else if (ArrayValuesChanged(tmp000, ADS.Temp000)) { return true; }
            else if (ArrayValuesChanged(tmp001, ADS.Temp001)) { return true; }
            else if (ArrayValuesChanged(tmp002, ADS.Temp002)) { return true; }
            else if (ArrayValuesChanged(tmp003, ADS.Temp003)) { return true; }
            else if (ArrayValuesChanged(tmp004, ADS.Temp004)) { return true; }
            else if (ArrayValuesChanged(tmp005, ADS.Temp005)) { return true; }
            else if (ArrayValuesChanged(tmp006, ADS.Temp006)) { return true; }
            else if (ArrayValuesChanged(tmp007, ADS.Temp007)) { return true; }
            else if (ArrayValuesChanged(tmp008, ADS.Temp008)) { return true; }
            else if (ArrayValuesChanged(tmp009, ADS.Temp009)) { return true; }
            else if (ArrayValuesChanged(tmp010, ADS.Temp010)) { return true; }
            else if (ArrayValuesChanged(tmp011, ADS.Temp011)) { return true; }
            else if (ArrayValuesChanged(tmp012, ADS.Temp012)) { return true; }
            else if (ArrayValuesChanged(tmp013, ADS.Temp013)) { return true; }
            else if (ArrayValuesChanged(tmp014, ADS.Temp014)) { return true; }
            else if (ArrayValuesChanged(tmp015, ADS.Temp015)) { return true; }
            else if (ArrayValuesChanged(tmp016, ADS.Temp016)) { return true; }
            else if (ArrayValuesChanged(tmp017, ADS.Temp017)) { return true; }
            else if (ArrayValuesChanged(tmp018, ADS.Temp018)) { return true; }
            else if (ArrayValuesChanged(tmp019, ADS.Temp019)) { return true; }
            else if (ArrayValuesChanged(tmp020, ADS.Temp020)) { return true; }
            else if (ArrayValuesChanged(tmp021, ADS.Temp021)) { return true; }
            else if (ArrayValuesChanged(tmp022, ADS.Temp022)) { return true; }
            else if (ArrayValuesChanged(tmp023, ADS.Temp023)) { return true; }
            else if (ArrayValuesChanged(tmp024, ADS.Temp024)) { return true; }
            else if (ArrayValuesChanged(tmp025, ADS.Temp025)) { return true; }
            else if (ArrayValuesChanged(tmp026, ADS.Temp026)) { return true; }

            else if (ArrayValuesChanged(aout000, ADS.AOut000)) { return true; }
            else if (ArrayValuesChanged(aout000RPM, ADS.AOut000RPM)) { return true; }
            else if (ArrayValuesChanged(aout001, ADS.AOut001)) { return true; }
            else if (ArrayValuesChanged(aout002, ADS.AOut002)) { return true; }
            else if (ArrayValuesChanged(aout003, ADS.AOut003)) { return true; }
            else if (ArrayValuesChanged(aout004, ADS.AOut004)) { return true; }
            else if (ArrayValuesChanged(aout005, ADS.AOut005)) { return true; }
            else if (ArrayValuesChanged(aout006, ADS.AOut006)) { return true; }
            else if (ArrayValuesChanged(aout007, ADS.AOut007)) { return true; }
            else if (ArrayValuesChanged(aout008, ADS.AOut008)) { return true; }
            else if (ArrayValuesChanged(aout009, ADS.AOut009)) { return true; }
            else if (ArrayValuesChanged(aout010, ADS.AOut010)) { return true; }

            else if (ArrayValuesChanged(do000, ADS.DO000)) { return true; }
            else if (ArrayValuesChanged(do001, ADS.DO001)) { return true; }
            else if (ArrayValuesChanged(do002, ADS.DO002)) { return true; }
            else if (ArrayValuesChanged(do003, ADS.DO003)) { return true; }
            else if (ArrayValuesChanged(do004, ADS.DO004)) { return true; }
            else if (ArrayValuesChanged(do005, ADS.DO005)) { return true; }
            else if (ArrayValuesChanged(do006, ADS.DO006)) { return true; }
            else if (ArrayValuesChanged(do007, ADS.DO007)) { return true; }
            else if (ArrayValuesChanged(do008, ADS.DO008)) { return true; }
            else if (ArrayValuesChanged(do009, ADS.DO009)) { return true; }
            else if (ArrayValuesChanged(do010, ADS.DO010)) { return true; }
            else if (ArrayValuesChanged(do011, ADS.DO011)) { return true; }
            else if (ArrayValuesChanged(do012, ADS.DO012)) { return true; }
            else if (ArrayValuesChanged(do013, ADS.DO013)) { return true; }
            else if (ArrayValuesChanged(do014, ADS.DO014)) { return true; }
            else if (ArrayValuesChanged(do015, ADS.DO015)) { return true; }
            else if (ArrayValuesChanged(do016, ADS.DO016)) { return true; }
            else if (ArrayValuesChanged(do017, ADS.DO017)) { return true; }
            else if (ArrayValuesChanged(do018, ADS.DO018)) { return true; }
            else if (ArrayValuesChanged(do019, ADS.DO019)) { return true; }
            else if (ArrayValuesChanged(do020, ADS.DO020)) { return true; }
            else if (ArrayValuesChanged(do021, ADS.DO021)) { return true; }
            else if (ArrayValuesChanged(do022, ADS.DO022)) { return true; }                             // 09/20/23 PS
            return false;
        }

        private bool ArrayValuesChanged(string[] a1, string[] a2)
        {
            bool changed = false;
            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i])
                {
                    changed = true;
                    break;
                }
            }
            return changed;
        }

        #endregion Point Entries

        #region Configuration

        private void LoadPointValues()
        {
            for (int i = 0; i < 9; i++)
            {
                pt000[i] = ADS.AIn000[i];
                pt001[i] = ADS.AIn001[i];
                pt002[i] = ADS.AIn002[i];
                pt003[i] = ADS.AIn003[i];
                pt004[i] = ADS.AIn004[i];
                pt005[i] = ADS.AIn005[i];
                pt006[i] = ADS.AIn006[i];
                pt007[i] = ADS.AIn007[i];
                pt008[i] = ADS.AIn008[i];
                pt009[i] = ADS.AIn009[i];
                pt010[i] = ADS.AIn010[i];
                pt011[i] = ADS.AIn011[i];
                pt012[i] = ADS.AIn012[i];
                pt013[i] = ADS.AIn013[i];
                pt014[i] = ADS.AIn014[i];
                pt015[i] = ADS.AIn015[i];
                pt016[i] = ADS.AIn016[i];
                pt017[i] = ADS.AIn017[i];
                pt018[i] = ADS.AIn018[i];
                pt019[i] = ADS.AIn019[i];
                pt020[i] = ADS.AIn020[i];
                pt021[i] = ADS.AIn021[i];
                pt022[i] = ADS.AIn022[i];
                pt023[i] = ADS.AIn023[i];
                pt024[i] = ADS.AIn024[i];
                pt025[i] = ADS.AIn025[i];
                pt026[i] = ADS.AIn026[i];
                pt027[i] = ADS.AIn027[i];
                pt028[i] = ADS.AIn028[i];
                pt040[i] = ADS.AIn040[i];
                pt041[i] = ADS.AIn041[i];
                pt042[i] = ADS.AIn042[i];
                pt043[i] = ADS.AIn043[i];
                pt044[i] = ADS.AIn044[i];
                pt045[i] = ADS.AIn045[i];
                pt046[i] = ADS.AIn046[i];
                pt050[i] = ADS.AIn050[i];
                pt051[i] = ADS.AIn051[i];
                pt052[i] = ADS.AIn052[i];
                pt053[i] = ADS.AIn053[i];
                pt054[i] = ADS.AIn054[i];
                pt055[i] = ADS.AIn055[i];
            }
            for (int i = 0; i < 8; i++)
            {
                tmp000[i] = ADS.Temp000[i];
                tmp001[i] = ADS.Temp001[i];
                tmp002[i] = ADS.Temp002[i];
                tmp003[i] = ADS.Temp003[i];
                tmp004[i] = ADS.Temp004[i];
                tmp005[i] = ADS.Temp005[i];
                tmp006[i] = ADS.Temp006[i];
                tmp007[i] = ADS.Temp007[i];
                tmp008[i] = ADS.Temp008[i];
                tmp009[i] = ADS.Temp009[i];
                tmp010[i] = ADS.Temp010[i];
                tmp011[i] = ADS.Temp011[i];
                tmp012[i] = ADS.Temp012[i];
                tmp013[i] = ADS.Temp013[i];
                tmp014[i] = ADS.Temp014[i];
                tmp015[i] = ADS.Temp015[i];
                tmp016[i] = ADS.Temp016[i];
                tmp017[i] = ADS.Temp017[i];
                tmp018[i] = ADS.Temp018[i];
                tmp019[i] = ADS.Temp019[i];
                tmp020[i] = ADS.Temp020[i];
                tmp021[i] = ADS.Temp021[i];
                tmp022[i] = ADS.Temp022[i];
                tmp023[i] = ADS.Temp023[i];
                tmp024[i] = ADS.Temp024[i];
                tmp025[i] = ADS.Temp025[i];
                tmp026[i] = ADS.Temp026[i];
                aout000[i] = ADS.AOut000[i];
                aout000RPM[i] = ADS.AOut000RPM[i];
                aout001[i] = ADS.AOut001[i];
                aout002[i] = ADS.AOut002[i];
                aout003[i] = ADS.AOut003[i];
                aout004[i] = ADS.AOut004[i];
                aout005[i] = ADS.AOut005[i];
                aout006[i] = ADS.AOut006[i];
                aout007[i] = ADS.AOut007[i];
                aout008[i] = ADS.AOut008[i];
                aout009[i] = ADS.AOut009[i];
                aout010[i] = ADS.AOut010[i];
            }

            kpdAIn000_Alias.Text = pt000[1];
            cmbAIn000_Units.SelectedIndex = Convert.ToInt32(pt000[2]);
            kpdAIn000_Min.Text = Units.strFormat(pt000[6], iMinDec);
            kpdAIn000_Offset.Text = Units.strFormat(pt000[7], iMinDec);

            kpdAIn001_Alias.Text = pt001[1];
            cmbAIn001_Units.SelectedIndex = Convert.ToInt32(pt001[2]);
            kpdAIn001_Min.Text = Units.strFormat(pt001[6], iMinDec);
            kpdAIn001_Offset.Text = Units.strFormat(pt001[7], iMinDec);

            kpdAIn002_Alias.Text = pt002[1];
            cmbAIn002_Units.SelectedIndex = Convert.ToInt32(pt002[2]);
            kpdAIn002_Min.Text = Units.strFormat(pt002[6], iMinDec);
            kpdAIn002_Offset.Text = Units.strFormat(pt002[7], iMinDec);

            kpdAIn003_Alias.Text = pt003[1];
            cmbAIn003_Units.SelectedIndex = Convert.ToInt32(pt003[2]);
            kpdAIn003_Min.Text = Units.strFormat(pt003[6], iMinDec);
            kpdAIn003_Offset.Text = Units.strFormat(pt003[7], iMinDec);

            kpdAIn004_Alias.Text = pt004[1];
            cmbAIn004_Units.SelectedIndex = Convert.ToInt32(pt004[2]);
            kpdAIn004_Min.Text = Units.strFormat(pt004[6], iMinDec);
            kpdAIn004_Offset.Text = Units.strFormat(pt004[7], iMinDec);

            kpdAIn005_Alias.Text = pt005[1];
            cmbAIn005_Units.SelectedIndex = Convert.ToInt32(pt005[2]);
            kpdAIn005_Min.Text = Units.strFormat(pt005[6], iMinDec);
            kpdAIn005_Offset.Text = Units.strFormat(pt005[7], iMinDec);

            kpdAIn006_Alias.Text = pt006[1];
            cmbAIn006_Units.SelectedIndex = Convert.ToInt32(pt006[2]);
            kpdAIn006_Min.Text = Units.strFormat(pt006[6], iMinDec);
            kpdAIn006_Offset.Text = Units.strFormat(pt006[7], iMinDec);

            kpdAIn007_Alias.Text = pt007[1];
            cmbAIn007_Units.SelectedIndex = Convert.ToInt32(pt007[2]);
            kpdAIn007_Min.Text = Units.strFormat(pt007[6], iMinDec);
            kpdAIn007_Offset.Text = Units.strFormat(pt007[7], iMinDec);

            kpdAIn008_Alias.Text = pt008[1];
            cmbAIn008_Units.SelectedIndex = Convert.ToInt32(pt008[2]);
            kpdAIn008_Min.Text = Units.strFormat(pt008[6], iMinDec);
            kpdAIn008_Offset.Text = Units.strFormat(pt008[7], iMinDec);

            kpdAIn009_Alias.Text = pt009[1];
            cmbAIn009_Units.SelectedIndex = Convert.ToInt32(pt009[2]);
            kpdAIn009_Min.Text = Units.strFormat(pt009[6], iMinDec);
            kpdAIn009_Offset.Text = Units.strFormat(pt009[7], iMinDec);

            kpdAIn010_Alias.Text = pt010[1];
            cmbAIn010_Units.SelectedIndex = Convert.ToInt32(pt010[2]);
            kpdAIn010_Min.Text = Units.strFormat(pt010[6], iMinDec);
            kpdAIn010_Offset.Text = Units.strFormat(pt010[7], iMinDec);

            kpdAIn011_Alias.Text = pt011[1];
            cmbAIn011_Units.SelectedIndex = Convert.ToInt32(pt011[2]);
            kpdAIn011_Min.Text = Units.strFormat(pt011[6], iMinDec);
            kpdAIn011_Offset.Text = Units.strFormat(pt011[7], iMinDec);

            kpdAIn012_Alias.Text = pt012[1];
            cmbAIn012_Units.SelectedIndex = Convert.ToInt32(pt012[2]);
            kpdAIn012_Min.Text = Units.strFormat(pt012[6], iMinDec);
            kpdAIn012_Offset.Text = Units.strFormat(pt012[7], iMinDec);

            kpdAIn013_Alias.Text = pt013[1];
            cmbAIn013_Units.SelectedIndex = Convert.ToInt32(pt013[2]);
            kpdAIn013_Min.Text = Units.strFormat(pt013[6], iMinDec);
            kpdAIn013_Offset.Text = Units.strFormat(pt013[7], iMinDec);

            kpdAIn014_Alias.Text = pt014[1];
            cmbAIn014_Units.SelectedIndex = Convert.ToInt32(pt014[2]);
            kpdAIn014_Min.Text = Units.strFormat(pt014[6], iMinDec);
            kpdAIn014_Offset.Text = Units.strFormat(pt014[7], iMinDec);

            kpdAIn015_Alias.Text = pt015[1];
            cmbAIn015_Units.SelectedIndex = Convert.ToInt32(pt015[2]);
            kpdAIn015_Min.Text = Units.strFormat(pt015[6], iMinDec);
            kpdAIn015_Offset.Text = Units.strFormat(pt015[7], iMinDec);

            kpdAIn016_Alias.Text = pt016[1];
            cmbAIn016_Units.SelectedIndex = Convert.ToInt32(pt016[2]);
            kpdAIn016_Min.Text = Units.strFormat(pt016[6], iMinDec);
            kpdAIn016_Offset.Text = Units.strFormat(pt016[7], iMinDec);

            kpdAIn017_Alias.Text = pt017[1];
            cmbAIn017_Units.SelectedIndex = Convert.ToInt32(pt017[2]);
            kpdAIn017_Min.Text = Units.strFormat(pt017[6], iMinDec);
            kpdAIn017_Offset.Text = Units.strFormat(pt017[7], iMinDec);

            kpdAIn018_Alias.Text = pt018[1];
            cmbAIn018_Units.SelectedIndex = Convert.ToInt32(pt018[2]);
            kpdAIn018_Min.Text = Units.strFormat(pt018[6], iMinDec);
            kpdAIn018_Offset.Text = Units.strFormat(pt018[7], iMinDec);

            kpdAIn019_Alias.Text = pt019[1];
            cmbAIn019_Units.SelectedIndex = Convert.ToInt32(pt019[2]);
            kpdAIn019_Min.Text = Units.strFormat(pt019[6], iMinDec);
            kpdAIn019_Offset.Text = Units.strFormat(pt019[7], iMinDec);

            kpdAIn020_Alias.Text = pt020[1];
            cmbAIn020_Units.SelectedIndex = Convert.ToInt32(pt020[2]);
            kpdAIn020_Min.Text = Units.strFormat(pt020[6], iMinDec);
            kpdAIn020_Offset.Text = Units.strFormat(pt020[7], iMinDec);

            kpdAIn021_Alias.Text = pt021[1];
            cmbAIn021_Units.SelectedIndex = Convert.ToInt32(pt021[2]);
            kpdAIn021_Min.Text = Units.strFormat(pt021[6], iMinDec);
            kpdAIn021_Offset.Text = Units.strFormat(pt021[7], iMinDec);

            kpdAIn022_Alias.Text = pt022[1];
            cmbAIn022_Units.SelectedIndex = Convert.ToInt32(pt022[2]);
            kpdAIn022_Min.Text = Units.strFormat(pt022[6], iMinDec);
            kpdAIn022_Offset.Text = Units.strFormat(pt022[7], iMinDec);

            kpdAIn023_Alias.Text = pt023[1];
            cmbAIn023_Units.SelectedIndex = Convert.ToInt32(pt023[2]);
            kpdAIn023_Min.Text = Units.strFormat(pt023[6], iMinDec);
            kpdAIn023_Offset.Text = Units.strFormat(pt023[7], iMinDec);

            kpdAIn024_Alias.Text = pt024[1];
            cmbAIn024_Units.SelectedIndex = Convert.ToInt32(pt024[2]);
            kpdAIn024_Min.Text = Units.strFormat(pt024[6], iMinDec);
            kpdAIn024_Offset.Text = Units.strFormat(pt024[7], iMinDec);

            kpdAIn025_Alias.Text = pt025[1];
            cmbAIn025_Units.SelectedIndex = Convert.ToInt32(pt025[2]);
            kpdAIn025_Min.Text = Units.strFormat(pt025[6], iMinDec);
            kpdAIn025_Offset.Text = Units.strFormat(pt025[7], iMinDec);

            kpdAIn026_Alias.Text = pt026[1];
            cmbAIn026_Units.SelectedIndex = Convert.ToInt32(pt026[2]);
            kpdAIn026_Min.Text = Units.strFormat(pt026[6], iMinDec);
            kpdAIn026_Offset.Text = Units.strFormat(pt026[7], iMinDec);

            kpdAIn027_Alias.Text = pt027[1];
            cmbAIn027_Units.SelectedIndex = Convert.ToInt32(pt027[2]);
            kpdAIn027_Min.Text = Units.strFormat(pt027[6], iMinDec);
            kpdAIn027_Offset.Text = Units.strFormat(pt027[7], iMinDec);

            kpdAIn028_Alias.Text = pt028[1];
            cmbAIn028_Units.SelectedIndex = Convert.ToInt32(pt028[2]);
            kpdAIn028_Min.Text = Units.strFormat(pt028[6], iMinDec);
            kpdAIn028_Offset.Text = Units.strFormat(pt028[7], iMinDec);

            kpdAIn040_Alias.Text = pt040[1];
            cmbAIn040_Units.SelectedIndex = Convert.ToInt32(pt040[2]);
            kpdAIn040_Min.Text = Units.strFormat(pt040[6], iMinDec);
            kpdAIn040_Offset.Text = Units.strFormat(pt040[7], iMinDec);

            kpdAIn041_Alias.Text = pt041[1];
            cmbAIn041_Units.SelectedIndex = Convert.ToInt32(pt041[2]);
            kpdAIn041_Min.Text = Units.strFormat(pt041[6], iMinDec);
            kpdAIn041_Offset.Text = Units.strFormat(pt041[7], iMinDec);

            kpdAIn042_Alias.Text = pt042[1];
            cmbAIn042_Units.SelectedIndex = Convert.ToInt32(pt042[2]);
            kpdAIn042_Min.Text = Units.strFormat(pt042[6], iMinDec);
            kpdAIn042_Offset.Text = Units.strFormat(pt042[7], iMinDec);

            kpdAIn043_Alias.Text = pt043[1];
            cmbAIn043_Units.SelectedIndex = Convert.ToInt32(pt043[2]);
            kpdAIn043_Min.Text = Units.strFormat(pt043[6], iMinDec);
            kpdAIn043_Offset.Text = Units.strFormat(pt043[7], iMinDec);

            kpdAIn044_Alias.Text = pt044[1];
            cmbAIn044_Units.SelectedIndex = Convert.ToInt32(pt044[2]);
            kpdAIn044_Min.Text = Units.strFormat(pt044[6], iMinDec);
            kpdAIn044_Offset.Text = Units.strFormat(pt044[7], iMinDec);

            kpdAIn045_Alias.Text = pt045[1];
            cmbAIn045_Units.SelectedIndex = Convert.ToInt32(pt045[2]);
            kpdAIn045_Min.Text = Units.strFormat(pt045[6], iMinDec);
            kpdAIn045_Offset.Text = Units.strFormat(pt045[7], iMinDec);

            kpdAIn046_Alias.Text = pt046[1];
            cmbAIn046_Units.SelectedIndex = Convert.ToInt32(pt046[2]);
            kpdAIn046_Min.Text = Units.strFormat(pt046[6], iMinDec);
            kpdAIn046_Offset.Text = Units.strFormat(pt046[7], iMinDec);

            tbkTemp000_Name.Text = tmp000[0];
            kpdTemp000_Alias.Text = tmp000[1];
            kpdTemp000_Offset.Text = Units.strFormat(tmp000[7], iMinDec);

            tbkTemp001_Name.Text = tmp001[0];
            kpdTemp001_Alias.Text = tmp001[1];
            kpdTemp001_Offset.Text = Units.strFormat(tmp001[7], iMinDec);

            tbkTemp002_Name.Text = tmp002[0];
            kpdTemp002_Alias.Text = tmp002[1];
            kpdTemp002_Offset.Text = Units.strFormat(tmp002[7], iMinDec);

            tbkTemp003_Name.Text = tmp003[0];
            kpdTemp003_Alias.Text = tmp003[1];
            kpdTemp003_Offset.Text = Units.strFormat(tmp003[7], iMinDec);

            tbkTemp004_Name.Text = tmp004[0];
            kpdTemp004_Alias.Text = tmp004[1];
            kpdTemp004_Offset.Text = Units.strFormat(tmp004[7], iMinDec);

            tbkTemp005_Name.Text = tmp005[0];
            kpdTemp005_Alias.Text = tmp005[1];
            kpdTemp005_Offset.Text = Units.strFormat(tmp005[7], iMinDec);

            tbkTemp006_Name.Text = tmp006[0];
            kpdTemp006_Alias.Text = tmp006[1];
            kpdTemp006_Offset.Text = Units.strFormat(tmp006[7], iMinDec);

            tbkTemp007_Name.Text = tmp007[0];
            kpdTemp007_Alias.Text = tmp007[1];
            kpdTemp007_Offset.Text = Units.strFormat(tmp007[7], iMinDec);

            tbkTemp008_Name.Text = tmp008[0];
            kpdTemp008_Alias.Text = tmp008[1];
            kpdTemp008_Offset.Text = Units.strFormat(tmp008[7], iMinDec);

            tbkTemp009_Name.Text = tmp009[0];
            kpdTemp009_Alias.Text = tmp009[1];
            kpdTemp009_Offset.Text = Units.strFormat(tmp009[7], iMinDec);

            tbkTemp010_Name.Text = tmp010[0];
            kpdTemp010_Alias.Text = tmp010[1];
            kpdTemp010_Offset.Text = Units.strFormat(tmp010[7], iMinDec);

            tbkTemp011_Name.Text = tmp011[0];
            kpdTemp011_Alias.Text = tmp011[1];
            kpdTemp011_Offset.Text = Units.strFormat(tmp011[7], iMinDec);

            tbkTemp012_Name.Text = tmp012[0];
            kpdTemp012_Alias.Text = tmp012[1];
            kpdTemp012_Offset.Text = Units.strFormat(tmp012[7], iMinDec);

            tbkTemp013_Name.Text = tmp013[0];
            kpdTemp013_Alias.Text = tmp013[1];
            kpdTemp013_Offset.Text = Units.strFormat(tmp013[7], iMinDec);

            tbkTemp014_Name.Text = tmp014[0];
            kpdTemp014_Alias.Text = tmp014[1];
            kpdTemp014_Offset.Text = Units.strFormat(tmp014[7], iMinDec);

            tbkTemp015_Name.Text = tmp015[0];
            kpdTemp015_Alias.Text = tmp015[1];
            kpdTemp015_Offset.Text = Units.strFormat(tmp015[7], iMinDec);

            tbkTemp016_Name.Text = tmp016[0];
            kpdTemp016_Alias.Text = tmp016[1];
            kpdTemp016_Offset.Text = Units.strFormat(tmp016[7], iMinDec);

            tbkTemp017_Name.Text = tmp017[0];
            kpdTemp017_Alias.Text = tmp017[1];
            kpdTemp017_Offset.Text = Units.strFormat(tmp017[7], iMinDec);

            tbkTemp018_Name.Text = tmp018[0];
            kpdTemp018_Alias.Text = tmp018[1];
            kpdTemp018_Offset.Text = Units.strFormat(tmp018[7], iMinDec);

            tbkTemp019_Name.Text = tmp019[0];
            kpdTemp019_Alias.Text = tmp019[1];
            kpdTemp019_Offset.Text = Units.strFormat(tmp019[7], iMinDec);

            tbkTemp020_Name.Text = tmp020[0];
            kpdTemp020_Alias.Text = tmp020[1];
            kpdTemp020_Offset.Text = Units.strFormat(tmp020[7], iMinDec);

            tbkTemp021_Name.Text = tmp021[0];
            kpdTemp021_Alias.Text = tmp021[1];
            kpdTemp021_Offset.Text = Units.strFormat(tmp021[7], iMinDec);

            tbkTemp022_Name.Text = tmp022[0];
            kpdTemp022_Alias.Text = tmp022[1];
            kpdTemp022_Offset.Text = Units.strFormat(tmp022[7], iMinDec);

            tbkTemp023_Name.Text = tmp023[0];
            kpdTemp023_Alias.Text = tmp023[1];
            kpdTemp023_Offset.Text = Units.strFormat(tmp023[7], iMinDec);

            tbkTemp024_Name.Text = tmp024[0];
            kpdTemp024_Alias.Text = tmp024[1];
            kpdTemp024_Offset.Text = Units.strFormat(tmp024[7], iMinDec);

            tbkTemp025_Name.Text = tmp025[0];
            kpdTemp025_Alias.Text = tmp025[1];
            kpdTemp025_Offset.Text = Units.strFormat(tmp025[7], iMinDec);

            tbkTemp026_Name.Text = tmp026[0];
            kpdTemp026_Alias.Text = tmp026[1];
            kpdTemp026_Offset.Text = Units.strFormat(tmp026[7], iMinDec);

            tbkAOut000_Name.Text = aout000[0];
            kpdAOut000_Alias.Text = aout000[1];

            tbkAOut000RPM_Name.Text = aout000RPM[0];
            kpdAOut000RPM_Alias.Text = aout000RPM[1];

            tbkAOut001_Name.Text = aout001[0];
            kpdAOut001_Alias.Text = aout001[1];

            tbkAOut002_Name.Text = aout002[0];
            kpdAOut002_Alias.Text = aout002[1];

            tbkAOut003_Name.Text = aout003[0];
            kpdAOut003_Alias.Text = aout003[1];

            tbkAOut004_Name.Text = aout004[0];
            kpdAOut004_Alias.Text = aout004[1];

            tbkAOut005_Name.Text = aout005[0];
            kpdAOut005_Alias.Text = aout005[1];

            tbkAOut006_Name.Text = aout006[0];
            kpdAOut006_Alias.Text = aout006[1];

            tbkAOut007_Name.Text = aout007[0];
            kpdAOut007_Alias.Text = aout007[1];

            tbkAOut008_Name.Text = aout008[0];
            kpdAOut008_Alias.Text = aout008[1];

            tbkAOut009_Name.Text = aout009[0];
            kpdAOut009_Alias.Text = aout009[1];

            tbkAOut010_Name.Text = aout010[0];
            kpdAOut010_Alias.Text = aout010[1];

            for (int i = 0; i < 2; i++)
            {
                do000[i] = ADS.DO000[i];
                do001[i] = ADS.DO001[i];
                do002[i] = ADS.DO002[i];
                do003[i] = ADS.DO003[i];
                do004[i] = ADS.DO004[i];
                do005[i] = ADS.DO005[i];
                do006[i] = ADS.DO006[i];
                do007[i] = ADS.DO007[i];
                do008[i] = ADS.DO008[i];
                do009[i] = ADS.DO009[i];
                do010[i] = ADS.DO010[i];
                do011[i] = ADS.DO011[i];
                do012[i] = ADS.DO012[i];
                do013[i] = ADS.DO013[i];
                do014[i] = ADS.DO014[i];
                do015[i] = ADS.DO015[i];
                do016[i] = ADS.DO016[i];
                do017[i] = ADS.DO017[i];
                do018[i] = ADS.DO018[i];
                do019[i] = ADS.DO019[i];
                do020[i] = ADS.DO020[i];
                do021[i] = ADS.DO021[i];
                do022[i] = ADS.DO022[i];                                                // 09/20/23 PS
            }

            tbkDO000_Name.Text = do000[0];
            kpdDO000_Alias.Text = do000[1];
            tbkDO001_Name.Text = do001[0];
            kpdDO001_Alias.Text = do001[1];
            tbkDO002_Name.Text = do002[0];
            kpdDO002_Alias.Text = do002[1];
            tbkDO003_Name.Text = do003[0];
            kpdDO003_Alias.Text = do003[1];
            tbkDO004_Name.Text = do004[0];
            kpdDO004_Alias.Text = do004[1];
            tbkDO005_Name.Text = do005[0];
            kpdDO005_Alias.Text = do005[1];
            tbkDO006_Name.Text = do006[0];
            kpdDO006_Alias.Text = do006[1];
            tbkDO007_Name.Text = do007[0];
            kpdDO007_Alias.Text = do007[1];
            tbkDO008_Name.Text = do008[0];
            kpdDO008_Alias.Text = do008[1];
            tbkDO009_Name.Text = do009[0];
            kpdDO009_Alias.Text = do009[1];
            tbkDO010_Name.Text = do010[0];
            kpdDO010_Alias.Text = do010[1];
            tbkDO011_Name.Text = do011[0];
            kpdDO011_Alias.Text = do011[1];
            tbkDO012_Name.Text = do012[0];
            kpdDO012_Alias.Text = do012[1];
            tbkDO013_Name.Text = do013[0];
            kpdDO013_Alias.Text = do013[1];
            tbkDO014_Name.Text = do014[0];
            kpdDO014_Alias.Text = do014[1];
            tbkDO015_Name.Text = do015[0];
            kpdDO015_Alias.Text = do015[1];
            tbkDO016_Name.Text = do016[0];
            kpdDO016_Alias.Text = do016[1];
            tbkDO017_Name.Text = do017[0];
            kpdDO017_Alias.Text = do017[1];
            tbkDO018_Name.Text = do018[0];
            kpdDO018_Alias.Text = do018[1];
            tbkDO019_Name.Text = do019[0];
            kpdDO019_Alias.Text = do019[1];
            tbkDO020_Name.Text = do020[0];
            kpdDO020_Alias.Text = do020[1];
            tbkDO021_Name.Text = do021[0];
            kpdDO021_Alias.Text = do021[1];
            tbkDO022_Name.Text = do022[0];                                                      // 09/20/23 PS
            kpdDO022_Alias.Text = do022[1];                                                     // 09/20/23 PS

            ResetDecimals();
            btnAINameClick();
            btnDONameClick();
        }

        private void SetDecimals(string[] pt, ref ComboBox cb, ref Touch.TextBoxKeyboard kplow, ref Touch.TextBoxKeyboard kphigh)
        {
            iDec = Convert.ToInt32(pt[3]);
            cb.SelectedIndex = iDec;
            kplow.Decimals = iDec;
            kphigh.Decimals = iDec;
            kplow.Text = Units.strFormat(pt[4], iDec);
            kphigh.Text = Units.strFormat(pt[5], iDec);
        }

        private void ResetDecimals()
        {
            SetDecimals(pt000, ref cmbAIn000_Decimals, ref kpdAIn000_Low, ref kpdAIn000_High);

            iDec = Convert.ToInt32(pt001[3]);
            cmbAIn001_Decimals.SelectedIndex = iDec;
            kpdAIn001_Low.Decimals = iDec;
            kpdAIn001_High.Decimals = iDec;
            kpdAIn001_Low.Text = Units.strFormat(pt001[4], iDec);
            kpdAIn001_High.Text = Units.strFormat(pt001[5], iDec);

            iDec = Convert.ToInt32(pt002[3]);
            cmbAIn002_Decimals.SelectedIndex = iDec;
            kpdAIn002_Low.Decimals = iDec;
            kpdAIn002_High.Decimals = iDec;
            kpdAIn002_Low.Text = Units.strFormat(pt002[4], iDec);
            kpdAIn002_High.Text = Units.strFormat(pt002[5], iDec);

            iDec = Convert.ToInt32(pt003[3]);
            cmbAIn003_Decimals.SelectedIndex = iDec;
            kpdAIn003_Low.Decimals = iDec;
            kpdAIn003_High.Decimals = iDec;
            kpdAIn003_Low.Text = Units.strFormat(pt003[4], iDec);
            kpdAIn003_High.Text = Units.strFormat(pt003[5], iDec);

            iDec = Convert.ToInt32(pt004[3]);
            cmbAIn004_Decimals.SelectedIndex = iDec;
            kpdAIn004_Low.Decimals = iDec;
            kpdAIn004_High.Decimals = iDec;
            kpdAIn004_Low.Text = Units.strFormat(pt004[4], iDec);
            kpdAIn004_High.Text = Units.strFormat(pt004[5], iDec);

            iDec = Convert.ToInt32(pt005[3]);
            cmbAIn005_Decimals.SelectedIndex = iDec;
            kpdAIn005_Low.Decimals = iDec;
            kpdAIn005_High.Decimals = iDec;
            kpdAIn005_Low.Text = Units.strFormat(pt005[4], iDec);
            kpdAIn005_High.Text = Units.strFormat(pt005[5], iDec);

            iDec = Convert.ToInt32(pt006[3]);
            cmbAIn006_Decimals.SelectedIndex = iDec;
            kpdAIn006_Low.Decimals = iDec;
            kpdAIn006_High.Decimals = iDec;
            kpdAIn006_Low.Text = Units.strFormat(pt006[4], iDec);
            kpdAIn006_High.Text = Units.strFormat(pt006[5], iDec);

            iDec = Convert.ToInt32(pt007[3]);
            cmbAIn007_Decimals.SelectedIndex = iDec;
            kpdAIn007_Low.Decimals = iDec;
            kpdAIn007_High.Decimals = iDec;
            kpdAIn007_Low.Text = Units.strFormat(pt007[4], iDec);
            kpdAIn007_High.Text = Units.strFormat(pt007[5], iDec);

            iDec = Convert.ToInt32(pt008[3]);
            cmbAIn008_Decimals.SelectedIndex = iDec;
            kpdAIn008_Low.Decimals = iDec;
            kpdAIn008_High.Decimals = iDec;
            kpdAIn008_Low.Text = Units.strFormat(pt008[4], iDec);
            kpdAIn008_High.Text = Units.strFormat(pt008[5], iDec);

            iDec = Convert.ToInt32(pt009[3]);
            cmbAIn009_Decimals.SelectedIndex = iDec;
            kpdAIn009_Low.Decimals = iDec;
            kpdAIn009_High.Decimals = iDec;
            kpdAIn009_Low.Text = Units.strFormat(pt009[4], iDec);
            kpdAIn009_High.Text = Units.strFormat(pt009[5], iDec);

            iDec = Convert.ToInt32(pt010[3]);
            cmbAIn010_Decimals.SelectedIndex = iDec;
            kpdAIn010_Low.Decimals = iDec;
            kpdAIn010_High.Decimals = iDec;
            kpdAIn010_Low.Text = Units.strFormat(pt010[4], iDec);
            kpdAIn010_High.Text = Units.strFormat(pt010[5], iDec);

            iDec = Convert.ToInt32(pt011[3]);
            cmbAIn011_Decimals.SelectedIndex = iDec;
            kpdAIn011_Low.Decimals = iDec;
            kpdAIn011_High.Decimals = iDec;
            kpdAIn011_Low.Text = Units.strFormat(pt011[4], iDec);
            kpdAIn011_High.Text = Units.strFormat(pt011[5], iDec);

            iDec = Convert.ToInt32(pt012[3]);
            cmbAIn012_Decimals.SelectedIndex = iDec;
            kpdAIn012_Low.Decimals = iDec;
            kpdAIn012_High.Decimals = iDec;
            kpdAIn012_Low.Text = Units.strFormat(pt012[4], iDec);
            kpdAIn012_High.Text = Units.strFormat(pt012[5], iDec);

            iDec = Convert.ToInt32(pt013[3]);
            cmbAIn013_Decimals.SelectedIndex = iDec;
            kpdAIn013_Low.Decimals = iDec;
            kpdAIn013_High.Decimals = iDec;
            kpdAIn013_Low.Text = Units.strFormat(pt013[4], iDec);
            kpdAIn013_High.Text = Units.strFormat(pt013[5], iDec);

            iDec = Convert.ToInt32(pt014[3]);
            cmbAIn014_Decimals.SelectedIndex = iDec;
            kpdAIn014_Low.Decimals = iDec;
            kpdAIn014_High.Decimals = iDec;
            kpdAIn014_Low.Text = Units.strFormat(pt014[4], iDec);
            kpdAIn014_High.Text = Units.strFormat(pt014[5], iDec);

            iDec = Convert.ToInt32(pt015[3]);
            cmbAIn015_Decimals.SelectedIndex = iDec;
            kpdAIn015_Low.Decimals = iDec;
            kpdAIn015_High.Decimals = iDec;
            kpdAIn015_Low.Text = Units.strFormat(pt015[4], iDec);
            kpdAIn015_High.Text = Units.strFormat(pt015[5], iDec);

            iDec = Convert.ToInt32(pt016[3]);
            cmbAIn016_Decimals.SelectedIndex = iDec;
            kpdAIn016_Low.Decimals = iDec;
            kpdAIn016_High.Decimals = iDec;
            kpdAIn016_Low.Text = Units.strFormat(pt016[4], iDec);
            kpdAIn016_High.Text = Units.strFormat(pt016[5], iDec);

            iDec = Convert.ToInt32(pt017[3]);
            cmbAIn017_Decimals.SelectedIndex = iDec;
            kpdAIn017_Low.Decimals = iDec;
            kpdAIn017_High.Decimals = iDec;
            kpdAIn017_Low.Text = Units.strFormat(pt017[4], iDec);
            kpdAIn017_High.Text = Units.strFormat(pt017[5], iDec);

            iDec = Convert.ToInt32(pt018[3]);
            cmbAIn018_Decimals.SelectedIndex = iDec;
            kpdAIn018_Low.Decimals = iDec;
            kpdAIn018_High.Decimals = iDec;
            kpdAIn018_Low.Text = Units.strFormat(pt018[4], iDec);
            kpdAIn018_High.Text = Units.strFormat(pt018[5], iDec);

            iDec = Convert.ToInt32(pt019[3]);
            cmbAIn019_Decimals.SelectedIndex = iDec;
            kpdAIn019_Low.Decimals = iDec;
            kpdAIn019_High.Decimals = iDec;
            kpdAIn019_Low.Text = Units.strFormat(pt019[4], iDec);
            kpdAIn019_High.Text = Units.strFormat(pt019[5], iDec);

            iDec = Convert.ToInt32(pt020[3]);
            cmbAIn020_Decimals.SelectedIndex = iDec;
            kpdAIn020_Low.Decimals = iDec;
            kpdAIn020_High.Decimals = iDec;
            kpdAIn020_Low.Text = Units.strFormat(pt020[4], iDec);
            kpdAIn020_High.Text = Units.strFormat(pt020[5], iDec);

            iDec = Convert.ToInt32(pt021[3]);
            cmbAIn021_Decimals.SelectedIndex = iDec;
            kpdAIn021_Low.Decimals = iDec;
            kpdAIn021_High.Decimals = iDec;
            kpdAIn021_Low.Text = Units.strFormat(pt021[4], iDec);
            kpdAIn021_High.Text = Units.strFormat(pt021[5], iDec);

            iDec = Convert.ToInt32(pt022[3]);
            cmbAIn022_Decimals.SelectedIndex = iDec;
            kpdAIn022_Low.Decimals = iDec;
            kpdAIn022_High.Decimals = iDec;
            kpdAIn022_Low.Text = Units.strFormat(pt022[4], iDec);
            kpdAIn022_High.Text = Units.strFormat(pt022[5], iDec);

            iDec = Convert.ToInt32(pt023[3]);
            cmbAIn023_Decimals.SelectedIndex = iDec;
            kpdAIn023_Low.Decimals = iDec;
            kpdAIn023_High.Decimals = iDec;
            kpdAIn023_Low.Text = Units.strFormat(pt023[4], iDec);
            kpdAIn023_High.Text = Units.strFormat(pt023[5], iDec);

            iDec = Convert.ToInt32(pt024[3]);
            cmbAIn024_Decimals.SelectedIndex = iDec;
            kpdAIn024_Low.Decimals = iDec;
            kpdAIn024_High.Decimals = iDec;
            kpdAIn024_Low.Text = Units.strFormat(pt024[4], iDec);
            kpdAIn024_High.Text = Units.strFormat(pt024[5], iDec);

            iDec = Convert.ToInt32(pt025[3]);
            cmbAIn025_Decimals.SelectedIndex = iDec;
            kpdAIn025_Low.Decimals = iDec;
            kpdAIn025_High.Decimals = iDec;
            kpdAIn025_Low.Text = Units.strFormat(pt025[4], iDec);
            kpdAIn025_High.Text = Units.strFormat(pt025[5], iDec);

            iDec = Convert.ToInt32(pt026[3]);
            cmbAIn026_Decimals.SelectedIndex = iDec;
            kpdAIn026_Low.Decimals = iDec;
            kpdAIn026_High.Decimals = iDec;
            kpdAIn026_Low.Text = Units.strFormat(pt026[4], iDec);
            kpdAIn026_High.Text = Units.strFormat(pt026[5], iDec);

            iDec = Convert.ToInt32(pt027[3]);
            cmbAIn027_Decimals.SelectedIndex = iDec;
            kpdAIn027_Low.Decimals = iDec;
            kpdAIn027_High.Decimals = iDec;
            kpdAIn027_Low.Text = Units.strFormat(pt027[4], iDec);
            kpdAIn027_High.Text = Units.strFormat(pt027[5], iDec);

            iDec = Convert.ToInt32(pt028[3]);
            cmbAIn028_Decimals.SelectedIndex = iDec;
            kpdAIn028_Low.Decimals = iDec;
            kpdAIn028_High.Decimals = iDec;
            kpdAIn028_Low.Text = Units.strFormat(pt028[4], iDec);
            kpdAIn028_High.Text = Units.strFormat(pt028[5], iDec);

            iDec = Convert.ToInt32(pt040[3]);
            cmbAIn040_Decimals.SelectedIndex = iDec;
            kpdAIn040_Low.Decimals = iDec;
            kpdAIn040_High.Decimals = iDec;
            kpdAIn040_Low.Text = Units.strFormat(pt040[4], iDec);
            kpdAIn040_High.Text = Units.strFormat(pt040[5], iDec);

            iDec = Convert.ToInt32(pt041[3]);
            cmbAIn041_Decimals.SelectedIndex = iDec;
            kpdAIn041_Low.Decimals = iDec;
            kpdAIn041_High.Decimals = iDec;
            kpdAIn041_Low.Text = Units.strFormat(pt041[4], iDec);
            kpdAIn041_High.Text = Units.strFormat(pt041[5], iDec);

            iDec = Convert.ToInt32(pt042[3]);
            cmbAIn042_Decimals.SelectedIndex = iDec;
            kpdAIn042_Low.Decimals = iDec;
            kpdAIn042_High.Decimals = iDec;
            kpdAIn042_Low.Text = Units.strFormat(pt042[4], iDec);
            kpdAIn042_High.Text = Units.strFormat(pt042[5], iDec);

            iDec = Convert.ToInt32(pt043[3]);
            cmbAIn043_Decimals.SelectedIndex = iDec;
            kpdAIn043_Low.Decimals = iDec;
            kpdAIn043_High.Decimals = iDec;
            kpdAIn043_Low.Text = Units.strFormat(pt043[4], iDec);
            kpdAIn043_High.Text = Units.strFormat(pt043[5], iDec);

            iDec = Convert.ToInt32(pt044[3]);
            cmbAIn044_Decimals.SelectedIndex = iDec;
            kpdAIn044_Low.Decimals = iDec;
            kpdAIn044_High.Decimals = iDec;
            kpdAIn044_Low.Text = Units.strFormat(pt044[4], iDec);
            kpdAIn044_High.Text = Units.strFormat(pt044[5], iDec);

            iDec = Convert.ToInt32(pt045[3]);
            cmbAIn045_Decimals.SelectedIndex = iDec;
            kpdAIn045_Low.Decimals = iDec;
            kpdAIn045_High.Decimals = iDec;
            kpdAIn045_Low.Text = Units.strFormat(pt045[4], iDec);
            kpdAIn045_High.Text = Units.strFormat(pt045[5], iDec);

            iDec = Convert.ToInt32(pt046[3]);
            cmbAIn046_Decimals.SelectedIndex = iDec;
            kpdAIn046_Low.Decimals = iDec;
            kpdAIn046_High.Decimals = iDec;
            kpdAIn046_Low.Text = Units.strFormat(pt046[4], iDec);
            kpdAIn046_High.Text = Units.strFormat(pt046[5], iDec);

            iDec = Convert.ToInt32(pt050[3]);
            kpdAIn050_Low.Decimals = iDec;
            kpdAIn050_High.Decimals = iDec;
            kpdAIn050_Low.Text = Units.strFormat(pt050[4], iDec);
            kpdAIn050_High.Text = Units.strFormat(pt050[5], iDec);

            iDec = Convert.ToInt32(pt051[3]);
            kpdAIn051_Low.Decimals = iDec;
            kpdAIn051_High.Decimals = iDec;
            kpdAIn051_Low.Text = Units.strFormat(pt051[4], iDec);
            kpdAIn051_High.Text = Units.strFormat(pt051[5], iDec);

            iDec = Convert.ToInt32(pt052[3]);
            kpdAIn052_Low.Decimals = iDec;
            kpdAIn052_High.Decimals = iDec;
            kpdAIn052_Low.Text = Units.strFormat(pt052[4], iDec);
            kpdAIn052_High.Text = Units.strFormat(pt052[5], iDec);

            iDec = Convert.ToInt32(pt053[3]);
            kpdAIn053_Low.Decimals = iDec;
            kpdAIn053_High.Decimals = iDec;
            kpdAIn053_Low.Text = Units.strFormat(pt053[4], iDec);
            kpdAIn053_High.Text = Units.strFormat(pt053[5], iDec);

            iDec = Convert.ToInt32(pt054[3]);
            kpdAIn054_Low.Decimals = iDec;
            kpdAIn054_High.Decimals = iDec;
            kpdAIn054_Low.Text = Units.strFormat(pt054[4], iDec);
            kpdAIn054_High.Text = Units.strFormat(pt054[5], iDec);

            iDec = Convert.ToInt32(pt055[3]);
            kpdAIn055_Low.Decimals = iDec;
            kpdAIn055_High.Decimals = iDec;
            kpdAIn055_Low.Text = Units.strFormat(pt055[4], iDec);
            kpdAIn055_High.Text = Units.strFormat(pt055[5], iDec);

            iDec = Convert.ToInt32(tmp000[3]);
            kpdTemp000_Low.Decimals = iDec;
            kpdTemp000_High.Decimals = iDec;
            kpdTemp000_Low.Text = Units.strFormat(tmp000[4], iDec);
            kpdTemp000_High.Text = Units.strFormat(tmp000[5], iDec);

            iDec = Convert.ToInt32(tmp001[3]);
            kpdTemp001_Low.Decimals = iDec;
            kpdTemp001_High.Decimals = iDec;
            kpdTemp001_Low.Text = Units.strFormat(tmp001[4], iDec);
            kpdTemp001_High.Text = Units.strFormat(tmp001[5], iDec);

            iDec = Convert.ToInt32(tmp002[3]);
            kpdTemp002_Low.Decimals = iDec;
            kpdTemp002_High.Decimals = iDec;
            kpdTemp002_Low.Text = Units.strFormat(tmp002[4], iDec);
            kpdTemp002_High.Text = Units.strFormat(tmp002[5], iDec);

            iDec = Convert.ToInt32(tmp003[3]);
            kpdTemp003_Low.Decimals = iDec;
            kpdTemp003_High.Decimals = iDec;
            kpdTemp003_Low.Text = Units.strFormat(tmp003[4], iDec);
            kpdTemp003_High.Text = Units.strFormat(tmp003[5], iDec);

            iDec = Convert.ToInt32(tmp004[3]);
            kpdTemp004_Low.Decimals = iDec;
            kpdTemp004_High.Decimals = iDec;
            kpdTemp004_Low.Text = Units.strFormat(tmp004[4], iDec);
            kpdTemp004_High.Text = Units.strFormat(tmp004[5], iDec);

            iDec = Convert.ToInt32(tmp005[3]);
            kpdTemp005_Low.Decimals = iDec;
            kpdTemp005_High.Decimals = iDec;
            kpdTemp005_Low.Text = Units.strFormat(tmp005[4], iDec);
            kpdTemp005_High.Text = Units.strFormat(tmp005[5], iDec);

            iDec = Convert.ToInt32(tmp006[3]);
            kpdTemp006_Low.Decimals = iDec;
            kpdTemp006_High.Decimals = iDec;
            kpdTemp006_Low.Text = Units.strFormat(tmp006[4], iDec);
            kpdTemp006_High.Text = Units.strFormat(tmp006[5], iDec);

            iDec = Convert.ToInt32(tmp007[3]);
            kpdTemp007_Low.Decimals = iDec;
            kpdTemp007_High.Decimals = iDec;
            kpdTemp007_Low.Text = Units.strFormat(tmp007[4], iDec);
            kpdTemp007_High.Text = Units.strFormat(tmp007[5], iDec);

            iDec = Convert.ToInt32(tmp008[3]);
            kpdTemp008_Low.Decimals = iDec;
            kpdTemp008_High.Decimals = iDec;
            kpdTemp008_Low.Text = Units.strFormat(tmp008[4], iDec);
            kpdTemp008_High.Text = Units.strFormat(tmp008[5], iDec);

            iDec = Convert.ToInt32(tmp009[3]);
            kpdTemp009_Low.Decimals = iDec;
            kpdTemp009_High.Decimals = iDec;
            kpdTemp009_Low.Text = Units.strFormat(tmp009[4], iDec);
            kpdTemp009_High.Text = Units.strFormat(tmp009[5], iDec);

            iDec = Convert.ToInt32(tmp010[3]);
            kpdTemp010_Low.Decimals = iDec;
            kpdTemp010_High.Decimals = iDec;
            kpdTemp010_Low.Text = Units.strFormat(tmp010[4], iDec);
            kpdTemp010_High.Text = Units.strFormat(tmp010[5], iDec);

            iDec = Convert.ToInt32(tmp011[3]);
            kpdTemp011_Low.Decimals = iDec;
            kpdTemp011_High.Decimals = iDec;
            kpdTemp011_Low.Text = Units.strFormat(tmp011[4], iDec);
            kpdTemp011_High.Text = Units.strFormat(tmp011[5], iDec);

            iDec = Convert.ToInt32(tmp012[3]);
            kpdTemp012_Low.Decimals = iDec;
            kpdTemp012_High.Decimals = iDec;
            kpdTemp012_Low.Text = Units.strFormat(tmp012[4], iDec);
            kpdTemp012_High.Text = Units.strFormat(tmp012[5], iDec);

            iDec = Convert.ToInt32(tmp013[3]);
            kpdTemp013_Low.Decimals = iDec;
            kpdTemp013_High.Decimals = iDec;
            kpdTemp013_Low.Text = Units.strFormat(tmp013[4], iDec);
            kpdTemp013_High.Text = Units.strFormat(tmp013[5], iDec);

            iDec = Convert.ToInt32(tmp014[3]);
            kpdTemp014_Low.Decimals = iDec;
            kpdTemp014_High.Decimals = iDec;
            kpdTemp014_Low.Text = Units.strFormat(tmp014[4], iDec);
            kpdTemp014_High.Text = Units.strFormat(tmp014[5], iDec);

            iDec = Convert.ToInt32(tmp015[3]);
            kpdTemp015_Low.Decimals = iDec;
            kpdTemp015_High.Decimals = iDec;
            kpdTemp015_Low.Text = Units.strFormat(tmp015[4], iDec);
            kpdTemp015_High.Text = Units.strFormat(tmp015[5], iDec);

            iDec = Convert.ToInt32(tmp016[3]);
            kpdTemp016_Low.Decimals = iDec;
            kpdTemp016_High.Decimals = iDec;
            kpdTemp016_Low.Text = Units.strFormat(tmp016[4], iDec);
            kpdTemp016_High.Text = Units.strFormat(tmp016[5], iDec);

            iDec = Convert.ToInt32(tmp017[3]);
            kpdTemp017_Low.Decimals = iDec;
            kpdTemp017_High.Decimals = iDec;
            kpdTemp017_Low.Text = Units.strFormat(tmp017[4], iDec);
            kpdTemp017_High.Text = Units.strFormat(tmp017[5], iDec);

            iDec = Convert.ToInt32(tmp018[3]);
            kpdTemp018_Low.Decimals = iDec;
            kpdTemp018_High.Decimals = iDec;
            kpdTemp018_Low.Text = Units.strFormat(tmp018[4], iDec);
            kpdTemp018_High.Text = Units.strFormat(tmp018[5], iDec);

            iDec = Convert.ToInt32(tmp019[3]);
            kpdTemp019_Low.Decimals = iDec;
            kpdTemp019_High.Decimals = iDec;
            kpdTemp019_Low.Text = Units.strFormat(tmp019[4], iDec);
            kpdTemp019_High.Text = Units.strFormat(tmp019[5], iDec);

            iDec = Convert.ToInt32(tmp020[3]);
            kpdTemp020_Low.Decimals = iDec;
            kpdTemp020_High.Decimals = iDec;
            kpdTemp020_Low.Text = Units.strFormat(tmp020[4], iDec);
            kpdTemp020_High.Text = Units.strFormat(tmp020[5], iDec);

            iDec = Convert.ToInt32(tmp021[3]);
            kpdTemp021_Low.Decimals = iDec;
            kpdTemp021_High.Decimals = iDec;
            kpdTemp021_Low.Text = Units.strFormat(tmp021[4], iDec);
            kpdTemp021_High.Text = Units.strFormat(tmp021[5], iDec);

            iDec = Convert.ToInt32(tmp022[3]);
            kpdTemp022_Low.Decimals = iDec;
            kpdTemp022_High.Decimals = iDec;
            kpdTemp022_Low.Text = Units.strFormat(tmp022[4], iDec);
            kpdTemp022_High.Text = Units.strFormat(tmp022[5], iDec);

            iDec = Convert.ToInt32(tmp023[3]);
            kpdTemp023_Low.Decimals = iDec;
            kpdTemp023_High.Decimals = iDec;
            kpdTemp023_Low.Text = Units.strFormat(tmp023[4], iDec);
            kpdTemp023_High.Text = Units.strFormat(tmp023[5], iDec);

            iDec = Convert.ToInt32(tmp024[3]);
            kpdTemp024_Low.Decimals = iDec;
            kpdTemp024_High.Decimals = iDec;
            kpdTemp024_Low.Text = Units.strFormat(tmp024[4], iDec);
            kpdTemp024_High.Text = Units.strFormat(tmp024[5], iDec);

            iDec = Convert.ToInt32(tmp025[3]);
            kpdTemp025_Low.Decimals = iDec;
            kpdTemp025_High.Decimals = iDec;
            kpdTemp025_Low.Text = Units.strFormat(tmp025[4], iDec);
            kpdTemp025_High.Text = Units.strFormat(tmp025[5], iDec);

            iDec = Convert.ToInt32(tmp026[3]);
            kpdTemp026_Low.Decimals = iDec;
            kpdTemp026_High.Decimals = iDec;
            kpdTemp026_Low.Text = Units.strFormat(tmp026[4], iDec);
            kpdTemp026_High.Text = Units.strFormat(tmp026[5], iDec);

            iDec = Convert.ToInt32(aout001[3]);
            kpdAOut001_Low.Decimals = iDec;
            kpdAOut001_High.Decimals = iDec;
            kpdAOut001_Low.Text = Units.strFormat(aout001[4], iDec);
            kpdAOut001_High.Text = Units.strFormat(aout001[5], iDec);

            iDec = Convert.ToInt32(aout002[3]);
            kpdAOut002_Low.Decimals = iDec;
            kpdAOut002_High.Decimals = iDec;
            kpdAOut002_Low.Text = Units.strFormat(aout002[4], iDec);
            kpdAOut002_High.Text = Units.strFormat(aout002[5], iDec);

            iDec = Convert.ToInt32(aout003[3]);
            kpdAOut003_Low.Decimals = iDec;
            kpdAOut003_High.Decimals = iDec;
            kpdAOut003_Low.Text = Units.strFormat(aout003[4], iDec);
            kpdAOut003_High.Text = Units.strFormat(aout003[5], iDec);

            iDec = Convert.ToInt32(aout004[3]);
            kpdAOut004_Low.Decimals = iDec;
            kpdAOut004_High.Decimals = iDec;
            kpdAOut004_Low.Text = Units.strFormat(aout004[4], iDec);
            kpdAOut004_High.Text = Units.strFormat(aout004[5], iDec);

            iDec = Convert.ToInt32(aout005[3]);
            kpdAOut005_Low.Decimals = iDec;
            kpdAOut005_High.Decimals = iDec;
            kpdAOut005_Low.Text = Units.strFormat(aout005[4], iDec);
            kpdAOut005_High.Text = Units.strFormat(aout005[5], iDec);

            iDec = Convert.ToInt32(aout006[3]);
            kpdAOut006_Low.Decimals = iDec;
            kpdAOut006_High.Decimals = iDec;
            kpdAOut006_Low.Text = Units.strFormat(aout006[4], iDec);
            kpdAOut006_High.Text = Units.strFormat(aout006[5], iDec);
        }

        private void LoadPointsConfig()
        {
            SetPointConfig(pt000, tbkAIn000_Name, kpdAIn000_Alias, cmbAIn000_Units, cmbAIn000_Decimals, kpdAIn000_Low, kpdAIn000_High, kpdAIn000_Min, kpdAIn000_Offset);
            SetPointConfig(pt001, tbkAIn001_Name, kpdAIn001_Alias, cmbAIn001_Units, cmbAIn001_Decimals, kpdAIn001_Low, kpdAIn001_High, kpdAIn001_Min, kpdAIn001_Offset);
            SetPointConfig(pt002, tbkAIn002_Name, kpdAIn002_Alias, cmbAIn002_Units, cmbAIn002_Decimals, kpdAIn002_Low, kpdAIn002_High, kpdAIn002_Min, kpdAIn002_Offset);
            SetPointConfig(pt003, tbkAIn003_Name, kpdAIn003_Alias, cmbAIn003_Units, cmbAIn003_Decimals, kpdAIn003_Low, kpdAIn003_High, kpdAIn003_Min, kpdAIn003_Offset);
            SetPointConfig(pt004, tbkAIn004_Name, kpdAIn004_Alias, cmbAIn004_Units, cmbAIn004_Decimals, kpdAIn004_Low, kpdAIn004_High, kpdAIn004_Min, kpdAIn004_Offset);
            SetPointConfig(pt005, tbkAIn005_Name, kpdAIn005_Alias, cmbAIn005_Units, cmbAIn005_Decimals, kpdAIn005_Low, kpdAIn005_High, kpdAIn005_Min, kpdAIn005_Offset);
            SetPointConfig(pt006, tbkAIn006_Name, kpdAIn006_Alias, cmbAIn006_Units, cmbAIn006_Decimals, kpdAIn006_Low, kpdAIn006_High, kpdAIn006_Min, kpdAIn006_Offset);
            SetPointConfig(pt007, tbkAIn007_Name, kpdAIn007_Alias, cmbAIn007_Units, cmbAIn007_Decimals, kpdAIn007_Low, kpdAIn007_High, kpdAIn007_Min, kpdAIn007_Offset);
            SetPointConfig(pt008, tbkAIn008_Name, kpdAIn008_Alias, cmbAIn008_Units, cmbAIn008_Decimals, kpdAIn008_Low, kpdAIn008_High, kpdAIn008_Min, kpdAIn008_Offset);
            SetPointConfig(pt009, tbkAIn009_Name, kpdAIn009_Alias, cmbAIn009_Units, cmbAIn009_Decimals, kpdAIn009_Low, kpdAIn009_High, kpdAIn009_Min, kpdAIn009_Offset);
            SetPointConfig(pt010, tbkAIn010_Name, kpdAIn010_Alias, cmbAIn010_Units, cmbAIn010_Decimals, kpdAIn010_Low, kpdAIn010_High, kpdAIn010_Min, kpdAIn010_Offset);
            SetPointConfig(pt011, tbkAIn011_Name, kpdAIn011_Alias, cmbAIn011_Units, cmbAIn011_Decimals, kpdAIn011_Low, kpdAIn011_High, kpdAIn011_Min, kpdAIn011_Offset);
            SetPointConfig(pt012, tbkAIn012_Name, kpdAIn012_Alias, cmbAIn012_Units, cmbAIn012_Decimals, kpdAIn012_Low, kpdAIn012_High, kpdAIn012_Min, kpdAIn012_Offset);
            SetPointConfig(pt013, tbkAIn013_Name, kpdAIn013_Alias, cmbAIn013_Units, cmbAIn013_Decimals, kpdAIn013_Low, kpdAIn013_High, kpdAIn013_Min, kpdAIn013_Offset);
            SetPointConfig(pt014, tbkAIn014_Name, kpdAIn014_Alias, cmbAIn014_Units, cmbAIn014_Decimals, kpdAIn014_Low, kpdAIn014_High, kpdAIn014_Min, kpdAIn014_Offset);
            SetPointConfig(pt015, tbkAIn015_Name, kpdAIn015_Alias, cmbAIn015_Units, cmbAIn015_Decimals, kpdAIn015_Low, kpdAIn015_High, kpdAIn015_Min, kpdAIn015_Offset);
            SetPointConfig(pt016, tbkAIn016_Name, kpdAIn016_Alias, cmbAIn016_Units, cmbAIn016_Decimals, kpdAIn016_Low, kpdAIn016_High, kpdAIn016_Min, kpdAIn016_Offset);
            SetPointConfig(pt017, tbkAIn017_Name, kpdAIn017_Alias, cmbAIn017_Units, cmbAIn017_Decimals, kpdAIn017_Low, kpdAIn017_High, kpdAIn017_Min, kpdAIn017_Offset);
            SetPointConfig(pt018, tbkAIn018_Name, kpdAIn018_Alias, cmbAIn018_Units, cmbAIn018_Decimals, kpdAIn018_Low, kpdAIn018_High, kpdAIn018_Min, kpdAIn018_Offset);
            SetPointConfig(pt019, tbkAIn019_Name, kpdAIn019_Alias, cmbAIn019_Units, cmbAIn019_Decimals, kpdAIn019_Low, kpdAIn019_High, kpdAIn019_Min, kpdAIn019_Offset);
            SetPointConfig(pt020, tbkAIn020_Name, kpdAIn020_Alias, cmbAIn020_Units, cmbAIn020_Decimals, kpdAIn020_Low, kpdAIn020_High, kpdAIn020_Min, kpdAIn020_Offset);
            SetPointConfig(pt021, tbkAIn021_Name, kpdAIn021_Alias, cmbAIn021_Units, cmbAIn021_Decimals, kpdAIn021_Low, kpdAIn021_High, kpdAIn021_Min, kpdAIn021_Offset);
            SetPointConfig(pt022, tbkAIn022_Name, kpdAIn022_Alias, cmbAIn022_Units, cmbAIn022_Decimals, kpdAIn022_Low, kpdAIn022_High, kpdAIn022_Min, kpdAIn022_Offset);
            SetPointConfig(pt023, tbkAIn023_Name, kpdAIn023_Alias, cmbAIn023_Units, cmbAIn023_Decimals, kpdAIn023_Low, kpdAIn023_High, kpdAIn023_Min, kpdAIn023_Offset);
            SetPointConfig(pt024, tbkAIn024_Name, kpdAIn024_Alias, cmbAIn024_Units, cmbAIn024_Decimals, kpdAIn024_Low, kpdAIn024_High, kpdAIn024_Min, kpdAIn024_Offset);
            SetPointConfig(pt025, tbkAIn025_Name, kpdAIn025_Alias, cmbAIn025_Units, cmbAIn025_Decimals, kpdAIn025_Low, kpdAIn025_High, kpdAIn025_Min, kpdAIn025_Offset);
            SetPointConfig(pt026, tbkAIn026_Name, kpdAIn026_Alias, cmbAIn026_Units, cmbAIn026_Decimals, kpdAIn026_Low, kpdAIn026_High, kpdAIn026_Min, kpdAIn026_Offset);
            SetPointConfig(pt027, tbkAIn027_Name, kpdAIn027_Alias, cmbAIn027_Units, cmbAIn027_Decimals, kpdAIn027_Low, kpdAIn027_High, kpdAIn027_Min, kpdAIn027_Offset);
            SetPointConfig(pt028, tbkAIn028_Name, kpdAIn028_Alias, cmbAIn028_Units, cmbAIn028_Decimals, kpdAIn028_Low, kpdAIn028_High, kpdAIn028_Min, kpdAIn028_Offset);
            SetPointConfig(pt040, tbkAIn040_Name, kpdAIn040_Alias, cmbAIn040_Units, cmbAIn040_Decimals, kpdAIn040_Low, kpdAIn040_High, kpdAIn040_Min, kpdAIn040_Offset);
            SetPointConfig(pt041, tbkAIn041_Name, kpdAIn041_Alias, cmbAIn041_Units, cmbAIn041_Decimals, kpdAIn041_Low, kpdAIn041_High, kpdAIn041_Min, kpdAIn041_Offset);
            SetPointConfig(pt042, tbkAIn042_Name, kpdAIn042_Alias, cmbAIn042_Units, cmbAIn042_Decimals, kpdAIn042_Low, kpdAIn042_High, kpdAIn042_Min, kpdAIn042_Offset);
            SetPointConfig(pt043, tbkAIn043_Name, kpdAIn043_Alias, cmbAIn043_Units, cmbAIn043_Decimals, kpdAIn043_Low, kpdAIn043_High, kpdAIn043_Min, kpdAIn043_Offset);
            SetPointConfig(pt044, tbkAIn044_Name, kpdAIn044_Alias, cmbAIn044_Units, cmbAIn044_Decimals, kpdAIn044_Low, kpdAIn044_High, kpdAIn044_Min, kpdAIn044_Offset);
            SetPointConfig(pt045, tbkAIn045_Name, kpdAIn045_Alias, cmbAIn045_Units, cmbAIn045_Decimals, kpdAIn045_Low, kpdAIn045_High, kpdAIn045_Min, kpdAIn045_Offset);
            SetPointConfig(pt046, tbkAIn046_Name, kpdAIn046_Alias, cmbAIn046_Units, cmbAIn046_Decimals, kpdAIn046_Low, kpdAIn046_High, kpdAIn046_Min, kpdAIn046_Offset);

            SetTempConfig(tmp000, tbkTemp000_Name, kpdTemp000_Alias, kpdTemp000_Low, kpdTemp000_High, kpdTemp000_Offset);
            SetTempConfig(tmp001, tbkTemp001_Name, kpdTemp001_Alias, kpdTemp001_Low, kpdTemp001_High, kpdTemp001_Offset);
            SetTempConfig(tmp002, tbkTemp002_Name, kpdTemp002_Alias, kpdTemp002_Low, kpdTemp002_High, kpdTemp002_Offset);
            SetTempConfig(tmp003, tbkTemp003_Name, kpdTemp003_Alias, kpdTemp003_Low, kpdTemp003_High, kpdTemp003_Offset);
            SetTempConfig(tmp004, tbkTemp004_Name, kpdTemp004_Alias, kpdTemp004_Low, kpdTemp004_High, kpdTemp004_Offset);
            SetTempConfig(tmp005, tbkTemp005_Name, kpdTemp005_Alias, kpdTemp005_Low, kpdTemp005_High, kpdTemp005_Offset);
            SetTempConfig(tmp006, tbkTemp006_Name, kpdTemp006_Alias, kpdTemp006_Low, kpdTemp006_High, kpdTemp006_Offset);
            SetTempConfig(tmp007, tbkTemp007_Name, kpdTemp007_Alias, kpdTemp007_Low, kpdTemp007_High, kpdTemp007_Offset);
            SetTempConfig(tmp008, tbkTemp008_Name, kpdTemp008_Alias, kpdTemp008_Low, kpdTemp008_High, kpdTemp008_Offset);
            SetTempConfig(tmp009, tbkTemp009_Name, kpdTemp009_Alias, kpdTemp009_Low, kpdTemp009_High, kpdTemp009_Offset);
            SetTempConfig(tmp010, tbkTemp010_Name, kpdTemp010_Alias, kpdTemp010_Low, kpdTemp010_High, kpdTemp010_Offset);
            SetTempConfig(tmp011, tbkTemp011_Name, kpdTemp011_Alias, kpdTemp011_Low, kpdTemp011_High, kpdTemp011_Offset);
            SetTempConfig(tmp012, tbkTemp012_Name, kpdTemp012_Alias, kpdTemp012_Low, kpdTemp012_High, kpdTemp012_Offset);
            SetTempConfig(tmp013, tbkTemp013_Name, kpdTemp013_Alias, kpdTemp013_Low, kpdTemp013_High, kpdTemp013_Offset);
            SetTempConfig(tmp014, tbkTemp014_Name, kpdTemp014_Alias, kpdTemp014_Low, kpdTemp014_High, kpdTemp014_Offset);
            SetTempConfig(tmp015, tbkTemp015_Name, kpdTemp015_Alias, kpdTemp015_Low, kpdTemp015_High, kpdTemp015_Offset);
            SetTempConfig(tmp016, tbkTemp016_Name, kpdTemp016_Alias, kpdTemp016_Low, kpdTemp016_High, kpdTemp016_Offset);
            SetTempConfig(tmp017, tbkTemp017_Name, kpdTemp017_Alias, kpdTemp017_Low, kpdTemp017_High, kpdTemp017_Offset);
            SetTempConfig(tmp018, tbkTemp018_Name, kpdTemp018_Alias, kpdTemp018_Low, kpdTemp018_High, kpdTemp018_Offset);
            SetTempConfig(tmp019, tbkTemp019_Name, kpdTemp019_Alias, kpdTemp019_Low, kpdTemp019_High, kpdTemp019_Offset);
            SetTempConfig(tmp020, tbkTemp020_Name, kpdTemp020_Alias, kpdTemp020_Low, kpdTemp020_High, kpdTemp020_Offset);
            SetTempConfig(tmp021, tbkTemp021_Name, kpdTemp021_Alias, kpdTemp021_Low, kpdTemp021_High, kpdTemp021_Offset);
            SetTempConfig(tmp022, tbkTemp022_Name, kpdTemp022_Alias, kpdTemp022_Low, kpdTemp022_High, kpdTemp022_Offset);
            SetTempConfig(tmp023, tbkTemp023_Name, kpdTemp023_Alias, kpdTemp023_Low, kpdTemp023_High, kpdTemp023_Offset);
            SetTempConfig(tmp024, tbkTemp024_Name, kpdTemp024_Alias, kpdTemp024_Low, kpdTemp024_High, kpdTemp024_Offset);
            SetTempConfig(tmp025, tbkTemp025_Name, kpdTemp025_Alias, kpdTemp025_Low, kpdTemp025_High, kpdTemp025_Offset);
            SetTempConfig(tmp026, tbkTemp026_Name, kpdTemp026_Alias, kpdTemp026_Low, kpdTemp026_High, kpdTemp026_Offset);

            SetAInConfig(pt050, tbkAIn050_Name, kpdAIn050_Low, kpdAIn050_High);
            SetAInConfig(pt051, tbkAIn051_Name, kpdAIn051_Low, kpdAIn051_High);
            SetAInConfig(pt052, tbkAIn052_Name, kpdAIn052_Low, kpdAIn052_High);
            SetAInConfig(pt053, tbkAIn053_Name, kpdAIn053_Low, kpdAIn053_High);
            SetAInConfig(pt054, tbkAIn054_Name, kpdAIn054_Low, kpdAIn054_High);
            SetAInConfig(pt055, tbkAIn055_Name, kpdAIn055_Low, kpdAIn055_High);

            SetOutConfig(aout000, kpdAOut000_Alias);
            SetOutConfig(aout000RPM, kpdAOut000RPM_Alias);
            SetAOutConfig(aout001, kpdAOut001_Alias, kpdAOut001_Low, kpdAOut001_High);
            SetAOutConfig(aout002, kpdAOut002_Alias, kpdAOut002_Low, kpdAOut002_High);
            SetAOutConfig(aout003, kpdAOut003_Alias, kpdAOut003_Low, kpdAOut003_High);
            SetAOutConfig(aout004, kpdAOut004_Alias, kpdAOut004_Low, kpdAOut004_High);
            SetAOutConfig(aout005, kpdAOut005_Alias, kpdAOut005_Low, kpdAOut005_High);
            SetAOutConfig(aout006, kpdAOut006_Alias, kpdAOut006_Low, kpdAOut006_High);
            SetOutConfig(aout007, kpdAOut007_Alias);
            SetOutConfig(aout008, kpdAOut008_Alias);
            SetOutConfig(aout009, kpdAOut009_Alias);
            SetOutConfig(aout010, kpdAOut010_Alias);

            SetOutConfig(do000, kpdDO000_Alias);
            SetOutConfig(do001, kpdDO001_Alias);
            SetOutConfig(do002, kpdDO002_Alias);
            SetOutConfig(do003, kpdDO003_Alias);
            SetOutConfig(do004, kpdDO004_Alias);
            SetOutConfig(do005, kpdDO005_Alias);
            SetOutConfig(do006, kpdDO006_Alias);
            SetOutConfig(do007, kpdDO007_Alias);
            SetOutConfig(do008, kpdDO008_Alias);
            SetOutConfig(do009, kpdDO009_Alias);
            SetOutConfig(do010, kpdDO010_Alias);
            SetOutConfig(do011, kpdDO011_Alias);
            SetOutConfig(do012, kpdDO012_Alias);
            SetOutConfig(do013, kpdDO013_Alias);
            SetOutConfig(do014, kpdDO014_Alias);
            SetOutConfig(do015, kpdDO015_Alias);
            SetOutConfig(do016, kpdDO016_Alias);
            SetOutConfig(do017, kpdDO017_Alias);
            SetOutConfig(do018, kpdDO018_Alias);
            SetOutConfig(do019, kpdDO019_Alias);
            SetOutConfig(do020, kpdDO020_Alias);
            SetOutConfig(do021, kpdDO021_Alias);
            SetOutConfig(do022, kpdDO022_Alias);                                                // 09/20/23 PS
        }

        private void SetPointConfig(string[] pt, TextBlock tb, Touch.TextBoxKeyboard kpa, ComboBox cbu, ComboBox cbd, Touch.TextBoxKeyboard kpl, Touch.TextBoxKeyboard kph, Touch.TextBoxKeyboard kpm, Touch.TextBoxKeyboard kpo)
        {
            string r = pt[0].Replace("_", "__");
            tb.Text = pt[0];
            kpa.Title = r + strAlias;
            kpa.MaxStringLength = ADS.iMAXLENGTH;
            cbu.ItemsSource = ADS._cmbUnitList;
            cbd.ItemsSource = ADS._cmbDecimalList;
            kpl.Title = r + strLowValue;
            kpl.Decimals = Convert.ToInt32(pt[3]);
            kph.Title = r + strHighValue;
            kph.Decimals = Convert.ToInt32(pt[3]);
            kpm.Title = r + strMinimumValue;
            kpm.Decimals = iMinDec;
            kpo.Title = r + strOffsetValue;
            kpo.Decimals = iMinDec;
        }

        private void SetTempConfig(string[] pt, TextBlock tb, Touch.TextBoxKeyboard kpa, Touch.TextBoxKeyboard kpl, Touch.TextBoxKeyboard kph, Touch.TextBoxKeyboard kpo)
        {
            string r = pt[0].Replace("_", "__");
            tb.Text = pt[0];
            kpa.Title = r + strAlias;
            kpa.MaxStringLength = ADS.iMAXLENGTH;
            kpl.Title = r + strLowValue;
            kpl.Decimals = Convert.ToInt32(pt[3]);
            kph.Title = r + strHighValue;
            kph.Decimals = Convert.ToInt32(pt[3]);
            kpo.Title = r + strOffsetValue;
            kpo.Decimals = iMinDec;
        }

        private void SetAInConfig(string[] pt, TextBlock tb, Touch.TextBoxKeyboard kpl, Touch.TextBoxKeyboard kph)
        {
            string r = pt[0].Replace("_", "__");
            tb.Text = pt[0];
            kpl.Title = r + strLowValue;
            kph.Title = r + strHighValue;
        }

        private void SetAOutConfig(string[] pt, Touch.TextBoxKeyboard kpa, Touch.TextBoxKeyboard kpl, Touch.TextBoxKeyboard kph)
        {
            string r = pt[0].Replace("_", "__");
            kpa.Title = r + strAlias;
            kpa.MaxStringLength = ADS.iMAXLENGTH;
            kpl.Title = r + strLowValue;
            kpl.Decimals = Convert.ToInt32(pt[3]);
            kph.Title = r + strHighValue;
            kph.Decimals = Convert.ToInt32(pt[3]);
        }

        private void SetOutConfig(string[] pt, Touch.TextBoxKeyboard kpd)
        {
            kpd.Title = pt[0].Replace("_", "__") + strAlias;
            kpd.MaxStringLength = ADS.iMAXLENGTH;
        }


        #endregion Configuration

    }
}
