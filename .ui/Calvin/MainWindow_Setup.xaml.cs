// Carbon Capture - Calvin
// MainWindow_Setup.xaml.cs
// Rev 1.00 - September 24, 2023

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Calvin.ConfigManager;
using Calvin.HelpManager;

namespace Calvin
{
    public partial class MainWindow
    {
        private float scPFactor;
        private float scIFactor;
        private float scIRange;

        public void LoadSetupConfiguration()
        {
            App.bKpdCheckOn = false;
            scPFactor = ADS.sPFactor;
            scIFactor = ADS.sIFactor;
            scIRange = ADS.sIRange;
            kpdHT_P_Value.Text = scPFactor.ToString("F0");
            kpdHT_I_Value.Text = scIFactor.ToString("F0");
            kpdHT_I_Range.Text = scIRange.ToString("F0");
            App.bKpdCheckOn = true;
        }

        public void UpdateSetupValues()                                                // 08/14/23 PS
        {
            //App.ButtonEnable(ref btnCartridge, bReady);
            //App.ButtonEnable(ref btnRestartComm, ADS.bRunWOAds && bReady);
            //App.ButtonEnable(ref btnErrorReport, bReady);
            //App.ButtonEnable(ref btnEmailList, bReady);
            //App.ButtonEnable(ref btnEmailPassword, bReady);

            App.ButtonEnable(ref btnCartridge, true);
            App.ButtonEnable(ref btnSchema, true);
            App.ButtonEnable(ref btnRestartComm, ADS.bRunWOAds);
            App.ButtonEnable(ref btnErrorReport, true);
            App.ButtonEnable(ref btnEmailList, true);
            App.ButtonEnable(ref btnEmailPassword, true);
        }

        private void kpdHT_P_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                ADS.sPFactor = Convert.ToSingle(kpdHT_P_Value.Text);
                ADS.SetAdsValue(ADS.GVTtgr + "HT_P_Value", ADS.sPFactor);
                PIDButtonEnable();
            }
        }

        private void kpdHT_I_Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                ADS.sIFactor = Convert.ToSingle(kpdHT_I_Value.Text);
                ADS.SetAdsValue(ADS.GVTtgr + "HT_I_Value", ADS.sIFactor);
                PIDButtonEnable();
            }
        }

        private void kpdHT_I_Range_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                ADS.sIRange = Convert.ToSingle(kpdHT_I_Range.Text);
                ADS.SetAdsValue(ADS.GVTtgr + "HT_I_Range", ADS.sIRange);
                PIDButtonEnable();
            }
        }

        private void btnSavePIDInfo_Click(object sender, RoutedEventArgs e)
        {
            ConfigManager.Config.SaveUserFile();
            scPFactor = ADS.sPFactor;
            scIFactor = ADS.sIFactor;
            scIRange = ADS.sIRange;
            PIDButtonEnable();
        }

        private void PIDButtonEnable()
        {
            bool enabled = scPFactor != ADS.sPFactor || scIFactor != ADS.sIFactor || scIRange != ADS.sIRange;
            App.ButtonEnable(ref btnSavePIDInfo, enabled);
        }

        private void btnRestartComm_Click(object sender, RoutedEventArgs e)
        {
            ADS.bCartridgeSaved = true;
            if (!ADS.TcAdsServices())
            { Process.GetCurrentProcess().Kill(); }
        }

        private void btnErrorReport_Click(object sender, RoutedEventArgs e)
        {
            ReportsManager.Reports.MachineErrorsReport();
        }

        private void btnCartridge_Click(object sender, RoutedEventArgs e)
        {
            Window WinMessage = new CartridgeWindow();
            _ = WinMessage.ShowDialog();
            if (ADS.bCartridgeSaved)
            {
                bool bNew = true;
                int iCartridge = 0;
                for (int i = 0; i < ADS.iCartridgeCount; i++)
                {
                    iCartridge = i + 1;
                    if (ADS.strCartridge == ADS.strCartridgeName[i])
                    {
                        bNew = false;
                        App.WPFMessageBoxOK("Cartridge name in use", App.bgGray);
                        break;
                    }
                }
                if (bNew)
                {
                    ADS.strCartridgeName[iCartridge] = ADS.strCartridge;
                    ADS.iCartridgeCount++;
                    Config.SaveCartridges();
                    ADS.LoadComboCartridges();
                }
            }
        }

        private void btnSchema_Click(object sender, RoutedEventArgs e)
        {
            Window WinMessage = new SchemaWindow();
            _ = WinMessage.ShowDialog();
            if (ADS.bSchemaSaved)
            {
                bool bNew = true;
                int iSchema = 0;
                for (int i = 0; i < ADS.iSchemaCount; i++)
                {
                    iSchema = i + 1;
                    if (ADS.strSchema == ADS.strSchemaName[i])
                    {
                        bNew = false;
                        App.WPFMessageBoxOK("Schema name in use", App.bgGray);
                        break;
                    }
                }
                if (bNew)
                {
                    ADS.strSchemaName[iSchema] = ADS.strSchema;
                    ADS.iSchemaCount++;
                    Config.SaveSchemas();
                    ADS.LoadComboSchemas();
                }
            }
        }

        private void btnEmailList_Click(object sender, RoutedEventArgs e)
        {
            Window Win = new EmailListWindow();
            _ = Win.ShowDialog();
        }

        private void btnEmailPassword_Click(object sender, RoutedEventArgs e)
        {
            Window WinMessage = new PasswordWindow();
            _ = WinMessage.ShowDialog();
        }
    }
}

