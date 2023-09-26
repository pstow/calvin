// Carbon Capture - Calvin
// Config.cs
// Rev 1.00 - September 20, 2023

using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Calvin.ConfigManager
{
    public class Config
    {
        public const string CP = "Configuration/Parameters/";
        public XmlDocument doc = new XmlDocument();

        public bool XConfig_Load()
        {
            bool bValid = true;
            string file = App.strUserFile;
            try                                                                                                     // Catch any errors               
            {
                doc.Load(file);                                                                                     // Load file
                LoadUserElements(doc);
                file = App.strPointFile;
                doc.Load(file);
                LoadPoints(doc);
                file = App.strCartridgeFile;
                doc.Load(file);
                LoadCartridges(doc);
                file = App.strSchemaFile;
                doc.Load(file);
                LoadSchemas(doc);
                doc = null;
                GC.Collect();
            }
            catch
            {
                App.WPFMessageBoxOK("Error Opening " + file, App.bgRed);
                bValid = false;
            }
            return bValid;
        }

        #region Cartridges

        private static void LoadCartridges(XmlDocument doc)                                                                 // 08/18/23 PS
        {
            XmlElement root = doc.DocumentElement;
            ADS.iCartridgeCount = (System.Text.RegularExpressions.Regex.Matches(root.InnerXml, "</").Count - 2) / 2;
            ADS.iTotalCycles = GetXMLValue(CP + "TOTAL_CYCLES", ADS.iTotalCycles, doc);
            for (int i = 0; i < ADS.iCartridgeCount; i++)
            {
                string count = i < 10 ? "00" + i.ToString() : "0" + i.ToString();
                ADS.strCartridgeName[i] = GetXMLValue(CP + "CARTRIDGE_NAME_" + count, ADS.strCartridgeName[i], doc);
                ADS.iCartridgeCycles[i] = GetXMLValue(CP + "CARTRIDGE_CYCLES_" + count, ADS.iCartridgeCycles[i], doc);
                ADS.iCartridgePointer = ADS.strCartridge == ADS.strCartridgeName[i] ? i : ADS.iCartridgePointer;
            }
        }

        public static void SaveCartridges()                                                                                 // 08/18/23 PS
        {
            try
            {
                XDocument xdoc = XDocument.Load(App.strCartridgeFile);
                SaveElement(xdoc, "TOTAL_CYCLES", ADS.iTotalCycles.ToString());
                for (int i = 0; i < ADS.iCartridgeCount; i++)
                {
                    string count = i < 10 ? "00" + i.ToString() : "0" + i.ToString();
                    SaveElement(xdoc, "CARTRIDGE_NAME_" + count, ADS.strCartridgeName[i]);
                    SaveElement(xdoc, "CARTRIDGE_CYCLES_" + count, ADS.iCartridgeCycles[i].ToString());
                }
                xdoc.Save(App.strCartridgeFile);
                xdoc = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                App.WPFMessageBoxOK(ex.ToString(), App.bgRed);
            }
        }

        #endregion Cartridges

        #region Schema

        private static void LoadSchemas(XmlDocument doc)                                                                 // 09/19/23 PS
        {
            XmlElement root = doc.DocumentElement;
            ADS.iSchemaCount = System.Text.RegularExpressions.Regex.Matches(root.InnerXml, "</").Count - 1;
            for (int i = 0; i < ADS.iSchemaCount; i++)
            {
                string count = i < 10 ? "00" + i.ToString() : "0" + i.ToString();
                ADS.strSchemaName[i] = GetXMLValue(CP + "SCHEMA_NAME_" + count, ADS.strSchemaName[i], doc);
                ADS.iSchemaPointer = ADS.strSchema == ADS.strSchemaName[i] ? i : ADS.iSchemaPointer;
            }
        }

        public static void SaveSchemas()                                                                                 // 09/19/23 PS
        {
            try
            {
                XDocument xdoc = XDocument.Load(App.strSchemaFile);
                for (int i = 0; i < ADS.iSchemaCount; i++)
                {
                    string count = i < 10 ? "00" + i.ToString() : "0" + i.ToString();
                    SaveElement(xdoc, "SCHEMA_NAME_" + count, ADS.strSchemaName[i]);
                }
                xdoc.Save(App.strSchemaFile);
                xdoc = null;
                GC.Collect();
            }
            catch (Exception ex)
            {
                App.WPFMessageBoxOK(ex.ToString(), App.bgRed);
            }
        }

        #endregion Schema

        #region User File

        private static void LoadUserElements(XmlDocument doc)
        {
            ADS.sPFactor = GetXMLValue(CP + "PID_Kp", ADS.sPFactor, doc);
            ADS.sIFactor = GetXMLValue(CP + "PID_Tn", ADS.sIFactor, doc);
            ADS.sIRange = GetXMLValue(CP + "PID_I_RANGE", ADS.sIRange, doc);
            ADS.strGraphPoint[0] = GetXMLValue(CP + "GRAPH_0", ADS.strGraphPoint[0], doc);
            ADS.iColors[0] = GetXMLValue(CP + "COLOR_0", ADS.iColors[0], doc);
            ADS.sGraphLow[0] = GetXMLValue(CP + "GRAPH_0_LOW", ADS.sGraphLow[0], doc);
            ADS.sGraphHigh[0] = GetXMLValue(CP + "GRAPH_0_HIGH", ADS.sGraphHigh[0], doc);
            ADS.strGraphPoint[1] = GetXMLValue(CP + "GRAPH_1", ADS.strGraphPoint[1], doc);
            ADS.iColors[1] = GetXMLValue(CP + "COLOR_1", ADS.iColors[1], doc);
            ADS.sGraphLow[1] = GetXMLValue(CP + "GRAPH_1_LOW", ADS.sGraphLow[1], doc);
            ADS.sGraphHigh[1] = GetXMLValue(CP + "GRAPH_1_HIGH", ADS.sGraphHigh[1], doc);
            ADS.strGraphPoint[2] = GetXMLValue(CP + "GRAPH_2", ADS.strGraphPoint[2], doc);
            ADS.iColors[2] = GetXMLValue(CP + "COLOR_2", ADS.iColors[2], doc);
            ADS.sGraphLow[2] = GetXMLValue(CP + "GRAPH_2_LOW", ADS.sGraphLow[2], doc);
            ADS.sGraphHigh[2] = GetXMLValue(CP + "GRAPH_2_HIGH", ADS.sGraphHigh[2], doc);
            ADS.strGraphPoint[3] = GetXMLValue(CP + "GRAPH_3", ADS.strGraphPoint[3], doc);
            ADS.iColors[3] = GetXMLValue(CP + "COLOR_3", ADS.iColors[3], doc);
            ADS.sGraphLow[3] = GetXMLValue(CP + "GRAPH_3_LOW", ADS.sGraphLow[3], doc);
            ADS.sGraphHigh[3] = GetXMLValue(CP + "GRAPH_3_HIGH", ADS.sGraphHigh[3], doc);
            ADS.strGraphPoint[4] = GetXMLValue(CP + "GRAPH_4", ADS.strGraphPoint[4], doc);
            ADS.iColors[4] = GetXMLValue(CP + "COLOR_4", ADS.iColors[4], doc);
            ADS.sGraphLow[4] = GetXMLValue(CP + "GRAPH_4_LOW", ADS.sGraphLow[4], doc);
            ADS.sGraphHigh[4] = GetXMLValue(CP + "GRAPH_4_HIGH", ADS.sGraphHigh[4], doc);
            ADS.strGraphPoint[5] = GetXMLValue(CP + "GRAPH_5", ADS.strGraphPoint[5], doc);
            ADS.iColors[5] = GetXMLValue(CP + "COLOR_5", ADS.iColors[5], doc);
            ADS.sGraphLow[5] = GetXMLValue(CP + "GRAPH_5_LOW", ADS.sGraphLow[5], doc);
            ADS.sGraphHigh[5] = GetXMLValue(CP + "GRAPH_5_HIGH", ADS.sGraphHigh[5], doc);
            ADS.sAOut001_SP = GetXMLValue(CP + "HT_1_SP", ADS.sAOut001_SP, doc);
            ADS.sAOut002_SP = GetXMLValue(CP + "HT_2_SP", ADS.sAOut002_SP, doc);
            ADS.sAOut003_SP = GetXMLValue(CP + "HT_3_SP", ADS.sAOut003_SP, doc);
            ADS.sAOut004_SP = GetXMLValue(CP + "HT_4_SP", ADS.sAOut004_SP, doc);
            ADS.sAOut005_SP = GetXMLValue(CP + "HT_5_SP", ADS.sAOut005_SP, doc);
            ADS.sAOut006_SP = GetXMLValue(CP + "HT_6_SP", ADS.sAOut006_SP, doc);
            ADS.sHT_PValue = GetXMLValue(CP + "HT_P_VALUE", ADS.sHT_PValue, doc);
            ADS.sHT_IValue = GetXMLValue(CP + "HT_I_VALUE", ADS.sHT_IValue, doc);
            ADS.sHT_IRange = GetXMLValue(CP + "HT_I_RANGE", ADS.sHT_IRange, doc);
            ADS.strCartridge = GetXMLValue(CP + "CARTRIDGE_NAME", ADS.strCartridge, doc);
            ADS.strSchema = GetXMLValue(CP + "SCHEMA_NAME", ADS.strSchema, doc);
            ADS.sSPAdsorptionTime = GetXMLValue(CP + "SP_ADSORPTION_TIME", ADS.sSPAdsorptionTime, doc);
            ADS.sSPAdsorptionCO2 = GetXMLValue(CP + "SP_ADSORPTION_CO2", ADS.sSPAdsorptionCO2, doc);
            ADS.sSPAdsorptionCO2Time = GetXMLValue(CP + "SP_ADSORPTION_CO2_TIME", ADS.sSPAdsorptionCO2Time, doc);
            ADS.sSPBoilerPressure = GetXMLValue(CP + "SP_BOILER_PRESSURE", ADS.sSPBoilerPressure, doc);
            ADS.sSPMinLRPCoolingLoopFlow = GetXMLValue(CP + "SP_MIN_LRP_COOLING_LOOP_FLOW", ADS.sSPMinLRPCoolingLoopFlow, doc);
            ADS.sSPEvacuationTargetPressure = GetXMLValue(CP + "SP_EVACUATION_TARGET_PRESSURE", ADS.sSPEvacuationTargetPressure, doc);
            ADS.sSPEvacuationTargetPressureSteam = GetXMLValue(CP + "SP_EVACUATION_TARGET_PRESSURE_STEAM", ADS.sSPEvacuationTargetPressureSteam, doc);  // 07/14/23 PS - New set point
            ADS.sSPMaxAllowedPressureLeakage = GetXMLValue(CP + "SP_MAX_ALLOWED_PRESSURE_LEAKAGE", ADS.sSPMaxAllowedPressureLeakage, doc);
            ADS.sSPReactorRepressurizationPressure = GetXMLValue(CP + "SP_REACTOR_REPRESSURIZATION_PRESSURE", ADS.sSPReactorRepressurizationPressure, doc);
            ADS.sSPAdsorptionTemp = GetXMLValue(CP + "SP_ADSORPTION_TEMP", ADS.sSPAdsorptionTemp, doc);
            ADS.sSPAdsorptionVFDOutput = GetXMLValue(CP + "SP_ADSORPTION_VFD_OUTPUT", ADS.sSPAdsorptionVFDOutput, doc);
            ADS.sSPMinBypassSteamFlow = GetXMLValue(CP + "SP_MIN_BYPASS_STEAM_FLOW", ADS.sSPMinBypassSteamFlow, doc);
            ADS.sSPMinBypassSteamTemp = GetXMLValue(CP + "SP_MIN_BYPASS_STEAM_TEMP", ADS.sSPMinBypassSteamTemp, doc);
            ADS.sSPReactorPressureLow = GetXMLValue(CP + "SP_REACTOR_PRESSURE_LOW", ADS.sSPReactorPressureLow, doc);
            ADS.sSPReactorPressureHigh = GetXMLValue(CP + "SP_REACTOR_PRESSURE_HIGH", ADS.sSPReactorPressureHigh, doc);
            ADS.sSPReactorValveLowPos = GetXMLValue(CP + "SP_REACTOR_VALVE_LOW_POS", ADS.sSPReactorValveLowPos, doc);
            ADS.sSPReactorValveHighPos = GetXMLValue(CP + "SP_REACTOR_VALVE_HIGH_POS", ADS.sSPReactorValveHighPos, doc);
            ADS.sSPSteamValvePercentOpen = GetXMLValue(CP + "SP_STEAM_VALVE_PERCENT_OPEN", ADS.sSPSteamValvePercentOpen, doc);
            ADS.sSPMaxAllowedSorbentTemp = GetXMLValue(CP + "SP_MAX_ALLOWED_SORBENT_TEMP", ADS.sSPMaxAllowedSorbentTemp, doc);
            ADS.sSPSteamRepressurizationPressure = GetXMLValue(CP + "SP_STEAM_REPRESSURIZATION_PRESSURE", ADS.sSPSteamRepressurizationPressure, doc);
            ADS.sSPSteamPurgeTime = GetXMLValue(CP + "SP_STEAM_PURGE_TIME", ADS.sSPSteamPurgeTime, doc);
            ADS.sSPSteamPurgeCutoffTemp = GetXMLValue(CP + "SP_STEAM_PURGE_CUTOFF_TEMP", ADS.sSPSteamPurgeCutoffTemp, doc);
            ADS.sSPEvapCoolingCutoffTemp = GetXMLValue(CP + "SP_EVAP_COOLING_CUTOFF_TEMP", ADS.sSPEvapCoolingCutoffTemp, doc);
            ADS.sSPEvapCoolingTargetPressure = GetXMLValue(CP + "SP_EVAP_COOLING_TARGET_PRESSURE", ADS.sSPEvapCoolingTargetPressure, doc);
            ADS.sSPLeakCheckTime = GetXMLValue(CP + "SP_LEAK_CHECK_TIME", ADS.sSPLeakCheckTime, doc);
            ADS.sSP_TEMP_MIN = GetXMLValue(CP + "SP_TEMP_MIN", ADS.sSP_TEMP_MIN, doc);
            ADS.sSP_TEMP_MAX = GetXMLValue(CP + "SP_TEMP_MAX", ADS.sSP_TEMP_MAX, doc);
            ADS.sSP_TIME_MIN = GetXMLValue(CP + "SP_TIME_MIN", ADS.sSP_TIME_MIN, doc);
            ADS.sSP_TIME_MAX = GetXMLValue(CP + "SP_TIME_MAX", ADS.sSP_TIME_MAX, doc);
            ADS.sSP_CO2_MIN = GetXMLValue(CP + "SP_CO2_MIN", ADS.sSP_CO2_MIN, doc);
            ADS.sSP_CO2_MAX = GetXMLValue(CP + "SP_CO2_MAX", ADS.sSP_CO2_MAX, doc);
            ADS.sBOILER_PRESSURE_MIN = GetXMLValue(CP + "BOILER_PRESSURE_MIN", ADS.sBOILER_PRESSURE_MIN, doc);
            ADS.sBOILER_PRESSURE_MAX = GetXMLValue(CP + "BOILER_PRESSURE_MAX", ADS.sBOILER_PRESSURE_MAX, doc);
            ADS.iCYCLES_MIN = GetXMLValue(CP + "CYCLES_MIN", ADS.iCYCLES_MIN, doc);
            ADS.iCYCLEs_MAX = GetXMLValue(CP + "CYCLES_MAX", ADS.iCYCLEs_MAX, doc);
            ADS.strLocation = GetXMLValue(CP + "LOCATION", ADS.strLocation, doc);
            ADS.strEmail[0] = GetXMLValue(CP + "EMAIL1", ADS.strEmail[0], doc);
            ADS.strEmail[1] = GetXMLValue(CP + "EMAIL2", ADS.strEmail[1], doc);
            ADS.strEmail[2] = GetXMLValue(CP + "EMAIL3", ADS.strEmail[2], doc);
            ADS.strEmail[3] = GetXMLValue(CP + "EMAIL4", ADS.strEmail[3], doc);
            ADS.strEmail[4] = GetXMLValue(CP + "EMAIL5", ADS.strEmail[4], doc);
            ADS.strData = GetXMLValue(CP + "DATA", ADS.strData, doc);
            ADS.strPassword = DecryptString(ADS.strData);
        }

        public static void SaveUserFile()
        {
            try
            {
                XDocument xdoc = XDocument.Load(App.strUserFile);
                SaveElement(xdoc, "PID_Kp", ADS.sPFactor.ToString());
                SaveElement(xdoc, "PID_Tn", ADS.sIFactor.ToString());
                SaveElement(xdoc, "PID_I_RANGE", ADS.sIRange.ToString());
                SaveElement(xdoc, "GRAPH_0", ADS.strGraphPoint[0]);
                SaveElement(xdoc, "COLOR_0", ADS.iColors[0].ToString());
                SaveElement(xdoc, "GRAPH_0_LOW", ADS.sGraphLow[0].ToString());
                SaveElement(xdoc, "GRAPH_0_HIGH", ADS.sGraphHigh[0].ToString());
                SaveElement(xdoc, "GRAPH_1", ADS.strGraphPoint[1]);
                SaveElement(xdoc, "COLOR_1", ADS.iColors[1].ToString());
                SaveElement(xdoc, "GRAPH_1_LOW", ADS.sGraphLow[1].ToString());
                SaveElement(xdoc, "GRAPH_1_HIGH", ADS.sGraphHigh[1].ToString());
                SaveElement(xdoc, "GRAPH_2", ADS.strGraphPoint[2]);
                SaveElement(xdoc, "COLOR_2", ADS.iColors[2].ToString());
                SaveElement(xdoc, "GRAPH_2_LOW", ADS.sGraphLow[2].ToString());
                SaveElement(xdoc, "GRAPH_2_HIGH", ADS.sGraphHigh[2].ToString());
                SaveElement(xdoc, "GRAPH_3", ADS.strGraphPoint[3]);
                SaveElement(xdoc, "COLOR_3", ADS.iColors[3].ToString());
                SaveElement(xdoc, "GRAPH_3_LOW", ADS.sGraphLow[3].ToString());
                SaveElement(xdoc, "GRAPH_3_HIGH", ADS.sGraphHigh[3].ToString());
                SaveElement(xdoc, "GRAPH_4", ADS.strGraphPoint[4]);
                SaveElement(xdoc, "COLOR_4", ADS.iColors[4].ToString());
                SaveElement(xdoc, "GRAPH_4_LOW", ADS.sGraphLow[4].ToString());
                SaveElement(xdoc, "GRAPH_4_HIGH", ADS.sGraphHigh[4].ToString());
                SaveElement(xdoc, "GRAPH_5", ADS.strGraphPoint[5]);
                SaveElement(xdoc, "COLOR_5", ADS.iColors[5].ToString());
                SaveElement(xdoc, "GRAPH_5_LOW", ADS.sGraphLow[5].ToString());
                SaveElement(xdoc, "GRAPH_5_HIGH", ADS.sGraphHigh[5].ToString());
                SaveElement(xdoc, "HT_1_SP", ADS.sAOut001_SP.ToString());
                SaveElement(xdoc, "HT_2_SP", ADS.sAOut002_SP.ToString());
                SaveElement(xdoc, "HT_3_SP", ADS.sAOut003_SP.ToString());
                SaveElement(xdoc, "HT_4_SP", ADS.sAOut004_SP.ToString());
                SaveElement(xdoc, "HT_5_SP", ADS.sAOut005_SP.ToString());
                SaveElement(xdoc, "HT_6_SP", ADS.sAOut006_SP.ToString());
                SaveElement(xdoc, "HT_P_VALUE", ADS.sHT_PValue.ToString());
                SaveElement(xdoc, "HT_I_VALUE", ADS.sHT_IValue.ToString());
                SaveElement(xdoc, "HT_I_RANGE", ADS.sHT_IRange.ToString());
                SaveElement(xdoc, "CARTRIDGE_NAME", ADS.strCartridge);
                SaveElement(xdoc, "SCHEMA_NAME", ADS.strSchema);
                SaveElement(xdoc, "SP_ADSORPTION_TIME", ADS.sSPAdsorptionTime.ToString());
                SaveElement(xdoc, "SP_ADSORPTION_CO2", ADS.sSPAdsorptionCO2.ToString());
                SaveElement(xdoc, "SP_ADSORPTION_CO2_TIME", ADS.sSPAdsorptionCO2Time.ToString());
                SaveElement(xdoc, "SP_BOILER_PRESSURE", ADS.sSPBoilerPressure.ToString());
                SaveElement(xdoc, "SP_MIN_LRP_COOLING_LOOP_FLOW", ADS.sSPMinLRPCoolingLoopFlow.ToString());
                SaveElement(xdoc, "SP_EVACUATION_TARGET_PRESSURE", ADS.sSPEvacuationTargetPressure.ToString());
                SaveElement(xdoc, "SP_EVACUATION_TARGET_PRESSURE_STEAM", ADS.sSPEvacuationTargetPressureSteam.ToString());  // 07/14/23 PS - New set point
                SaveElement(xdoc, "SP_MAX_ALLOWED_PRESSURE_LEAKAGE", ADS.sSPMaxAllowedPressureLeakage.ToString());
                SaveElement(xdoc, "SP_REACTOR_REPRESSURIZATION_PRESSURE", ADS.sSPReactorRepressurizationPressure.ToString());
                SaveElement(xdoc, "SP_ADSORPTION_TEMP", ADS.sSPAdsorptionTemp.ToString());
                SaveElement(xdoc, "SP_ADSORPTION_VFD_OUTPUT", ADS.sSPAdsorptionVFDOutput.ToString());
                SaveElement(xdoc, "SP_MIN_BYPASS_STEAM_FLOW", ADS.sSPMinBypassSteamFlow.ToString());
                SaveElement(xdoc, "SP_MIN_BYPASS_STEAM_TEMP", ADS.sSPMinBypassSteamTemp.ToString());
                SaveElement(xdoc, "SP_REACTOR_PRESSURE_LOW", ADS.sSPReactorPressureLow.ToString());
                SaveElement(xdoc, "SP_REACTOR_PRESSURE_HIGH", ADS.sSPReactorPressureHigh.ToString());
                SaveElement(xdoc, "SP_REACTOR_VALVE_LOW_POS", ADS.sSPReactorValveLowPos.ToString());
                SaveElement(xdoc, "SP_REACTOR_VALVE_HIGH_POS", ADS.sSPReactorValveHighPos.ToString());
                SaveElement(xdoc, "SP_STEAM_VALVE_PERCENT_OPEN", ADS.sSPSteamValvePercentOpen.ToString());
                SaveElement(xdoc, "SP_MAX_ALLOWED_SORBENT_TEMP", ADS.sSPMaxAllowedSorbentTemp.ToString());
                SaveElement(xdoc, "SP_STEAM_REPRESSURIZATION_PRESSURE", ADS.sSPSteamRepressurizationPressure.ToString());
                SaveElement(xdoc, "SP_STEAM_PURGE_TIME", ADS.sSPSteamPurgeTime.ToString());
                SaveElement(xdoc, "SP_STEAM_PURGE_CUTOFF_TEMP", ADS.sSPSteamPurgeCutoffTemp.ToString());
                SaveElement(xdoc, "SP_EVAP_COOLING_CUTOFF_TEMP", ADS.sSPEvapCoolingCutoffTemp.ToString());
                SaveElement(xdoc, "SP_EVAP_COOLING_TARGET_PRESSURE", ADS.sSPEvapCoolingTargetPressure.ToString());
                SaveElement(xdoc, "SP_LEAK_CHECK_TIME", ADS.sSPLeakCheckTime.ToString());
                SaveElement(xdoc, "EMAIL1", ADS.strEmail[0]);
                SaveElement(xdoc, "EMAIL2", ADS.strEmail[1]);
                SaveElement(xdoc, "EMAIL3", ADS.strEmail[2]);
                SaveElement(xdoc, "EMAIL4", ADS.strEmail[3]);
                SaveElement(xdoc, "EMAIL5", ADS.strEmail[4]);
                ADS.strData = EncryptString(ADS.strPassword);
                SaveElement(xdoc, "DATA", ADS.strData);
                xdoc.Save(App.strUserFile);
                xdoc = null;
                GC.Collect();
            }
            catch (Exception ex)
            { App.WPFMessageBoxOK(ex.ToString(), App.bgRed); }
        }

        public static string EncryptString(string str)
        {
            string s = "";
            int j;
            char[] a = str.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                j = 158 - a[i] + i;
                string c = j.ToString("X");
                s += c;
            }
            return s;
        }

        public static string DecryptString(string str)
        {
            string s = "";
            int j;
            char p;
            for (int i = 0; i < str.Length; i += 2)
            {
                string c = str.Substring(i, 2);
                j = int.Parse(c, System.Globalization.NumberStyles.HexNumber);
                j = 158 - j + (i / 2);
                p = (char)j;
                s += p;
            }
            return s;
        }

        #endregion User File

        #region Points

        private static void LoadPoints(XmlDocument doc)
        {
            Units.strUnits[0] = " ";
            Units.strUnits[1] = GetXMLValue(CP + "Unit01", Units.strUnits[1], doc);
            Units.strUnits[2] = GetXMLValue(CP + "Unit02", Units.strUnits[2], doc);
            Units.strUnits[3] = GetXMLValue(CP + "Unit03", Units.strUnits[3], doc);
            Units.strUnits[4] = GetXMLValue(CP + "Unit04", Units.strUnits[4], doc);
            Units.strUnits[5] = GetXMLValue(CP + "Unit05", Units.strUnits[5], doc);
            Units.strUnits[6] = GetXMLValue(CP + "Unit06", Units.strUnits[6], doc);
            Units.strUnits[7] = GetXMLValue(CP + "Unit07", Units.strUnits[7], doc);
            Units.strUnits[8] = GetXMLValue(CP + "Unit08", Units.strUnits[8], doc);
            Units.strUnits[9] = GetXMLValue(CP + "Unit09", Units.strUnits[9], doc);
            Units.strUnits[10] = GetXMLValue(CP + "Unit10", Units.strUnits[10], doc);
            Units.strUnits[11] = GetXMLValue(CP + "Unit11", Units.strUnits[11], doc);
            Units.strUnits[12] = GetXMLValue(CP + "Unit12", Units.strUnits[12], doc);
            Units.strUnits[13] = GetXMLValue(CP + "Unit13", Units.strUnits[13], doc);
            Units.strUnits[14] = GetXMLValue(CP + "Unit14", Units.strUnits[14], doc);
            Units.strUnits[15] = GetXMLValue(CP + "Unit15", Units.strUnits[15], doc);
            Units.strUnits[16] = GetXMLValue(CP + "Unit16", Units.strUnits[16], doc);
            Units.strUnits[17] = GetXMLValue(CP + "Unit17", Units.strUnits[17], doc);
            Units.strUnits[18] = GetXMLValue(CP + "Unit18", Units.strUnits[18], doc);
            Units.strUnits[19] = GetXMLValue(CP + "Unit19", Units.strUnits[19], doc);
            Units.strUnits[20] = GetXMLValue(CP + "Unit20", Units.strUnits[20], doc);
            Units.strUnits[21] = GetXMLValue(CP + "Unit21", Units.strUnits[21], doc);
            Units.strUnits[22] = GetXMLValue(CP + "Unit22", Units.strUnits[22], doc);
            Units.strUnits[23] = GetXMLValue(CP + "Unit23", Units.strUnits[23], doc);
            Units.strUnits[24] = GetXMLValue(CP + "Unit24", Units.strUnits[24], doc);
            Units.strUnits[25] = GetXMLValue(CP + "Unit25", Units.strUnits[25], doc);
            Units.strUnits[26] = GetXMLValue(CP + "Unit26", Units.strUnits[26], doc);
            Units.strUnits[27] = GetXMLValue(CP + "Unit27", Units.strUnits[27], doc);
            Units.strUnits[28] = GetXMLValue(CP + "Unit28", Units.strUnits[28], doc);
            Units.strUnits[29] = GetXMLValue(CP + "Unit29", Units.strUnits[29], doc);

            GetAndParsePoint(CP + "AIn000", ADS.AIn000, doc, true);
            GetAndParsePoint(CP + "AIn001", ADS.AIn001, doc, true);
            GetAndParsePoint(CP + "AIn002", ADS.AIn002, doc, true);
            GetAndParsePoint(CP + "AIn003", ADS.AIn003, doc, true);
            GetAndParsePoint(CP + "AIn004", ADS.AIn004, doc, true);
            GetAndParsePoint(CP + "AIn005", ADS.AIn005, doc, true);
            GetAndParsePoint(CP + "AIn006", ADS.AIn006, doc, true);
            GetAndParsePoint(CP + "AIn007", ADS.AIn007, doc, true);
            GetAndParsePoint(CP + "AIn008", ADS.AIn008, doc, true);
            GetAndParsePoint(CP + "AIn009", ADS.AIn009, doc, true);
            GetAndParsePoint(CP + "AIn010", ADS.AIn010, doc, true);
            GetAndParsePoint(CP + "AIn011", ADS.AIn011, doc, true);
            GetAndParsePoint(CP + "AIn012", ADS.AIn012, doc, true);
            GetAndParsePoint(CP + "AIn013", ADS.AIn013, doc, true);
            GetAndParsePoint(CP + "AIn014", ADS.AIn014, doc, true);
            GetAndParsePoint(CP + "AIn015", ADS.AIn015, doc, true);
            GetAndParsePoint(CP + "AIn016", ADS.AIn016, doc, true);
            GetAndParsePoint(CP + "AIn017", ADS.AIn017, doc, true);
            GetAndParsePoint(CP + "AIn018", ADS.AIn018, doc, true);
            GetAndParsePoint(CP + "AIn019", ADS.AIn019, doc, true);
            GetAndParsePoint(CP + "AIn020", ADS.AIn020, doc, true);
            GetAndParsePoint(CP + "AIn021", ADS.AIn021, doc, true);
            GetAndParsePoint(CP + "AIn022", ADS.AIn022, doc, true);
            GetAndParsePoint(CP + "AIn023", ADS.AIn023, doc, true);
            GetAndParsePoint(CP + "AIn024", ADS.AIn024, doc, true);
            GetAndParsePoint(CP + "AIn025", ADS.AIn025, doc, true);
            GetAndParsePoint(CP + "AIn026", ADS.AIn026, doc, true);
            GetAndParsePoint(CP + "AIn027", ADS.AIn027, doc, true);
            GetAndParsePoint(CP + "AIn028", ADS.AIn028, doc, true);
            GetAndParsePoint(CP + "AIn040", ADS.AIn040, doc, true);
            GetAndParsePoint(CP + "AIn041", ADS.AIn041, doc, true);
            GetAndParsePoint(CP + "AIn042", ADS.AIn042, doc, true);
            GetAndParsePoint(CP + "AIn043", ADS.AIn043, doc, true);
            GetAndParsePoint(CP + "AIn044", ADS.AIn044, doc, true);
            GetAndParsePoint(CP + "AIn045", ADS.AIn045, doc, true);
            GetAndParsePoint(CP + "AIn046", ADS.AIn046, doc, true);
            GetAndParsePoint(CP + "AIn050", ADS.AIn050, doc, true);
            GetAndParsePoint(CP + "AIn051", ADS.AIn051, doc, true);
            GetAndParsePoint(CP + "AIn052", ADS.AIn052, doc, true);
            GetAndParsePoint(CP + "AIn053", ADS.AIn053, doc, true);
            GetAndParsePoint(CP + "AIn054", ADS.AIn054, doc, true);
            GetAndParsePoint(CP + "AIn055", ADS.AIn055, doc, true);

            GetAndParsePoint(CP + "Temp000", ADS.Temp000, doc, false);
            GetAndParsePoint(CP + "Temp001", ADS.Temp001, doc, false);
            GetAndParsePoint(CP + "Temp002", ADS.Temp002, doc, false);
            GetAndParsePoint(CP + "Temp003", ADS.Temp003, doc, false);
            GetAndParsePoint(CP + "Temp004", ADS.Temp004, doc, false);
            GetAndParsePoint(CP + "Temp005", ADS.Temp005, doc, false);
            GetAndParsePoint(CP + "Temp006", ADS.Temp006, doc, false);
            GetAndParsePoint(CP + "Temp007", ADS.Temp007, doc, false);
            GetAndParsePoint(CP + "Temp008", ADS.Temp008, doc, false);
            GetAndParsePoint(CP + "Temp009", ADS.Temp009, doc, false);
            GetAndParsePoint(CP + "Temp010", ADS.Temp010, doc, false);
            GetAndParsePoint(CP + "Temp011", ADS.Temp011, doc, false);
            GetAndParsePoint(CP + "Temp012", ADS.Temp012, doc, false);
            GetAndParsePoint(CP + "Temp013", ADS.Temp013, doc, false);
            GetAndParsePoint(CP + "Temp014", ADS.Temp014, doc, false);
            GetAndParsePoint(CP + "Temp015", ADS.Temp015, doc, false);
            GetAndParsePoint(CP + "Temp016", ADS.Temp016, doc, false);
            GetAndParsePoint(CP + "Temp017", ADS.Temp017, doc, false);
            GetAndParsePoint(CP + "Temp018", ADS.Temp018, doc, false);
            GetAndParsePoint(CP + "Temp019", ADS.Temp019, doc, false);
            GetAndParsePoint(CP + "Temp020", ADS.Temp020, doc, false);
            GetAndParsePoint(CP + "Temp021", ADS.Temp021, doc, false);
            GetAndParsePoint(CP + "Temp022", ADS.Temp022, doc, false);
            GetAndParsePoint(CP + "Temp023", ADS.Temp023, doc, false);
            GetAndParsePoint(CP + "Temp024", ADS.Temp024, doc, false);
            GetAndParsePoint(CP + "Temp025", ADS.Temp025, doc, false);
            GetAndParsePoint(CP + "Temp026", ADS.Temp026, doc, false);

            GetAndParsePoint(CP + "AOut000", ADS.AOut000, doc, false);
            GetAndParsePoint(CP + "AOut000RPM", ADS.AOut000RPM, doc, false);
            GetAndParsePoint(CP + "AOut001", ADS.AOut001, doc, false);
            GetAndParsePoint(CP + "AOut002", ADS.AOut002, doc, false);
            GetAndParsePoint(CP + "AOut003", ADS.AOut003, doc, false);
            GetAndParsePoint(CP + "AOut004", ADS.AOut004, doc, false);
            GetAndParsePoint(CP + "AOut005", ADS.AOut005, doc, false);
            GetAndParsePoint(CP + "AOut006", ADS.AOut006, doc, false);
            GetAndParsePoint(CP + "AOut007", ADS.AOut007, doc, false);
            GetAndParsePoint(CP + "AOut008", ADS.AOut008, doc, false);
            GetAndParsePoint(CP + "AOut009", ADS.AOut009, doc, false);
            GetAndParsePoint(CP + "AOut010", ADS.AOut010, doc, false);

            GetAndParseDO(CP + "DO000", ADS.DO000, doc);
            GetAndParseDO(CP + "DO001", ADS.DO001, doc);
            GetAndParseDO(CP + "DO002", ADS.DO002, doc);
            GetAndParseDO(CP + "DO003", ADS.DO003, doc);
            GetAndParseDO(CP + "DO004", ADS.DO004, doc);
            GetAndParseDO(CP + "DO005", ADS.DO005, doc);
            GetAndParseDO(CP + "DO006", ADS.DO006, doc);
            GetAndParseDO(CP + "DO007", ADS.DO007, doc);
            GetAndParseDO(CP + "DO008", ADS.DO008, doc);
            GetAndParseDO(CP + "DO009", ADS.DO009, doc);
            GetAndParseDO(CP + "DO010", ADS.DO010, doc);
            GetAndParseDO(CP + "DO011", ADS.DO011, doc);
            GetAndParseDO(CP + "DO012", ADS.DO012, doc);
            GetAndParseDO(CP + "DO013", ADS.DO013, doc);
            GetAndParseDO(CP + "DO014", ADS.DO014, doc);
            GetAndParseDO(CP + "DO015", ADS.DO015, doc);
            GetAndParseDO(CP + "DO016", ADS.DO016, doc);
            GetAndParseDO(CP + "DO017", ADS.DO017, doc);
            GetAndParseDO(CP + "DO018", ADS.DO018, doc);
            GetAndParseDO(CP + "DO019", ADS.DO019, doc);
            GetAndParseDO(CP + "DO020", ADS.DO020, doc);
            GetAndParseDO(CP + "DO021", ADS.DO021, doc);
            GetAndParseDO(CP + "DO022", ADS.DO022, doc);                                            // 09/20/23 PS
        }

        public static void SavePointFile()
        {
            try
            {
                XDocument xdoc = XDocument.Load(App.strPointFile);

                GetAndSavePoint("AIn000", ADS.AIn000, xdoc);
                GetAndSavePoint("AIn001", ADS.AIn001, xdoc);
                GetAndSavePoint("AIn002", ADS.AIn002, xdoc);
                GetAndSavePoint("AIn003", ADS.AIn003, xdoc);
                GetAndSavePoint("AIn004", ADS.AIn004, xdoc);
                GetAndSavePoint("AIn005", ADS.AIn005, xdoc);
                GetAndSavePoint("AIn006", ADS.AIn006, xdoc);
                GetAndSavePoint("AIn007", ADS.AIn007, xdoc);
                GetAndSavePoint("AIn008", ADS.AIn008, xdoc);
                GetAndSavePoint("AIn009", ADS.AIn009, xdoc);
                GetAndSavePoint("AIn010", ADS.AIn010, xdoc);
                GetAndSavePoint("AIn011", ADS.AIn011, xdoc);
                GetAndSavePoint("AIn012", ADS.AIn012, xdoc);
                GetAndSavePoint("AIn013", ADS.AIn013, xdoc);
                GetAndSavePoint("AIn014", ADS.AIn014, xdoc);
                GetAndSavePoint("AIn015", ADS.AIn015, xdoc);
                GetAndSavePoint("AIn016", ADS.AIn016, xdoc);
                GetAndSavePoint("AIn017", ADS.AIn017, xdoc);
                GetAndSavePoint("AIn018", ADS.AIn018, xdoc);
                GetAndSavePoint("AIn019", ADS.AIn019, xdoc);
                GetAndSavePoint("AIn020", ADS.AIn020, xdoc);
                GetAndSavePoint("AIn021", ADS.AIn021, xdoc);
                GetAndSavePoint("AIn022", ADS.AIn022, xdoc);
                GetAndSavePoint("AIn023", ADS.AIn023, xdoc);
                GetAndSavePoint("AIn024", ADS.AIn024, xdoc);
                GetAndSavePoint("AIn025", ADS.AIn025, xdoc);
                GetAndSavePoint("AIn026", ADS.AIn026, xdoc);
                GetAndSavePoint("AIn027", ADS.AIn027, xdoc);
                GetAndSavePoint("AIn028", ADS.AIn028, xdoc);
                GetAndSavePoint("AIn040", ADS.AIn040, xdoc);
                GetAndSavePoint("AIn041", ADS.AIn041, xdoc);
                GetAndSavePoint("AIn042", ADS.AIn042, xdoc);
                GetAndSavePoint("AIn043", ADS.AIn043, xdoc);
                GetAndSavePoint("AIn044", ADS.AIn044, xdoc);
                GetAndSavePoint("AIn045", ADS.AIn045, xdoc);
                GetAndSavePoint("AIn046", ADS.AIn046, xdoc);
                GetAndSavePoint("AIn050", ADS.AIn050, xdoc);
                GetAndSavePoint("AIn051", ADS.AIn051, xdoc);
                GetAndSavePoint("AIn052", ADS.AIn052, xdoc);
                GetAndSavePoint("AIn053", ADS.AIn053, xdoc);
                GetAndSavePoint("AIn054", ADS.AIn054, xdoc);
                GetAndSavePoint("AIn055", ADS.AIn055, xdoc);

                GetAndSavePoint("Temp000", ADS.Temp000, xdoc);
                GetAndSavePoint("Temp001", ADS.Temp001, xdoc);
                GetAndSavePoint("Temp002", ADS.Temp002, xdoc);
                GetAndSavePoint("Temp003", ADS.Temp003, xdoc);
                GetAndSavePoint("Temp004", ADS.Temp004, xdoc);
                GetAndSavePoint("Temp005", ADS.Temp005, xdoc);
                GetAndSavePoint("Temp006", ADS.Temp006, xdoc);
                GetAndSavePoint("Temp007", ADS.Temp007, xdoc);
                GetAndSavePoint("Temp008", ADS.Temp008, xdoc);
                GetAndSavePoint("Temp009", ADS.Temp009, xdoc);
                GetAndSavePoint("Temp010", ADS.Temp010, xdoc);
                GetAndSavePoint("Temp011", ADS.Temp011, xdoc);
                GetAndSavePoint("Temp012", ADS.Temp012, xdoc);
                GetAndSavePoint("Temp013", ADS.Temp013, xdoc);
                GetAndSavePoint("Temp014", ADS.Temp014, xdoc);
                GetAndSavePoint("Temp015", ADS.Temp015, xdoc);
                GetAndSavePoint("Temp016", ADS.Temp016, xdoc);
                GetAndSavePoint("Temp017", ADS.Temp017, xdoc);
                GetAndSavePoint("Temp018", ADS.Temp018, xdoc);
                GetAndSavePoint("Temp019", ADS.Temp019, xdoc);
                GetAndSavePoint("Temp020", ADS.Temp020, xdoc);
                GetAndSavePoint("Temp021", ADS.Temp021, xdoc);
                GetAndSavePoint("Temp022", ADS.Temp022, xdoc);
                GetAndSavePoint("Temp023", ADS.Temp023, xdoc);
                GetAndSavePoint("Temp024", ADS.Temp024, xdoc);
                GetAndSavePoint("Temp025", ADS.Temp025, xdoc);
                GetAndSavePoint("Temp026", ADS.Temp026, xdoc);

                GetAndSavePoint("AOut000", ADS.AOut000, xdoc);
                GetAndSavePoint("AOut000RPM", ADS.AOut000RPM, xdoc);
                GetAndSavePoint("AOut001", ADS.AOut001, xdoc);
                GetAndSavePoint("AOut002", ADS.AOut002, xdoc);
                GetAndSavePoint("AOut003", ADS.AOut003, xdoc);
                GetAndSavePoint("AOut004", ADS.AOut004, xdoc);
                GetAndSavePoint("AOut005", ADS.AOut005, xdoc);
                GetAndSavePoint("AOut006", ADS.AOut006, xdoc);
                GetAndSavePoint("AOut007", ADS.AOut007, xdoc);
                GetAndSavePoint("AOut008", ADS.AOut008, xdoc);
                GetAndSavePoint("AOut009", ADS.AOut009, xdoc);
                GetAndSavePoint("AOut010", ADS.AOut010, xdoc);

                GetAndSaveDO("DO000", ADS.DO000, xdoc);
                GetAndSaveDO("DO001", ADS.DO001, xdoc);
                GetAndSaveDO("DO002", ADS.DO002, xdoc);
                GetAndSaveDO("DO003", ADS.DO003, xdoc);
                GetAndSaveDO("DO004", ADS.DO004, xdoc);
                GetAndSaveDO("DO005", ADS.DO005, xdoc);
                GetAndSaveDO("DO006", ADS.DO006, xdoc);
                GetAndSaveDO("DO007", ADS.DO007, xdoc);
                GetAndSaveDO("DO008", ADS.DO008, xdoc);
                GetAndSaveDO("DO009", ADS.DO009, xdoc);
                GetAndSaveDO("DO010", ADS.DO010, xdoc);
                GetAndSaveDO("DO011", ADS.DO011, xdoc);
                GetAndSaveDO("DO012", ADS.DO012, xdoc);
                GetAndSaveDO("DO013", ADS.DO013, xdoc);
                GetAndSaveDO("DO014", ADS.DO014, xdoc);
                GetAndSaveDO("DO015", ADS.DO015, xdoc);
                GetAndSaveDO("DO016", ADS.DO016, xdoc);
                GetAndSaveDO("DO017", ADS.DO017, xdoc);
                GetAndSaveDO("DO018", ADS.DO018, xdoc);
                GetAndSaveDO("DO019", ADS.DO019, xdoc);
                GetAndSaveDO("DO020", ADS.DO020, xdoc);
                GetAndSaveDO("DO021", ADS.DO021, xdoc);
                GetAndSaveDO("DO022", ADS.DO022, xdoc);                                            // 09/20/23 PS

                xdoc.Save(App.strPointFile);
                xdoc = null;
                GC.Collect();
            }
            catch (Exception ex)
            { App.WPFMessageBoxOK(ex.ToString(), App.bgRed); }
        }

        private static void GetAndParsePoint(string point, string[] item, XmlDocument doc, bool save)
        {
            string pt = "";
            pt = GetXMLValue(point, pt, doc);
            string[] parsed = pt.Split(',');
            for (int i = 0; i < 9; i++)
            {
                item[i] = parsed[i];
            }
            if (save) { item[9] = "x"; }
        }

        private static void GetAndSavePoint(string point, string[] item, XDocument xdoc)
        {
            string c = ",";
            string pt = item[0] + c + item[1] + c + item[2] + c + item[3] + c + item[4] + c + item[5] + c + item[6] + c + item[7] + c + item[8];
            SaveElement(xdoc, point, pt);
        }

        public static void GetAndParseDO(string point, string[] item, XmlDocument doc)
        {
            string pt = "";
            pt = GetXMLValue(point, pt, doc);
            string[] parsed = pt.Split(',');
            for (int i = 0; i < 3; i++)
            {
                item[i] = parsed[i];
            }
        }

        private static void GetAndSaveDO(string point, string[] item, XDocument xdoc)
        {
            string c = ",";
            string pt = item[0] + c + item[1] + c + item[2];
            SaveElement(xdoc, point, pt);
        }

        #endregion Points

        #region File Functions

        private static void SaveElement(XDocument x, string name, string value)
        {
            try
            { x.XPathSelectElement("//" + name).Value = value; }
            catch
            {
                XElement config = x.Root;
                config.Element("Parameters").Add(new XElement(name, value));
            }
        }


        private static string GetXMLValue(string item, string value, XmlDocument data)
        {
            string temp = data.SelectSingleNode(item).InnerText;
            return temp;
        }

        private static short GetXMLValue(string item, short value, XmlDocument data)
        {
            return Convert.ToInt16(data.SelectSingleNode(item).InnerText);
        }

        private static int GetXMLValue(string item, int value, XmlDocument data)
        {
            int temp;
            try
            {
                string strTemp = data.SelectSingleNode(item).InnerText;
                temp = Convert.ToInt32(strTemp);
            }
            catch
            { temp = value; }
            return temp;
        }

        private static float GetXMLValue(string item, float value, XmlDocument data)
        {
            float temp;
            try
            {
                string strTemp = data.SelectSingleNode(item).InnerText;
                temp = Convert.ToSingle(strTemp);
            }
            catch
            { temp = value; }
            return temp;
        }

        #endregion File Functions

    }
}



