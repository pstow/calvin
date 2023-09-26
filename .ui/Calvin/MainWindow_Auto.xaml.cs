// Carbon Capture - Calvin
// MainWindow_Auto.xaml.cs
// Rev 1.00 - August 20, 2023

using Calvin.ConfigManager;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Calvin
{
    public partial class MainWindow
    {
        #region Variables

        private const short cWarmup = 1;
        private const short cEvacuation = 2;
        private const short cSteamBypass = 3;
        private const short cDesorption = 4;
        private const short cRepressurization = 5;
        private const short cAdsorption = 6;
        private const string strLogValues = "Log values";
        private const string strCO2 = "CO" + "\u2082";
        private const string strProcessAWS = "Process AWS data";

        private bool bReady1;
        private TimeSpan tSpan;
        private float sSPAdsorptionTime;
        private float sSPAdsorptionCO2;
        private float sSPAdsorptionCO2Time;
        private float sSPBoilerPressure;
        private float sSPMinLRPCoolingLoopFlow;
        private float sSPEvacuationTargetPressure;
        private float sSPEvacuationTargetPressureSteam;                                                                     // 07/14/23 PS - New set point
        private float sSPMaxAllowedPressureLeakage;
        private float sSPReactorRepressurizationPressure;
        private float sSPAdsorptionTemp;
        private float sSPAdsorptionVFDOutput;
        private float sSPMinBypassSteamFlow;
        private float sSPMinBypassSteamTemp;
        private float sSPReactorPressureLow;
        private float sSPReactorPressureHigh;
        private float sSPReactorValveLowPos;
        private float sSPReactorValveHighPos;
        private float sSPSteamValvePercentOpen;
        private float sSPMaxAllowedSorbentTemp;
        private float sSPSteamRepressurizationPressure;
        private float sSPSteamPurgeTime;
        private float sSPSteamPurgeCutoffTemp;
        private float sSPEvapCoolingCutoffTemp;
        private float sSPEvapCoolingTargetPressure;
        private float sSPLeakCheckTime;

        private readonly string strAdsorptionTime = "Adsorption Time";
        private readonly string strAdsorptionCO2 = "Adsorption " + strCO2;
        private readonly string strAdsorptionCO2Time = "Adsorption " + strCO2 + " Time";
        private readonly string strBoilerPressure = "Boiler Pressure";
        private readonly string strMinLRPCoolingLoopFlow = "Min LRP Cooling Loop Flow";
        private readonly string strEvacuationTargetPressure = "Evacuation Target Pressure";
        private readonly string strEvacuationTargetPressureSteam = "Evacuation Target Pressure Steam";                      // 07/14/23 PS - New set point
        private readonly string strMaxAllowedPressureLeakage = "Maximum Allow Pressure Leakage";
        private readonly string strReactorRepressurizationPressure = "Reactor Repressurization Pressure";
        private readonly string strAdsorptionTemp = "Adsorption Temperature";
        private readonly string strAdsorptionVFDOutput = "Adsorption VFD Output";
        private readonly string strMinBypassSteamFlow = "Min Bypass Steam Flow";
        private readonly string strMinBypassSteamTemp = "Min Bypass Steam Temperature";
        private readonly string strReactorPressureLow = "Reactor Low Pressure";
        private readonly string strReactorPressureHigh = "Reactor High Pressure";
        private readonly string strReactorValveLowPos = "Reactor Valve Low Position";
        private readonly string strReactorValveHighPos = "Reactor Valve High Position";
        private readonly string strSteamValvePercentOpen = "Steam Valve Percent Open";
        private readonly string strMaxAllowedSorbentTemp = "Max Sorbent Temperature";
        private readonly string strSteamRepressurizationPressure = "Steam Repressurization Pressure";
        private readonly string strSteamPurgeTime = "Steam Purge Time";
        private readonly string strSteamPurgeCutoffTemp = "Steam Purge Cutoff Temperature";
        private readonly string strEvapCoolingCutoffTemp = "Evap Cooling Cutoff Temperature";
        private readonly string strEvapCoolingTargetPressure = "Evap Cooling Target Pressure";
        private readonly string strLeakCheckTime = "Leak Check Time";
        private readonly string strCyclesToRun = "Cycles to Run";

        #endregion Variables

        #region Updating

        public void UpdateAutoValues()
        {
            bReady1 = bReady && ADS.iAlarm == 0 && !ADS.bTestRunning && !ADS.bUploading;
            lblStatus.Content = ADS.iAlarm == 0 ? ADS.strStatusLabel : ADS.bHalfSecPulse ? ADS.strStatusLabel : "";
            lblStatus.Foreground = App.LabelStatus(ADS.iAlarm > 0, bReady);

            tSpan = TimeSpan.FromMilliseconds(ADS.sRemainingTime);
            lblStatus_Step.Content = ADS.bUploading ? "Uploading log" : "Step " + ADS.iCurrentStep.ToString() + " - " + tSpan.ToString(@"hh\:mm\:ss");
            lblStatus_Step.Visibility = ADS.iCurrentStep > 0 || ADS.bUploading ? Visibility.Visible : Visibility.Hidden;

            labWarmup.Foreground = App.WarmupState(ADS.bWarmupComplete, ADS.iState == cWarmup);
            App.ButtonEnable(ref btnWarmup, bReady1);
            //App.ButtonEnable(ref btnWarmup, bReady && !ADS.bWarmupComplete);

            labEvacuation.Foreground = App.ActiveState(ADS.iState == cEvacuation);
            App.ButtonEnable(ref btnEvacuation, bReady1);

            labSteamBypass.Foreground = App.ActiveState(ADS.iState == cSteamBypass);
            App.ButtonEnable(ref btnSteamBypass, bReady1);

            labDesorption.Foreground = App.ActiveState(ADS.iState == cDesorption);
            App.ButtonEnable(ref btnDesorption, bReady1);

            labRepressurization.Foreground = App.ActiveState(ADS.iState == cRepressurization);
            App.ButtonEnable(ref btnRepressurization, bReady1);

            labAdsorption.Foreground = App.ActiveState(ADS.iState == cAdsorption);
            labAdsorption.Content = ADS.bAdsorpTimeCO2 ? "Adsorption - " + strCO2 : "Adsorption - Time";
            App.ButtonEnable(ref btnAdsorption, bReady1);
            App.ButtonEnable(ref btnAdsorpTimeCO2, bReady);
            App.KpdBoldBlue(ref kpdSPAdsorptionTime, !ADS.bAdsorpTimeCO2);
            App.KpdBoldBlue(ref kpdSPAdsorptionCO2, ADS.bAdsorpTimeCO2);
            App.KpdBoldBlue(ref kpdSPAdsorptionCO2Time, ADS.bAdsorpTimeCO2);

            App.ButtonEnable(ref btnTVSAStart, bReady1);
            App.ButtonEnable(ref btnTVSACancel, ADS.bInProcess && (ADS.iCycleCount + 1 < ADS.iCyclesToRun));

            App.ButtonEnable(ref btnStopProcess, ADS.bInProcess && !ADS.bAdsDown);

            kpdCyclesToRun.IsEnabled = bReady1;

            cmbCartridges.IsEnabled = bReady1;

            App.ButtonContentEnable(ref btnLoggingStopStart, bReady && !ADS.bUploading, ADS.iLogging > 0, strLoggingOff, strLoggingOn);
            App.ButtonContentEnable(ref btnLoggingStopStart_copy, bReady && !ADS.bUploading, ADS.iLogging > 0, strLoggingOff, strLoggingOn);

            lblCycleCount.Content = ADS.iCycleCount;
            lblTotalCycleCount.Content = ADS.iTotalCycleCount;
            lblCartridgeCycleCount.Content = ADS.iCartridgeCycles[ADS.iCartridgePointer];

            kpdSPBoilerPressure.Decimals = Convert.ToInt32(ADS.AIn014[3]);
            kpdSPBoilerPressure.Title = Units.KeyboardTitle(strBoilerPressure + strSetpoint, ADS.sBOILER_PRESSURE_MIN, ADS.sBOILER_PRESSURE_MAX, ADS.AIn014[3], ADS.AIn014[2]);
            labSPBoilerPressureUnits.Content = Units.UnitString(ADS.AIn014);

            kpdSPMinLRPCoolingLoopFlow.Decimals = Convert.ToInt32(ADS.AIn022[3]);
            kpdSPMinLRPCoolingLoopFlow.Title = Units.KeyboardTitle(strMinLRPCoolingLoopFlow + strSetpoint, ADS.AIn022[4], ADS.AIn022[5], ADS.AIn022[3], ADS.AIn022[2]);
            labSPMinLRPCoolingLoopFlow.Content = Units.UnitString(ADS.AIn022);

            kpdSPEvacuationTargetPressure.Decimals = Convert.ToInt32(ADS.AIn020[3]);
            kpdSPEvacuationTargetPressure.Title = Units.KeyboardTitle(strEvacuationTargetPressure + strSetpoint, ADS.AIn020[4], ADS.AIn020[5], ADS.AIn020[3], ADS.AIn020[2]);
            labSPEvacuationTargetPressure.Content = Units.UnitString(ADS.AIn020);

            kpdSPEvacuationTargetPressureSteam.Decimals = Convert.ToInt32(ADS.AIn020[3]);
            kpdSPEvacuationTargetPressureSteam.Title = Units.KeyboardTitle(strEvacuationTargetPressure + strSetpoint, ADS.AIn020[4], ADS.AIn020[5], ADS.AIn020[3], ADS.AIn020[2]);
            labSPEvacuationTargetPressureSteam.Content = Units.UnitString(ADS.AIn020);

            kpdSPMaxAllowedPressureLeakage.Decimals = Convert.ToInt32(ADS.AIn006[3]);
            kpdSPMaxAllowedPressureLeakage.Title = Units.KeyboardTitle(strMaxAllowedPressureLeakage + strSetpoint, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3], ADS.AIn006[2]);
            labSPMaxAllowedPressureLeakage.Content = Units.UnitString(ADS.AIn006);

            kpdSPReactorRepressurizationPressure.Decimals = Convert.ToInt32(ADS.AIn006[3]);
            kpdSPReactorRepressurizationPressure.Title = Units.KeyboardTitle(strReactorRepressurizationPressure + strSetpoint, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3], ADS.AIn006[2]);
            labSPReactorRepressurizationPressure.Content = Units.UnitString(ADS.AIn006);

            kpdSPMinBypassSteamFlow.Decimals = Convert.ToInt32(ADS.AIn015[3]);
            kpdSPMinBypassSteamFlow.Title = Units.KeyboardTitle(strMinBypassSteamFlow + strSetpoint, ADS.AIn015[4], ADS.AIn015[5], ADS.AIn015[3], ADS.AIn015[2]);
            labSPMinBypassSteamFlow.Content = Units.UnitString(ADS.AIn015);

            kpdSPReactorPressureLow.Decimals = Convert.ToInt32(ADS.AIn006[3]);
            kpdSPReactorPressureLow.Title = Units.KeyboardTitle(strReactorPressureLow + strSetpoint, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3], ADS.AIn006[2]);
            labReactorLowPressure.Content = Units.UnitString(ADS.AIn006);

            kpdSPReactorPressureHigh.Decimals = Convert.ToInt32(ADS.AIn006[3]);
            kpdSPReactorPressureHigh.Title = Units.KeyboardTitle(strReactorPressureHigh + strSetpoint, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3], ADS.AIn006[2]);
            labReactorHighPressure.Content = Units.UnitString(ADS.AIn006);

            kpdSPSteamRepressurizationPressure.Decimals = Convert.ToInt32(ADS.AIn006[3]);
            kpdSPSteamRepressurizationPressure.Title = Units.KeyboardTitle(strSteamRepressurizationPressure + strSetpoint, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3], ADS.AIn006[2]);
            labSteamRepressurizationPressure.Content = Units.UnitString(ADS.AIn006);

            kpdSPEvapCoolingTargetPressure.Decimals = Convert.ToInt32(ADS.AIn006[3]);
            kpdSPEvapCoolingTargetPressure.Title = Units.KeyboardTitle(strEvapCoolingTargetPressure + strSetpoint, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3], ADS.AIn006[2]);
            labEvapCoolingTargetPressure.Content = Units.UnitString(ADS.AIn006);

            bool bTempReady = bReady;                                                                                       // 08/14/23 PS
            bReady = true;                                                                                                  // 08/14/23 PS
            kpdSPAdsorptionCO2.IsEnabled = bReady;                                                                          // 07/21/23 PS
            kpdSPAdsorptionCO2Time.IsEnabled = bReady;                                                                      // 07/21/23 PS
            kpdSPAdsorptionTemp.IsEnabled = bReady;                                                                         // 07/21/23 PS
            kpdSPAdsorptionTime.IsEnabled = bReady;                                                                         // 07/21/23 PS
            kpdSPAdsorptionVFDOutput.IsEnabled = bReady;                                                                    // 07/21/23 PS
            kpdSPBoilerPressure.IsEnabled = bReady;                                                                         // 07/21/23 PS
            kpdSPEvacuationTargetPressure.IsEnabled = bReady;                                                               // 07/21/23 PS
            kpdSPEvacuationTargetPressureSteam.IsEnabled = bReady;                                                          // 07/21/23 PS
            kpdSPEvapCoolingCutoffTemp.IsEnabled = bReady;                                                                  // 07/21/23 PS
            kpdSPEvapCoolingTargetPressure.IsEnabled = bReady;                                                              // 07/21/23 PS
            kpdSPLeakCheckTime.IsEnabled = bReady;                                                                          // 07/21/23 PS
            kpdSPMaxAllowedPressureLeakage.IsEnabled = bReady;                                                              // 07/21/23 PS
            kpdSPMaxAllowedSorbentTemp.IsEnabled = bReady;                                                                  // 07/21/23 PS
            kpdSPMinBypassSteamFlow.IsEnabled = bReady;                                                                     // 07/21/23 PS
            kpdSPMinBypassSteamTemp.IsEnabled = bReady;                                                                     // 07/21/23 PS
            kpdSPMinLRPCoolingLoopFlow.IsEnabled = bReady;                                                                  // 07/21/23 PS
            kpdSPReactorHighValve.IsEnabled = bReady;                                                                       // 07/21/23 PS
            kpdSPReactorLowValve.IsEnabled = bReady;                                                                        // 07/21/23 PS
            kpdSPReactorPressureHigh.IsEnabled = bReady;                                                                    // 07/21/23 PS
            kpdSPReactorPressureLow.IsEnabled = bReady;                                                                     // 07/21/23 PS
            kpdSPReactorRepressurizationPressure.IsEnabled = bReady;                                                        // 07/21/23 PS
            kpdSPSteamPurgeCutoffTemp.IsEnabled = bReady;                                                                   // 07/21/23 PS
            kpdSPSteamPurgeTime.IsEnabled = bReady;                                                                         // 07/21/23 PS
            kpdSPSteamRepressurizationPressure.IsEnabled = bReady;                                                          // 07/21/23 PS
            kpdSPSteamValvePercentOpen.IsEnabled = bReady;                                                                  // 07/21/23 PS
            bReady = bTempReady;                                                                                            // 08/14/23 PS

            App.ButtonBlinkEnable(ref btnSaveAdsorptionSetpoints, CheckTimeCO2Values());
            App.ButtonBlinkEnable(ref btnSaveSetpoints, CheckSPValues());
            App.ButtonEnable(ref btnResetSetpoints, btnSaveSetpoints.IsEnabled);
        }

        private bool CheckTimeCO2Values()
        {
            if (sSPAdsorptionTime != ADS.sSPAdsorptionTime) { return true; }
            if (sSPAdsorptionCO2 != ADS.sSPAdsorptionCO2) { return true; }
            if (sSPAdsorptionCO2Time != ADS.sSPAdsorptionCO2Time) { return true; }
            return false;
        }

        private bool CheckSPValues()
        {
            if (sSPBoilerPressure != ADS.sSPBoilerPressure) { return true; }
            if (sSPMinLRPCoolingLoopFlow != ADS.sSPMinLRPCoolingLoopFlow) { return true; }
            if (sSPEvacuationTargetPressure != ADS.sSPEvacuationTargetPressure) { return true; }
            if (sSPEvacuationTargetPressureSteam != ADS.sSPEvacuationTargetPressureSteam) { return true; }                  // 07/14/23 PS - New set point
            if (sSPMaxAllowedPressureLeakage != ADS.sSPMaxAllowedPressureLeakage) { return true; }
            if (sSPReactorRepressurizationPressure != ADS.sSPReactorRepressurizationPressure) { return true; }
            if (sSPAdsorptionTemp != ADS.sSPAdsorptionTemp) { return true; }
            if (sSPAdsorptionVFDOutput != ADS.sSPAdsorptionVFDOutput) { return true; }
            if (sSPMinBypassSteamFlow != ADS.sSPMinBypassSteamFlow) { return true; }
            if (sSPMinBypassSteamTemp != ADS.sSPMinBypassSteamTemp) { return true; }
            if (sSPReactorPressureLow != ADS.sSPReactorPressureLow) { return true; }
            if (sSPReactorPressureHigh != ADS.sSPReactorPressureHigh) { return true; }
            if (sSPReactorValveLowPos != ADS.sSPReactorValveLowPos) { return true; }
            if (sSPReactorValveHighPos != ADS.sSPReactorValveHighPos) { return true; }
            if (sSPSteamValvePercentOpen != ADS.sSPSteamValvePercentOpen) { return true; }
            if (sSPMaxAllowedSorbentTemp != ADS.sSPMaxAllowedSorbentTemp) { return true; }
            if (sSPSteamRepressurizationPressure != ADS.sSPSteamRepressurizationPressure) { return true; }
            if (sSPSteamPurgeTime != ADS.sSPSteamPurgeTime) { return true; }
            if (sSPSteamPurgeCutoffTemp != ADS.sSPSteamPurgeCutoffTemp) { return true; }
            if (sSPEvapCoolingCutoffTemp != ADS.sSPEvapCoolingCutoffTemp) { return true; }
            if (sSPEvapCoolingTargetPressure != ADS.sSPEvapCoolingTargetPressure) { return true; }
            if (sSPLeakCheckTime != ADS.sSPLeakCheckTime) { return true; }
            return false;
        }

        #endregion Updating

        #region Buttons

        private void cmbCartridges_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ADS.bCartridgeSaved)
            {
                ADS.strCartridge = (string)cmbCartridges.SelectedItem;
                ADS.iCartridgePointer = cmbCartridges.SelectedIndex;
                Config.SaveUserFile();
            }
            else
            {
                LoadCartridgeCombobox();
                ADS.bCartridgeSaved = false;
            }
        }

        private void cmbSchemas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ADS.bSchemaSaved)
            {
                ADS.strSchema = (string)cmbSchemas.SelectedItem;
                ADS.iSchemaPointer = cmbSchemas.SelectedIndex;
                Config.SaveUserFile();
            }
            else
            {
                LoadSchemaCombobox();
                ADS.bSchemaSaved = false;
            }
        }

        private void btnWarmup_Click(object sender, RoutedEventArgs e)                                                      // 08/20/23 PS
        {
            SetState((short)LogType.Warmup, cWarmup);
        }

        private void btnEvacuation_Click(object sender, RoutedEventArgs e)                                                  // 08/20/23 PS
        {
            SetState((short)LogType.Evacuation, cEvacuation);
        }

        private void btnSteamBypass_Click(object sender, RoutedEventArgs e)                                                 // 08/20/23 PS
        {
            SetState((short)LogType.SteamBypass, cSteamBypass);
        }

        private void btnDesorption_Click(object sender, RoutedEventArgs e)                                                  // 08/20/23 PS
        {
            SetState((short)LogType.Desorption, cDesorption);
        }

        private void btnRepressurization_Click(object sender, RoutedEventArgs e)                                            // 08/20/23 PS
        {
            SetState((short)LogType.Repressurization, cRepressurization);
        }

        private void btnAdsorption_Click(object sender, RoutedEventArgs e)                                                  // 08/20/23 PS
        {
            SetState((short)LogType.Adsorption, cAdsorption);
        }

        private void SetState(short log, int state)                                                                         // 08/20/23 PS
        {
            int response = App.WPFMessageBoxYesNoCancel(strLogValues, App.bgGray);
            if (response != (int)MsgResponse.Cancel)
            {
                if (response == (int)MsgResponse.Yes)
                {
                    ADS.bProcessData = App.WPFMessageBoxYesNo(strProcessAWS, App.bgGray);
                    ADS.iLogging = (short)log;
                }
                ADS.iState = (short)state;
                ADS.SetAdsValue(ADS.GVTtgi + "Machine_State", ADS.iState);
            }
        }

        private void btnAdsorpTimeCO2_Click(object sender, RoutedEventArgs e)
        {
            ADS.bAdsorpTimeCO2 = !ADS.bAdsorpTimeCO2;
            ADS.SetAdsValue(ADS.GVTtgb + "Adsorption_CO2", ADS.bAdsorpTimeCO2);
        }

        private void btnTVSAStart_Click(object sender, RoutedEventArgs e)
        {
            int result = App.WPFMessageCommentBoxYesNoCancel("Enter optional cycle comment.  Process data", App.bgGray);
            if (result != (int)MsgResponse.Cancel)
            {
                if (result == (int)MsgResponse.Yes)
                {
                    ADS.bProcessData = true;
                }
                ADS.iLogging = (short)LogType.Cycle;
                ADS.bCycleLoggingOn = true;
                ADS.SetAdsValue(ADS.GVTtgi + "Cycle_Count", (short)0);
                ADS.SetAdsValue(ADS.GVTtgb + "Manual_Heater_Mode", false);
                ADS.SetAdsValue(ADS.GVTtgi + "Machine_State", cEvacuation);
                ADS.SetAdsValue(ADS.GVTtgb + "Auto_Mode", true);
            }
        }

        private void btnTVSACancel_Click(object sender, RoutedEventArgs e)
        {
            if (App.WPFMessageBoxYesNo("Cancel TVSA after current cycle", App.bgGray))
            {
                ADS.iCyclesToRun = (short)(ADS.iCycleCount + 1);
                ADS.SetAdsValue(ADS.GVTtgi + "Cycles_To_Run", ADS.iCyclesToRun);
                App.bKpdCheckOn = false;
                kpdCyclesToRun.Text = ADS.iCyclesToRun.ToString();
                App.bKpdCheckOn = true;
            }
        }

        private void btnStopProcess_Click(object sender, RoutedEventArgs e)
        {
            ADS.SetAdsValue(ADS.GVTtgb + "Stop_Cycle", true);
        }

        private void btnSaveAdsorptionSetpoints_Click(object sender, RoutedEventArgs e)
        {
            ADS.sSPAdsorptionTime = sSPAdsorptionTime;
            ADS.sSPAdsorptionCO2 = sSPAdsorptionCO2;
            ADS.sSPAdsorptionCO2Time = sSPAdsorptionCO2Time;
            Config.SaveUserFile();
            ADS.SetTCSPValues();
        }

        private void btnSaveSetpoints_Click(object sender, RoutedEventArgs e)
        {
            ADS.sSPBoilerPressure = sSPBoilerPressure;
            ADS.sSPMinLRPCoolingLoopFlow = sSPMinLRPCoolingLoopFlow;
            ADS.sSPEvacuationTargetPressure = sSPEvacuationTargetPressure;
            ADS.sSPEvacuationTargetPressureSteam = sSPEvacuationTargetPressureSteam;                                        // 07/14/23 PS - New set point
            ADS.sSPMaxAllowedPressureLeakage = sSPMaxAllowedPressureLeakage;
            ADS.sSPReactorRepressurizationPressure = sSPReactorRepressurizationPressure;
            ADS.sSPAdsorptionTemp = sSPAdsorptionTemp;
            ADS.sSPAdsorptionVFDOutput = sSPAdsorptionVFDOutput;
            ADS.sSPMinBypassSteamFlow = sSPMinBypassSteamFlow;
            ADS.sSPMinBypassSteamTemp = sSPMinBypassSteamTemp;
            ADS.sSPReactorPressureLow = sSPReactorPressureLow;
            ADS.sSPReactorPressureHigh = sSPReactorPressureHigh;
            ADS.sSPReactorValveLowPos = sSPReactorValveLowPos;
            ADS.sSPReactorValveHighPos = sSPReactorValveHighPos;
            ADS.sSPSteamValvePercentOpen = sSPSteamValvePercentOpen;
            ADS.sSPMaxAllowedSorbentTemp = sSPMaxAllowedSorbentTemp;
            ADS.sSPSteamRepressurizationPressure = sSPSteamRepressurizationPressure;
            ADS.sSPSteamPurgeTime = sSPSteamPurgeTime;
            ADS.sSPSteamPurgeCutoffTemp = sSPSteamPurgeCutoffTemp;
            ADS.sSPEvapCoolingCutoffTemp = sSPEvapCoolingCutoffTemp;
            ADS.sSPEvapCoolingTargetPressure = sSPEvapCoolingTargetPressure;
            ADS.sSPLeakCheckTime = sSPLeakCheckTime;
            Config.SaveUserFile();
            ADS.SetTCSPValues();
        }

        private void btnResetSetpoints_Click(object sender, RoutedEventArgs e)
        {
            LoadSetpoints();
            LoadSetpointKeypads();
        }

        #endregion Buttons

        #region Keypad Entry

        private void kpdCyclesToRun_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strCyclesToRun, 0, kpdCyclesToRun.Text, ADS.iCYCLES_MIN, ADS.iCYCLEs_MAX, 0))
                {
                    ADS.iCyclesToRun = (short)Convert.ToInt16(kpdCyclesToRun.Text);
                    ADS.SetAdsValue(ADS.GVTtgi + "Cycles_To_Run", ADS.iCyclesToRun);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdCyclesToRun.Text = ADS.iCyclesToRun.ToString();
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPAdsorptionTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strAdsorptionTime + strSetpoint, 17, kpdSPAdsorptionTime.Text, ADS.sSP_TIME_MIN, ADS.sSP_TIME_MAX, 0))
                {
                    sSPAdsorptionTime = Convert.ToSingle(kpdSPAdsorptionTime.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPAdsorptionTime.Text = sSPAdsorptionTime.ToString();
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPAdsorptionCO2Time_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strAdsorptionCO2Time + strSetpoint, 17, kpdSPAdsorptionCO2Time.Text, ADS.sSP_TIME_MIN, ADS.sSP_TIME_MAX, 0))
                {
                    sSPAdsorptionCO2Time = Convert.ToSingle(kpdSPAdsorptionCO2Time.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPAdsorptionCO2Time.Text = sSPAdsorptionCO2Time.ToString();
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPAdsorptionCO2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strAdsorptionCO2 + strSetpoint, 4, kpdSPAdsorptionCO2.Text, ADS.sSP_CO2_MIN, ADS.sSP_CO2_MAX, 0))
                {
                    sSPAdsorptionCO2 = Convert.ToSingle(kpdSPAdsorptionCO2.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPAdsorptionCO2.Text = sSPAdsorptionCO2.ToString();
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPBoilerPressure_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strBoilerPressure + strSetpoint, ADS.AIn014[2], kpdSPBoilerPressure.Text, ADS.sBOILER_PRESSURE_MIN, ADS.sBOILER_PRESSURE_MAX, ADS.AIn014[3]))
                {
                    sSPBoilerPressure = Convert.ToSingle(kpdSPBoilerPressure.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPBoilerPressure.Text = Units.strFormat(sSPBoilerPressure, ADS.AIn014[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPMinLRPCoolingLoopFlow_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strMinLRPCoolingLoopFlow + strSetpoint, ADS.AIn022[2], kpdSPMinLRPCoolingLoopFlow.Text, ADS.AIn022[4], ADS.AIn022[5], ADS.AIn022[3]))
                {
                    sSPMinLRPCoolingLoopFlow = Convert.ToSingle(kpdSPMinLRPCoolingLoopFlow.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPMinLRPCoolingLoopFlow.Text = Units.strFormat(sSPMinLRPCoolingLoopFlow, ADS.AIn022[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPEvacuationTargetPressure_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strEvacuationTargetPressure + strSetpoint, ADS.AIn020[2], kpdSPEvacuationTargetPressure.Text, ADS.AIn020[4], ADS.AIn020[5], ADS.AIn020[3]))
                {
                    sSPEvacuationTargetPressure = Convert.ToSingle(kpdSPEvacuationTargetPressure.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPEvacuationTargetPressure.Text = Units.strFormat(sSPEvacuationTargetPressure, ADS.AIn020[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPEvacuationTargetPressureSteam_TextChanged(object sender, TextChangedEventArgs e)                  // 07/14/23 PS - New set point
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strEvacuationTargetPressureSteam + strSetpoint, ADS.AIn020[2], kpdSPEvacuationTargetPressureSteam.Text, ADS.AIn020[4], ADS.AIn020[5], ADS.AIn020[3]))
                {
                    sSPEvacuationTargetPressureSteam = Convert.ToSingle(kpdSPEvacuationTargetPressureSteam.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPEvacuationTargetPressureSteam.Text = Units.strFormat(sSPEvacuationTargetPressureSteam, ADS.AIn020[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPMaxAllowedPressureLeakage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strMaxAllowedPressureLeakage + strSetpoint, ADS.AIn006[2], kpdSPMaxAllowedPressureLeakage.Text, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3]))
                {
                    sSPMaxAllowedPressureLeakage = Convert.ToSingle(kpdSPMaxAllowedPressureLeakage.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPMaxAllowedPressureLeakage.Text = Units.strFormat(sSPMaxAllowedPressureLeakage, ADS.AIn006[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPReactorRepressurizationPressure_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strReactorRepressurizationPressure + strSetpoint, ADS.AIn006[2], kpdSPReactorRepressurizationPressure.Text, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3]))
                {
                    sSPReactorRepressurizationPressure = Convert.ToSingle(kpdSPReactorRepressurizationPressure.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPReactorRepressurizationPressure.Text = Units.strFormat(sSPReactorRepressurizationPressure, ADS.AIn006[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPAdsorptionTemp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strAdsorptionTemp + strSetpoint, 1, kpdSPAdsorptionTemp.Text, ADS.sSP_TEMP_MIN, ADS.sSP_TEMP_MAX, 1))
                {
                    sSPAdsorptionTemp = Convert.ToSingle(kpdSPAdsorptionTemp.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPAdsorptionTemp.Text = sSPAdsorptionTemp.ToString("F1");
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPAdsorptionVFDOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strMinBypassSteamFlow + strSetpoint, 13, kpdSPAdsorptionVFDOutput.Text, 1, 100, 1))
                {
                    sSPAdsorptionVFDOutput = Convert.ToSingle(kpdSPAdsorptionVFDOutput.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPAdsorptionVFDOutput.Text = sSPAdsorptionVFDOutput.ToString("F1");
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPMinBypassSteamFlow_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strMinBypassSteamFlow + strSetpoint, ADS.AIn015[2], kpdSPMinBypassSteamFlow.Text, ADS.AIn015[4], ADS.AIn015[5], ADS.AIn015[3]))
                {
                    sSPMinBypassSteamFlow = Convert.ToSingle(kpdSPMinBypassSteamFlow.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPMinBypassSteamFlow.Text = Units.strFormat(sSPMinBypassSteamFlow, ADS.AIn015[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPMinBypassSteamTemp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strMinBypassSteamTemp + strSetpoint, 1, kpdSPMinBypassSteamTemp.Text, ADS.sSP_TEMP_MIN, ADS.sSP_TEMP_MAX, 1))
                {
                    sSPMinBypassSteamTemp = Convert.ToSingle(kpdSPMinBypassSteamTemp.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPMinBypassSteamTemp.Text = Units.strFormat(sSPMinBypassSteamTemp, 1);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPReactorPressureLow_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strReactorPressureLow + strSetpoint, ADS.AIn006[2], kpdSPReactorPressureLow.Text, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3]))
                {
                    sSPReactorPressureLow = Convert.ToSingle(kpdSPReactorPressureLow.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPReactorPressureLow.Text = Units.strFormat(sSPReactorPressureLow, ADS.AIn006[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPReactorPressureHigh_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strReactorPressureHigh + strSetpoint, ADS.AIn006[2], kpdSPReactorPressureHigh.Text, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3]))
                {
                    sSPReactorPressureHigh = Convert.ToSingle(kpdSPReactorPressureHigh.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPReactorPressureHigh.Text = Units.strFormat(sSPReactorPressureHigh, ADS.AIn006[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPReactorLowValve_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strReactorValveLowPos + strSetpoint, 6, kpdSPReactorLowValve.Text, 0, 100, 1))
                {
                    sSPReactorValveLowPos = Convert.ToSingle(kpdSPReactorLowValve.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPReactorLowValve.Text = Units.strFormat(sSPReactorValveLowPos, 1);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPReactorHighValve_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strReactorValveHighPos + strSetpoint, 6, kpdSPReactorHighValve.Text, 0, 100, 1))
                {
                    sSPReactorValveHighPos = Convert.ToSingle(kpdSPReactorHighValve.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPReactorHighValve.Text = Units.strFormat(sSPReactorValveHighPos, 1);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPSteamValvePercentOpen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strSteamValvePercentOpen + strSetpoint, 6, kpdSPSteamValvePercentOpen.Text, 0, 100, 1))
                {
                    sSPSteamValvePercentOpen = Convert.ToSingle(kpdSPSteamValvePercentOpen.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPSteamValvePercentOpen.Text = sSPSteamValvePercentOpen.ToString("F1");
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPMaxAllowedSorbentTemp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strMaxAllowedSorbentTemp + strSetpoint, 1, kpdSPMaxAllowedSorbentTemp.Text, 0, 200, 1))
                {
                    sSPMaxAllowedSorbentTemp = Convert.ToSingle(kpdSPMaxAllowedSorbentTemp.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPMaxAllowedSorbentTemp.Text = sSPMaxAllowedSorbentTemp.ToString("F1");
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPSteamRepressurizationPressure_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strSteamRepressurizationPressure + strSetpoint, ADS.AIn006[2], kpdSPSteamRepressurizationPressure.Text, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3]))
                {
                    sSPSteamRepressurizationPressure = Convert.ToSingle(kpdSPSteamRepressurizationPressure.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPSteamRepressurizationPressure.Text = Units.strFormat(sSPSteamRepressurizationPressure, ADS.AIn006[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPSteamPurgeTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strSteamPurgeTime + strSetpoint, 17, kpdSPSteamPurgeTime.Text, 1, 1000, 1))
                {
                    sSPSteamPurgeTime = Convert.ToSingle(kpdSPSteamPurgeTime.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPSteamPurgeTime.Text = sSPSteamPurgeTime.ToString("F1");
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPLeakCheckTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strLeakCheckTime + strSetpoint, 20, kpdSPLeakCheckTime.Text, 1, 1000, 0))
                {
                    sSPLeakCheckTime = Convert.ToSingle(kpdSPLeakCheckTime.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPLeakCheckTime.Text = sSPLeakCheckTime.ToString("F0");
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPSteamPurgeCutoffTemp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strSteamPurgeCutoffTemp + strSetpoint, 1, kpdSPSteamPurgeCutoffTemp.Text, 1, 400, 1))
                {
                    sSPSteamPurgeCutoffTemp = Convert.ToSingle(kpdSPSteamPurgeCutoffTemp.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPSteamPurgeCutoffTemp.Text = sSPSteamPurgeCutoffTemp.ToString("F1");
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPEvapCoolingCutoffTemp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strEvapCoolingCutoffTemp + strSetpoint, 1, kpdSPEvapCoolingCutoffTemp.Text, ADS.sSP_TEMP_MIN, ADS.sSP_TEMP_MAX, 1))
                {
                    sSPEvapCoolingCutoffTemp = Convert.ToSingle(kpdSPEvapCoolingCutoffTemp.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPEvapCoolingCutoffTemp.Text = sSPEvapCoolingCutoffTemp.ToString("F1");
                    App.bKpdCheckOn = true;
                }
            }
        }

        private void kpdSPEvapCoolingTargetPressure_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (App.bKpdCheckOn)
            {
                if (App.Test_MinMax(strEvapCoolingTargetPressure + strSetpoint, ADS.AIn006[2], kpdSPEvapCoolingTargetPressure.Text, ADS.AIn006[4], ADS.AIn006[5], ADS.AIn006[3]))
                {
                    sSPEvapCoolingTargetPressure = Convert.ToSingle(kpdSPEvapCoolingTargetPressure.Text);
                }
                else
                {
                    App.bKpdCheckOn = false;
                    kpdSPEvapCoolingTargetPressure.Text = Units.strFormat(sSPEvapCoolingTargetPressure, ADS.AIn006[3]);
                    App.bKpdCheckOn = true;
                }
            }
        }

        #endregion Keypad Entry

        #region Configuration

        private void LoadConfiguration_Auto()
        {
            labCO2.Content = strCO2;
            kpdCyclesToRun.Title = Units.KeyboardTitle(strCyclesToRun, ADS.iCYCLES_MIN, ADS.iCYCLEs_MAX, 0, 0);
            kpdSPAdsorptionTime.Title = Units.KeyboardTitle(strAdsorptionTime + strSetpoint, ADS.sSP_TIME_MIN, ADS.sSP_TIME_MAX, 0, 17);
            kpdSPAdsorptionCO2.Title = Units.KeyboardTitle(strAdsorptionCO2 + strSetpoint, ADS.sSP_CO2_MIN, ADS.sSP_CO2_MAX, 0, 4);
            kpdSPAdsorptionCO2Time.Title = Units.KeyboardTitle(strAdsorptionCO2Time + strSetpoint, ADS.sSP_TIME_MIN, ADS.sSP_TIME_MAX, 0, 17);
            kpdSPAdsorptionTemp.Title = Units.KeyboardTitle(strAdsorptionTemp + strSetpoint, ADS.sSP_TEMP_MIN, ADS.sSP_TEMP_MAX, 1, 1);
            kpdSPAdsorptionVFDOutput.Title = Units.KeyboardTitle(strAdsorptionVFDOutput + strSetpoint, 1, 100, 1, 6);
            kpdSPMinBypassSteamTemp.Title = Units.KeyboardTitle(strMinBypassSteamTemp + strSetpoint, ADS.sSP_TEMP_MIN, ADS.sSP_TEMP_MAX, 1, 1);
            kpdSPReactorLowValve.Title = Units.KeyboardTitle(strReactorValveLowPos + strSetpoint, 0, 100, 1, 6);
            kpdSPReactorHighValve.Title = Units.KeyboardTitle(strReactorValveHighPos + strSetpoint, 0, 100, 1, 6);
            kpdSPSteamValvePercentOpen.Title = Units.KeyboardTitle(strSteamValvePercentOpen + strSetpoint, 0, 100, 1, 6);
            kpdSPMaxAllowedSorbentTemp.Title = Units.KeyboardTitle(strMaxAllowedSorbentTemp + strSetpoint, ADS.sSP_TEMP_MIN, ADS.sSP_TEMP_MAX, 1, 1);
            kpdSPSteamPurgeTime.Title = Units.KeyboardTitle(strSteamPurgeTime + strSetpoint, ADS.sSP_TIME_MIN, ADS.sSP_TIME_MAX, 0, 17);
            kpdSPSteamPurgeCutoffTemp.Title = Units.KeyboardTitle(strSteamPurgeCutoffTemp + strSetpoint, ADS.sSP_TEMP_MIN, ADS.sSP_TEMP_MAX, 1, 1);
            kpdSPEvapCoolingCutoffTemp.Title = Units.KeyboardTitle(strEvapCoolingCutoffTemp + strSetpoint, ADS.sSP_TEMP_MIN, ADS.sSP_TEMP_MAX, 1, 1);
            kpdSPLeakCheckTime.Title = Units.KeyboardTitle(strLeakCheckTime + strSetpoint, ADS.sSP_TIME_MIN, ADS.sSP_TIME_MAX, 0, 20);
            LoadCartridgeCombobox();
            LoadSchemaCombobox();
            LoadAdsorptionSetpoints();
            LoadSetpoints();
            LoadAdsorptionKeypads();
            LoadSetpointKeypads();
        }

        private void LoadAdsorptionSetpoints()
        {
            sSPAdsorptionTime = ADS.sSPAdsorptionTime;
            sSPAdsorptionCO2 = ADS.sSPAdsorptionCO2;
            sSPAdsorptionCO2Time = ADS.sSPAdsorptionCO2Time;
        }

        private void LoadSetpoints()
        {
            sSPBoilerPressure = ADS.sSPBoilerPressure;
            sSPMinLRPCoolingLoopFlow = ADS.sSPMinLRPCoolingLoopFlow;
            sSPEvacuationTargetPressure = ADS.sSPEvacuationTargetPressure;
            sSPEvacuationTargetPressureSteam = ADS.sSPEvacuationTargetPressureSteam;                                        // 07/14/23 PS - New set point
            sSPMaxAllowedPressureLeakage = ADS.sSPMaxAllowedPressureLeakage;
            sSPReactorRepressurizationPressure = ADS.sSPReactorRepressurizationPressure;
            sSPAdsorptionTemp = ADS.sSPAdsorptionTemp;
            sSPAdsorptionVFDOutput = ADS.sSPAdsorptionVFDOutput;
            sSPMinBypassSteamFlow = ADS.sSPMinBypassSteamFlow;
            sSPMinBypassSteamTemp = ADS.sSPMinBypassSteamTemp;
            sSPReactorPressureLow = ADS.sSPReactorPressureLow;
            sSPReactorPressureHigh = ADS.sSPReactorPressureHigh;
            sSPReactorValveLowPos = ADS.sSPReactorValveLowPos;
            sSPReactorValveHighPos = ADS.sSPReactorValveHighPos;
            sSPSteamValvePercentOpen = ADS.sSPSteamValvePercentOpen;
            sSPMaxAllowedSorbentTemp = ADS.sSPMaxAllowedSorbentTemp;
            sSPSteamRepressurizationPressure = ADS.sSPSteamRepressurizationPressure;
            sSPSteamPurgeTime = ADS.sSPSteamPurgeTime;
            sSPSteamPurgeCutoffTemp = ADS.sSPSteamPurgeCutoffTemp;
            sSPEvapCoolingCutoffTemp = ADS.sSPEvapCoolingCutoffTemp;
            sSPEvapCoolingTargetPressure = ADS.sSPEvapCoolingTargetPressure;
            sSPLeakCheckTime = ADS.sSPLeakCheckTime;
        }

        private void LoadAdsorptionKeypads()
        {
            App.bKpdCheckOn = false;
            kpdSPAdsorptionTime.Text = sSPAdsorptionTime.ToString("F0");
            kpdSPAdsorptionCO2.Text = sSPAdsorptionCO2.ToString("F0");
            kpdSPAdsorptionCO2Time.Text = sSPAdsorptionCO2Time.ToString("F0");
            App.bKpdCheckOn = true;
        }

        private void LoadSetpointKeypads()
        {
            App.bKpdCheckOn = false;
            kpdSPBoilerPressure.Text = Units.strFormat(sSPBoilerPressure, ADS.AIn014[3]);
            kpdSPMinLRPCoolingLoopFlow.Text = Units.strFormat(sSPMinLRPCoolingLoopFlow, ADS.AIn022[3]);
            kpdSPEvacuationTargetPressure.Text = Units.strFormat(sSPEvacuationTargetPressure, ADS.AIn020[3]);
            kpdSPEvacuationTargetPressureSteam.Text = Units.strFormat(sSPEvacuationTargetPressureSteam, ADS.AIn020[3]);     // 07/14/23 PS - New set point
            kpdSPMaxAllowedPressureLeakage.Text = Units.strFormat(sSPMaxAllowedPressureLeakage, ADS.AIn006[3]);
            kpdSPReactorRepressurizationPressure.Text = Units.strFormat(sSPReactorRepressurizationPressure, ADS.AIn006[3]);
            kpdSPAdsorptionTemp.Text = sSPAdsorptionTemp.ToString("F1");
            kpdSPAdsorptionVFDOutput.Text = sSPAdsorptionVFDOutput.ToString("F1");
            kpdSPMinBypassSteamFlow.Text = Units.strFormat(sSPMinBypassSteamFlow, ADS.AIn015[3]);
            kpdSPMinBypassSteamTemp.Text = sSPMinBypassSteamTemp.ToString("F1");
            kpdSPReactorPressureLow.Text = Units.strFormat(sSPReactorPressureLow, ADS.AIn006[3]);
            kpdSPReactorPressureHigh.Text = Units.strFormat(sSPReactorPressureHigh, ADS.AIn006[3]);
            kpdSPReactorLowValve.Text = sSPReactorValveLowPos.ToString("F1");
            kpdSPReactorHighValve.Text = sSPReactorValveHighPos.ToString("F1");
            kpdSPSteamValvePercentOpen.Text = sSPSteamValvePercentOpen.ToString("F1");
            kpdSPMaxAllowedSorbentTemp.Text = sSPMaxAllowedSorbentTemp.ToString("F1");
            kpdSPSteamRepressurizationPressure.Text = Units.strFormat(sSPSteamRepressurizationPressure, ADS.AIn006[3]);
            kpdSPSteamPurgeTime.Text = sSPSteamPurgeTime.ToString("F1");
            kpdSPSteamPurgeCutoffTemp.Text = sSPSteamPurgeCutoffTemp.ToString("F1");
            kpdSPEvapCoolingCutoffTemp.Text = sSPEvapCoolingCutoffTemp.ToString("F1");
            kpdSPEvapCoolingTargetPressure.Text = Units.strFormat(sSPEvapCoolingTargetPressure, ADS.AIn006[3]);
            kpdSPLeakCheckTime.Text = sSPLeakCheckTime.ToString("F0");
            App.bKpdCheckOn = true;
        }

        public void LoadCartridgeCombobox()
        {
            cmbCartridges.ItemsSource = ADS._CartridgeList;
            cmbCartridges.SelectedIndex = ADS.iCartridgePointer;
        }

        public void LoadSchemaCombobox()
        {
            cmbSchemas.ItemsSource = ADS._SchemaList;
            cmbSchemas.SelectedIndex = ADS.iSchemaPointer;
        }

        #endregion Configuration
    }
}
