// Carbon Capture - Calvin
// ADS.cs
// Rev 1.00 - September 20, 2023

using System;
using System.Collections.ObjectModel;
using System.IO;
using TwinCAT.Ads;
using Calvin.ConfigManager;

namespace Calvin
{
    public class ADS
    {
        #region Variables

        public static ObservableCollection<PointInfo> _PointCollection = new ObservableCollection<PointInfo>();
        public ObservableCollection<PointInfo> PointCollection => _PointCollection;

        public static ObservableCollection<string> _cmbPointList = new ObservableCollection<string>();
        public ObservableCollection<string> cmbPointList => _cmbPointList;

        public static ObservableCollection<string> _cmbUnitList = new ObservableCollection<string>();
        public ObservableCollection<string> UnitCollection => _cmbUnitList;

        public static ObservableCollection<string> _cmbDecimalList = new ObservableCollection<string>();
        public ObservableCollection<string> DecimalCollection => _cmbDecimalList;

        public static ObservableCollection<string> _SortList = new ObservableCollection<string>();
        public ObservableCollection<string> SortCollection => _SortList;

        public static ObservableCollection<string> _CartridgeList = new ObservableCollection<string>();
        public ObservableCollection<string> CartridgeCollection => _CartridgeList;

        public static ObservableCollection<string> _SchemaList = new ObservableCollection<string>();
        public ObservableCollection<string> SchemaCollection => _SchemaList;

        public static bool bAdsDown;
        public static bool bAdsOffline;
        public static bool bRunWOAds;
        public static bool bFirstAdsRun;
        public static string strUser = "Calvin";                                                                            // 07/21/23 PS - Added Log On/Off function
        public static short iLevel = 5;                                                                                     // 07/21/23 PS
        public static string strCurrentUser = "";                                                                           // 07/21/23 PS
        public static int hCurrentUser;                                                                                     // 07/21/23 PS
        public static int hUserChange;                                                                                      // 07/21/23 PS
        public static bool bLoggedOn;                                                                                       // 07/21/23 PS
        public static bool bLocked;                                                                                         // 07/22/23 PS
        public static int hLocked;                                                                                          // 07/22/23 PS
        public static string strLogOnTime;                                                                                  // 07/22/23 PS
        public static int hLogOnTime;                                                                                       // 07/22/23 PS

        public const int ciUnitsCount = 30;
        public const int ciAICount = 29;
        public const int ciDOCount = 23;

        public static int iTotalCycles;
        public static int iCartridgeCount;
        public static int iCartridgePointer;
        public static string[] strCartridgeName = new string[100];
        public static int[] iCartridgeCycles = new int[100];
        public static int iSchemaCount;
        public static int iSchemaPointer;
        public static string[] strSchemaName = new string[100];
        public static bool bProcessData;                                                                                    // 08/20/23 PS
        public static bool bPressureDropTest;                                                                               // 08/20/23 PS

        public static string strComment;
        public static bool bFirstReadComplete;
        public static bool bInProcess;
        public static bool bControlsLoaded;
        public static bool bDisplayPassword;
        public static short iState;
        public static int hState;
        public static short iStatus;
        public static int hStatus;
        public static string strStatusLabel;
        public static int hStatusLabel;
        public static short iCycleCount;
        public static int hCycleCount;
        public static long iTotalCycleCount;
        public static int hTotalCycleCount;
        public static string strActiveAlarm;
        public static int hActiveAlarm;
        public static int hActiveAlarmChange;
        public static bool bActiveAlarm;
        public static bool bErrorOn;
        public static string strAlarm;
        public static short iAlarm;
        public static int hAlarm;
        public static int hAlarmChange;
        public static bool bAdsorpTimeCO2;
        public static int hAdsorpTimeCO2;
        public static float sRemainingTime;
        public static int hRemainingTime;
        public static short iCurrentStep;
        public static int hCurrentStep;

        public static bool bAlarmReset;
        public static bool bHalfSecPulse;
        public static int hHalfSecPulse;
        public static bool bHmiActive;
        public static int hHmiActive;
        public static bool bManualHeat;
        public static int hManualHeat;
        public static bool bTestRunning;
        public static int hTestRunning;
        public static bool bWarmupComplete;
        public static int hWarmupComplete;

        public static bool bCycleLoggingOn;
        public static bool bUploading;
        public static short iLogging = 0;
        public static short iCyclesToRun = 1;

        public static float sHT_PValue;
        public static float sHT_IValue;
        public static float sHT_IRange;
        public static string strData;
        public static string[] strEmail = new string[5];
        public static string strLocation;
        public static string strPassword;
        public static string strCartridge;
        public static bool bCartridgeSaved = true;
        public static string strSchema;
        public static bool bSchemaSaved = true;

        // Graph Inputs
        public static float[] sGraphLow = new float[6];
        public static float[] sGraphHigh = new float[6];
        public static string[] strGraphPoint = new string[6];
        public static int[] iGraphHandle = new int[6];
        public static int[] iSelectedItem = new int[6];
        public static string[] strGraphAlias = new string[6];
        public static string[] strDecimals = new string[6];
        public static string[] strUnits = new string[6];
        public static int[] iColors = new int[6];
        public static bool[] bGraphChanged = new bool[7];
        public static bool bResetGraph;

        // General
        public static TcAdsClient tcClient;
        public static AdsStream dataStream;
        public static BinaryReader binReader;
        public static bool bADS_Active;
        public static bool bRunning;
        public static bool bTCStarted;

        public static string GVT = "Global_Variables_Tags.tg";
        public static string GVTtgr = GVT + "r_";
        public static string GVTtgb = GVT + "b_";
        public static string GVTtgi = GVT + "i_";
        public static string GVTtgs = GVT + "s_";
        public static string GP = "Global_Points.";
        public static string strInactive = "Inactive";
        public static string strOutput = "_Output";
        public static int iMAXLENGTH = 12;

        public static float sSetPoint;
        public static float sPFactor;
        public static float sIFactor;
        public static float sIRange;
        public static float sManualValue;
        public static short iDataLife;
        public static short iSampling = 1;
        public static short iTempAlarm;

        // Setpoints
        public static float sSPAdsorptionTime;
        public static float sSPAdsorptionCO2;
        public static float sSPAdsorptionCO2Time;
        public static float sSPBoilerPressure;
        public static float sSPMinLRPCoolingLoopFlow;
        public static float sSPEvacuationTargetPressure;
        public static float sSPMaxAllowedPressureLeakage;
        public static float sSPReactorRepressurizationPressure;
        public static float sSPAdsorptionTemp;
        public static float sSPAdsorptionVFDOutput;
        public static float sSPMinBypassSteamFlow;
        public static float sSPMinBypassSteamTemp;
        public static float sSPReactorPressureLow;
        public static float sSPReactorPressureHigh;
        public static float sSPReactorValveLowPos;
        public static float sSPReactorValveHighPos;
        public static float sSPSteamValvePercentOpen;
        public static float sSPMaxAllowedSorbentTemp;
        public static float sSPSteamRepressurizationPressure;
        public static float sSPSteamPurgeTime;
        public static float sSPSteamPurgeCutoffTemp;
        public static float sSPEvapCoolingCutoffTemp;
        public static float sSPEvapCoolingTargetPressure;
        public static float sSPLeakCheckTime;
        public static float sSPEvacuationTargetPressureSteam;                                                               // 07/14/23 PS - New set point

        public static float sSP_TEMP_MIN;
        public static float sSP_TEMP_MAX;
        public static float sSP_TIME_MIN;
        public static float sSP_TIME_MAX;
        public static float sSP_CO2_MIN;
        public static float sSP_CO2_MAX;
        public static float sBOILER_PRESSURE_MIN;
        public static float sBOILER_PRESSURE_MAX;
        public static short iCYCLES_MIN = 0;
        public static short iCYCLEs_MAX = 1000;

        #endregion Variables

        #region Points

        public static string[] strAINames = new string[ciAICount];
        public static string[] strAIAliases = new string[ciAICount];
        public static bool[] bUsed = new bool[ciAICount];
        public static string[] strDONames = new string[ciDOCount];
        public static string[] strDOAliases = new string[ciDOCount];

        // Standard Analog in points
        public static string strPt;
        public static string[] AIn000 = new string[10];
        public static float sAIn000;
        public static int hAIn000;
        public static string[] AIn001 = new string[10];
        public static float sAIn001;
        public static int hAIn001;
        public static string[] AIn002 = new string[10];
        public static float sAIn002;
        public static int hAIn002;
        public static string[] AIn003 = new string[10];
        public static float sAIn003;
        public static int hAIn003;
        public static string[] AIn004 = new string[10];
        public static float sAIn004;
        public static int hAIn004;
        public static string[] AIn005 = new string[10];
        public static float sAIn005;
        public static int hAIn005;
        public static string[] AIn006 = new string[10];
        public static float sAIn006;
        public static int hAIn006;
        public static string[] AIn007 = new string[10];
        public static float sAIn007;
        public static int hAIn007;
        public static string[] AIn008 = new string[10];
        public static float sAIn008;
        public static int hAIn008;
        public static string[] AIn009 = new string[10];
        public static float sAIn009;
        public static int hAIn009;
        public static string[] AIn010 = new string[10];
        public static float sAIn010;
        public static int hAIn010;
        public static string[] AIn011 = new string[10];
        public static float sAIn011;
        public static int hAIn011;
        public static string[] AIn012 = new string[10];
        public static float sAIn012;
        public static int hAIn012;
        public static string[] AIn013 = new string[10];
        public static float sAIn013;
        public static int hAIn013;
        public static string[] AIn014 = new string[10];
        public static float sAIn014;
        public static int hAIn014;
        public static string[] AIn015 = new string[10];
        public static float sAIn015;
        public static int hAIn015;
        public static string[] AIn016 = new string[10];
        public static float sAIn016;
        public static int hAIn016;
        public static string[] AIn017 = new string[10];
        public static float sAIn017;
        public static int hAIn017;
        public static string[] AIn018 = new string[10];
        public static float sAIn018;
        public static int hAIn018;
        public static string[] AIn019 = new string[10];
        public static float sAIn019;
        public static int hAIn019;
        public static string[] AIn020 = new string[10];
        public static float sAIn020;
        public static int hAIn020;
        public static string[] AIn021 = new string[10];
        public static float sAIn021;
        public static int hAIn021;
        public static string[] AIn022 = new string[10];
        public static float sAIn022;
        public static int hAIn022;
        public static string[] AIn023 = new string[10];
        public static float sAIn023;
        public static int hAIn023;
        public static string[] AIn024 = new string[10];
        public static float sAIn024;
        public static int hAIn024;
        public static string[] AIn025 = new string[10];
        public static float sAIn025;
        public static int hAIn025;
        public static string[] AIn026 = new string[10];
        public static float sAIn026;
        public static int hAIn026;
        public static string[] AIn027 = new string[10];
        public static float sAIn027;
        public static int hAIn027;
        public static string[] AIn028 = new string[10];
        public static float sAIn028;
        public static int hAIn028;
        public static string[] AIn040 = new string[10];
        public static float sAIn040;
        public static int hAIn040;
        public static string[] AIn041 = new string[10];
        public static float sAIn041;
        public static int hAIn041;
        public static string[] AIn042 = new string[10];
        public static float sAIn042;
        public static int hAIn042;
        public static string[] AIn043 = new string[10];
        public static float sAIn043;
        public static int hAIn043;
        public static string[] AIn044 = new string[10];
        public static float sAIn044;
        public static int hAIn044;
        public static string[] AIn045 = new string[10];
        public static float sAIn045;
        public static int hAIn045;
        public static string[] AIn046 = new string[10];
        public static float sAIn046;
        public static int hAIn046;
        public static string[] AIn050 = new string[10];
        public static float sAIn050;
        public static int hAIn050;
        public static string[] AIn051 = new string[10];
        public static float sAIn051;
        public static int hAIn051;
        public static string[] AIn052 = new string[10];
        public static float sAIn052;
        public static int hAIn052;
        public static string[] AIn053 = new string[10];
        public static float sAIn053;
        public static int hAIn053;
        public static string[] AIn054 = new string[10];
        public static float sAIn054;
        public static int hAIn054;
        public static string[] AIn055 = new string[10];
        public static float sAIn055;
        public static int hAIn055;

        // Temperature points
        public static string[] Temp000 = new string[9];
        public static float sTemp000;
        public static int hTemp000;
        public static bool bTemp000_Error;
        public static int hTemp000_Error;
        public static string[] Temp001 = new string[9];
        public static float sTemp001;
        public static int hTemp001;
        public static bool bTemp001_Error;
        public static int hTemp001_Error;
        public static string[] Temp002 = new string[9];
        public static float sTemp002;
        public static int hTemp002;
        public static bool bTemp002_Error;
        public static int hTemp002_Error;
        public static string[] Temp003 = new string[9];
        public static float sTemp003;
        public static int hTemp003;
        public static bool bTemp003_Error;
        public static int hTemp003_Error;
        public static string[] Temp004 = new string[9];
        public static float sTemp004;
        public static int hTemp004;
        public static bool bTemp004_Error;
        public static int hTemp004_Error;
        public static string[] Temp005 = new string[9];
        public static float sTemp005;
        public static int hTemp005;
        public static bool bTemp005_Error;
        public static int hTemp005_Error;
        public static string[] Temp006 = new string[9];
        public static float sTemp006;
        public static int hTemp006;
        public static bool bTemp006_Error;
        public static int hTemp006_Error;
        public static string[] Temp007 = new string[9];
        public static float sTemp007;
        public static int hTemp007;
        public static bool bTemp007_Error;
        public static int hTemp007_Error;
        public static string[] Temp008 = new string[9];
        public static float sTemp008;
        public static int hTemp008;
        public static bool bTemp008_Error;
        public static int hTemp008_Error;
        public static string[] Temp009 = new string[9];
        public static float sTemp009;
        public static int hTemp009;
        public static bool bTemp009_Error;
        public static int hTemp009_Error;
        public static string[] Temp010 = new string[9];
        public static float sTemp010;
        public static int hTemp010;
        public static bool bTemp010_Error;
        public static int hTemp010_Error;
        public static string[] Temp011 = new string[9];
        public static float sTemp011;
        public static int hTemp011;
        public static bool bTemp011_Error;
        public static int hTemp011_Error;
        public static string[] Temp012 = new string[9];
        public static float sTemp012;
        public static int hTemp012;
        public static bool bTemp012_Error;
        public static int hTemp012_Error;
        public static string[] Temp013 = new string[9];
        public static float sTemp013;
        public static int hTemp013;
        public static bool bTemp013_Error;
        public static int hTemp013_Error;
        public static string[] Temp014 = new string[9];
        public static float sTemp014;
        public static int hTemp014;
        public static bool bTemp014_Error;
        public static int hTemp014_Error;
        public static string[] Temp015 = new string[9];
        public static float sTemp015;
        public static int hTemp015;
        public static bool bTemp015_Error;
        public static int hTemp015_Error;
        public static string[] Temp016 = new string[9];
        public static float sTemp016;
        public static int hTemp016;
        public static bool bTemp016_Error;
        public static int hTemp016_Error;
        public static string[] Temp017 = new string[9];
        public static float sTemp017;
        public static int hTemp017;
        public static bool bTemp017_Error;
        public static int hTemp017_Error;
        public static string[] Temp018 = new string[9];
        public static float sTemp018;
        public static int hTemp018;
        public static bool bTemp018_Error;
        public static int hTemp018_Error;
        public static string[] Temp019 = new string[9];
        public static float sTemp019;
        public static int hTemp019;
        public static bool bTemp019_Error;
        public static int hTemp019_Error;
        public static string[] Temp020 = new string[9];
        public static float sTemp020;
        public static int hTemp020;
        public static bool bTemp020_Error;
        public static int hTemp020_Error;
        public static string[] Temp021 = new string[9];
        public static float sTemp021;
        public static int hTemp021;
        public static bool bTemp021_Error;
        public static int hTemp021_Error;
        public static string[] Temp022 = new string[9];
        public static float sTemp022;
        public static int hTemp022;
        public static bool bTemp022_Error;
        public static int hTemp022_Error;
        public static string[] Temp023 = new string[9];
        public static float sTemp023;
        public static int hTemp023;
        public static bool bTemp023_Error;
        public static int hTemp023_Error;
        public static string[] Temp024 = new string[9];
        public static float sTemp024;
        public static int hTemp024;
        public static bool bTemp024_Error;
        public static int hTemp024_Error;
        public static string[] Temp025 = new string[9];
        public static float sTemp025;
        public static int hTemp025;
        public static bool bTemp025_Error;
        public static int hTemp025_Error;
        public static string[] Temp026 = new string[9];
        public static float sTemp026;
        public static int hTemp026;
        public static bool bTemp026_Error;
        public static int hTemp026_Error;

        // Analog out points
        public static string[] AOut000 = new string[9];
        public static float sAOut000;
        public static int hAOut000;
        public static string[] AOut000RPM = new string[9];
        public static float sAOut000RPM;
        public static int hAOut000RPM;
        public static bool bHS_1102_Enabled;
        public static int hHS_1102_Enabled;
        public static bool bHS_1102_Running;
        public static int hHS_1102_Running;

        public static bool bAOutChange;
        public static float sAOut000_Out;
        public static string[] AOut001 = new string[9];
        public static float sAOut001;
        public static int hAOut001;
        public static float sAOut001_SP;
        public static int hAOut001_Output;
        public static bool bAOut001_On;
        public static int hAOut001_On;
        public static string[] AOut002 = new string[9];
        public static float sAOut002;
        public static int hAOut002;
        public static float sAOut002_SP;
        public static int hAOut002_Output;
        public static bool bAOut002_On;
        public static int hAOut002_On;
        public static string[] AOut003 = new string[9];
        public static float sAOut003;
        public static int hAOut003;
        public static float sAOut003_SP;
        public static int hAOut003_Output;
        public static bool bAOut003_On;
        public static int hAOut003_On;
        public static string[] AOut004 = new string[9];
        public static float sAOut004;
        public static int hAOut004;
        public static float sAOut004_SP;
        public static int hAOut004_Output;
        public static bool bAOut004_On;
        public static int hAOut004_On;
        public static string[] AOut005 = new string[9];
        public static float sAOut005;
        public static int hAOut005;
        public static float sAOut005_SP;
        public static int hAOut005_Output;
        public static bool bAOut005_On;
        public static int hAOut005_On;
        public static string[] AOut006 = new string[9];
        public static float sAOut006;
        public static int hAOut006;
        public static float sAOut006_SP;
        public static int hAOut006_Output;
        public static bool bAOut006_On;
        public static int hAOut006_On;
        public static string[] AOut007 = new string[9];
        public static float sAOut007;
        public static int hAOut007;
        public static float sAOut007_Out;
        public static string[] AOut008 = new string[9];
        public static float sAOut008;
        public static int hAOut008;
        public static float sAOut008_Out;
        public static string[] AOut009 = new string[9];
        public static float sAOut009;
        public static int hAOut009;
        public static float sAOut009_Out;
        public static string[] AOut010 = new string[9];
        public static float sAOut010;
        public static int hAOut010;
        public static float sAOut010_Out;

        // Digital in points
        public static bool bDI000_Open;
        public static int hDI000_Open;
        public static bool bDI000_Closed;
        public static int hDI000_Closed;
        public static bool bDI001_Open;
        public static int hDI001_Open;
        public static bool bDI001_Closed;
        public static int hDI001_Closed;

        // Digital out points
        public static string[] DO000 = new string[3];
        public static bool bDO000;
        public static int hDO000;
        public static string[] DO001 = new string[3];
        public static bool bDO001;
        public static int hDO001;
        public static string[] DO002 = new string[3];
        public static bool bDO002;
        public static int hDO002;
        public static string[] DO003 = new string[3];
        public static bool bDO003;
        public static int hDO003;
        public static string[] DO004 = new string[3];
        public static bool bDO004;
        public static int hDO004;
        public static bool bDO004_Open;
        public static int hDO004_Open;
        public static bool bDO004_Closed;
        public static int hDO004_Closed;
        public static string[] DO005 = new string[3];
        public static bool bDO005;
        public static int hDO005;
        public static bool bDO005_Open;
        public static int hDO005_Open;
        public static bool bDO005_Closed;
        public static int hDO005_Closed;
        public static string[] DO006 = new string[3];
        public static bool bDO006;
        public static int hDO006;
        public static bool bDO006_Open;
        public static int hDO006_Open;
        public static bool bDO006_Closed;
        public static int hDO006_Closed;
        public static string[] DO007 = new string[3];
        public static bool bDO007;
        public static int hDO007;
        public static bool bDO007_LSH;
        public static int hDO007_LSH;
        public static bool bDO007_LSL;
        public static int hDO007_LSL;
        public static string[] DO008 = new string[3];
        public static bool bDO008;
        public static int hDO008;
        public static bool bDO008_Open;
        public static int hDO008_Open;
        public static bool bDO008_Closed;
        public static int hDO008_Closed;
        public static string[] DO009 = new string[3];
        public static bool bDO009;
        public static int hDO009;
        public static bool bDO009_Open;
        public static int hDO009_Open;
        public static bool bDO009_Closed;
        public static int hDO009_Closed;
        public static string[] DO010 = new string[3];
        public static bool bDO010;
        public static int hDO010;
        public static string[] DO011 = new string[3];
        public static bool bDO011;
        public static int hDO011;
        public static bool bDO011_LSH;
        public static int hDO011_LSH;
        public static bool bDO011_LSL;
        public static int hDO011_LSL;
        public static string[] DO012 = new string[3];
        public static bool bDO012;
        public static int hDO012;
        public static string[] DO013 = new string[3];
        public static bool bDO013;
        public static int hDO013;
        public static bool bDO013_Open;
        public static int hDO013_Open;
        public static bool bDO013_Closed;
        public static int hDO013_Closed;
        public static string[] DO014 = new string[3];
        public static bool bDO014;
        public static int hDO014;
        public static bool bDO014_Open;
        public static int hDO014_Open;
        public static bool bDO014_Closed;
        public static int hDO014_Closed;
        public static string[] DO015 = new string[3];
        public static bool bDO015;
        public static int hDO015;
        public static string[] DO016 = new string[3];
        public static bool bDO016;
        public static int hDO016;
        public static bool bDO016_Open;
        public static int hDO016_Open;
        public static bool bDO016_Closed;
        public static int hDO016_Closed;
        public static string[] DO017 = new string[3];
        public static bool bDO017;
        public static int hDO017;
        public static bool bDO017_Open;
        public static int hDO017_Open;
        public static bool bDO017_Closed;
        public static int hDO017_Closed;
        public static string[] DO018 = new string[3];
        public static bool bDO018;
        public static int hDO018;
        public static bool bDO018_LSH;
        public static int hDO018_LSH;
        public static bool bDO018_LSL;
        public static int hDO018_LSL;
        public static string[] DO019 = new string[3];
        public static bool bDO019;
        public static int hDO019;
        public static string[] DO020 = new string[3];
        public static bool bDO020;
        public static int hDO020;
        public static string[] DO021 = new string[3];
        public static bool bDO021;
        public static int hDO021;
        public static bool bDO021_Open;
        public static int hDO021_Open;
        public static bool bDO021_Closed;
        public static int hDO021_Closed;
        public static string[] DO022 = new string[3];
        public static bool bDO022;
        public static int hDO022;
        public static bool bDO022_Open;
        public static int hDO022_Open;
        public static bool bDO022_Closed;
        public static int hDO022_Closed;

        // Weather Station
        public static float sMI_2001;
        public static float sTI_2001;
        public static float sSI_2001;
        public static float sZI_2001;
        public static float sPI_2001;
        public static float sZXI_2002;
        public static float sZYI_2002;

        // Current & Voltage
        public static float sIT_1;
        public static int hIT_1;
        public static float sIT_2;
        public static int hIT_2;
        public static float sIT_3;
        public static int hIT_3;
        public static float sVT_1;
        public static int hVT_1;
        public static float sVT_2;
        public static int hVT_2;
        public static float sVT_3;
        public static int hVT_3;

        #endregion Points

        #region TcAds Services

        public static bool TcAdsServices()
        {
            bool bLoadItems = false;
            bool bReturn = false;
            try
            {
                tcClient = new TcAdsClient();
                tcClient.Connect("10.12.1.55.1.1", 851);            // Connect to TC3 PLC port - Calvin/Home CX9020
                //tcClient.Connect(851);                            // Connect to TC3 PLC port - Use when AMS Net ID can be changed

                if (AdsCommDown())
                {
                    if (bRunWOAds)
                    {
                        App.WPFMessageBoxOK("No communication with TwinCAT program - program will close!", App.bgRed);
                    }
                    else
                    {
                        if (App.WPFMessageBoxYesNo("No communication with TwinCAT program - Continue", App.bgRed))
                        {
                            bRunWOAds = true;
                            bLoadItems = true;
                        }
                    }
                }
                else
                {
                    CreateHandles();
                    LoadNotifications();
                    bLoadItems = true;
                    bTCStarted = true;
                    bAdsOffline = false;
                    bRunWOAds = false;
                    bFirstAdsRun = true;
                    bReturn = true;
                }
                if (bLoadItems)
                {
                    LoadPointCollection();
                    LoadGraphCombobox();
                    LoadComboBoxes();
                    LoadComboCartridges();
                    LoadComboSchemas();
                    bResetGraph = true;
                }
            }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                {
                    App.WPFMessageBoxOK("Error - " + ex.ToString(), App.bgRed);
                }
            }
            return bReturn;
        }

        public static void CreateHandles()
        {
            hAIn000 = tcClient.CreateVariableHandle(GVTtgr + AIn000[0]);
            hAIn001 = tcClient.CreateVariableHandle(GVTtgr + AIn001[0]);
            hAIn002 = tcClient.CreateVariableHandle(GVTtgr + AIn002[0]);
            hAIn003 = tcClient.CreateVariableHandle(GVTtgr + AIn003[0]);
            hAIn004 = tcClient.CreateVariableHandle(GVTtgr + AIn004[0]);
            hAIn005 = tcClient.CreateVariableHandle(GVTtgr + AIn005[0]);
            hAIn006 = tcClient.CreateVariableHandle(GVTtgr + AIn006[0]);
            hAIn007 = tcClient.CreateVariableHandle(GVTtgr + AIn007[0]);
            hAIn008 = tcClient.CreateVariableHandle(GVTtgr + AIn008[0]);
            hAIn009 = tcClient.CreateVariableHandle(GVTtgr + AIn009[0]);
            hAIn010 = tcClient.CreateVariableHandle(GVTtgr + AIn010[0]);
            hAIn011 = tcClient.CreateVariableHandle(GVTtgr + AIn011[0]);
            hAIn012 = tcClient.CreateVariableHandle(GVTtgr + AIn012[0]);
            hAIn013 = tcClient.CreateVariableHandle(GVTtgr + AIn013[0]);
            hAIn014 = tcClient.CreateVariableHandle(GVTtgr + AIn014[0]);
            hAIn015 = tcClient.CreateVariableHandle(GVTtgr + AIn015[0]);
            hAIn016 = tcClient.CreateVariableHandle(GVTtgr + AIn016[0]);
            hAIn017 = tcClient.CreateVariableHandle(GVTtgr + AIn017[0]);
            hAIn018 = tcClient.CreateVariableHandle(GVTtgr + AIn018[0]);
            hAIn019 = tcClient.CreateVariableHandle(GVTtgr + AIn019[0]);
            hAIn020 = tcClient.CreateVariableHandle(GVTtgr + AIn020[0]);
            hAIn021 = tcClient.CreateVariableHandle(GVTtgr + AIn021[0]);
            hAIn022 = tcClient.CreateVariableHandle(GVTtgr + AIn022[0]);
            hAIn023 = tcClient.CreateVariableHandle(GVTtgr + AIn023[0]);
            hAIn024 = tcClient.CreateVariableHandle(GVTtgr + AIn024[0]);
            hAIn025 = tcClient.CreateVariableHandle(GVTtgr + AIn025[0]);
            hAIn026 = tcClient.CreateVariableHandle(GVTtgr + AIn026[0]);
            hAIn027 = tcClient.CreateVariableHandle(GVTtgr + AIn027[0]);
            hAIn028 = tcClient.CreateVariableHandle(GVTtgr + AIn028[0]);
            hAIn040 = tcClient.CreateVariableHandle(GVTtgr + AIn040[0]);
            hAIn041 = tcClient.CreateVariableHandle(GVTtgr + AIn041[0]);
            hAIn042 = tcClient.CreateVariableHandle(GVTtgr + AIn042[0]);
            hAIn043 = tcClient.CreateVariableHandle(GVTtgr + AIn043[0]);
            hAIn044 = tcClient.CreateVariableHandle(GVTtgr + AIn044[0]);
            hAIn045 = tcClient.CreateVariableHandle(GVTtgr + AIn045[0]);
            hAIn046 = tcClient.CreateVariableHandle(GVTtgr + AIn046[0]);
            hAIn050 = tcClient.CreateVariableHandle(GVTtgr + AIn050[0]);
            hAIn051 = tcClient.CreateVariableHandle(GVTtgr + AIn051[0]);
            hAIn052 = tcClient.CreateVariableHandle(GVTtgr + AIn052[0]);
            hAIn053 = tcClient.CreateVariableHandle(GVTtgr + AIn053[0]);
            hAIn054 = tcClient.CreateVariableHandle(GVTtgr + AIn054[0]);
            hAIn055 = tcClient.CreateVariableHandle(GVTtgr + AIn055[0]);

            hTemp000 = tcClient.CreateVariableHandle(GVTtgr + Temp000[0]);
            hTemp001 = tcClient.CreateVariableHandle(GVTtgr + Temp001[0]);
            hTemp002 = tcClient.CreateVariableHandle(GVTtgr + Temp002[0]);
            hTemp003 = tcClient.CreateVariableHandle(GVTtgr + Temp003[0]);
            hTemp004 = tcClient.CreateVariableHandle(GVTtgr + Temp004[0]);
            hTemp005 = tcClient.CreateVariableHandle(GVTtgr + Temp005[0]);
            hTemp006 = tcClient.CreateVariableHandle(GVTtgr + Temp006[0]);
            hTemp007 = tcClient.CreateVariableHandle(GVTtgr + Temp007[0]);
            hTemp008 = tcClient.CreateVariableHandle(GVTtgr + Temp008[0]);
            hTemp009 = tcClient.CreateVariableHandle(GVTtgr + Temp009[0]);
            hTemp010 = tcClient.CreateVariableHandle(GVTtgr + Temp010[0]);
            hTemp011 = tcClient.CreateVariableHandle(GVTtgr + Temp011[0]);
            hTemp012 = tcClient.CreateVariableHandle(GVTtgr + Temp012[0]);
            hTemp013 = tcClient.CreateVariableHandle(GVTtgr + Temp013[0]);
            hTemp014 = tcClient.CreateVariableHandle(GVTtgr + Temp014[0]);
            hTemp015 = tcClient.CreateVariableHandle(GVTtgr + Temp015[0]);
            hTemp016 = tcClient.CreateVariableHandle(GVTtgr + Temp016[0]);
            hTemp017 = tcClient.CreateVariableHandle(GVTtgr + Temp017[0]);
            hTemp018 = tcClient.CreateVariableHandle(GVTtgr + Temp018[0]);
            hTemp019 = tcClient.CreateVariableHandle(GVTtgr + Temp019[0]);
            hTemp020 = tcClient.CreateVariableHandle(GVTtgr + Temp020[0]);
            hTemp021 = tcClient.CreateVariableHandle(GVTtgr + Temp021[0]);
            hTemp022 = tcClient.CreateVariableHandle(GVTtgr + Temp022[0]);
            hTemp023 = tcClient.CreateVariableHandle(GVTtgr + Temp023[0]);
            hTemp024 = tcClient.CreateVariableHandle(GVTtgr + Temp024[0]);
            hTemp025 = tcClient.CreateVariableHandle(GVTtgr + Temp025[0]);
            hTemp026 = tcClient.CreateVariableHandle(GVTtgr + Temp026[0]);

            hAOut001_Output = tcClient.CreateVariableHandle(GVTtgr + "HT_1_Ctrl");
            hAOut002_Output = tcClient.CreateVariableHandle(GVTtgr + "HT_2_Ctrl");
            hAOut003_Output = tcClient.CreateVariableHandle(GVTtgr + "HT_3_Ctrl");
            hAOut004_Output = tcClient.CreateVariableHandle(GVTtgr + "HT_4_Ctrl");
            hAOut005_Output = tcClient.CreateVariableHandle(GVTtgr + "HT_5_Ctrl");
            hAOut006_Output = tcClient.CreateVariableHandle(GVTtgr + "HT_6_Ctrl");

            hAOut000RPM = tcClient.CreateVariableHandle(GVTtgr + AOut000RPM[0]);

            hIT_1 = tcClient.CreateVariableHandle(GVTtgr + "IT_1");
            hIT_2 = tcClient.CreateVariableHandle(GVTtgr + "IT_2");
            hIT_3 = tcClient.CreateVariableHandle(GVTtgr + "IT_3");
            hVT_1 = tcClient.CreateVariableHandle(GVTtgr + "VT_1");
            hVT_2 = tcClient.CreateVariableHandle(GVTtgr + "VT_2");
            hVT_3 = tcClient.CreateVariableHandle(GVTtgr + "VT_3");

            hActiveAlarm = tcClient.CreateVariableHandle(GVTtgs + "Active_Alarm");
            hAlarm = tcClient.CreateVariableHandle(GVTtgs + "Alarm");
            hCurrentUser = tcClient.CreateVariableHandle(GVTtgs + "Current_User");                                          // 07/21/23 PS
            hLogOnTime = tcClient.CreateVariableHandle(GVTtgs + "Hmi_Log_Date_Time");                                       // 07/22/23 PS
            hStatusLabel = tcClient.CreateVariableHandle(GVTtgs + "Status");
            hRemainingTime = tcClient.CreateVariableHandle(GVTtgr + "Remaining_Time");
            hCurrentStep = tcClient.CreateVariableHandle(GVTtgi + "Current_Step");
        }

        public static void LoadNotifications()
        {
            dataStream = new AdsStream(220);                                                                                // 07/22/23 PS
            binReader = new BinaryReader(dataStream, System.Text.Encoding.ASCII);
            hHalfSecPulse = tcClient.AddDeviceNotification(GVTtgb + "Half_Sec_Pulse", dataStream, 0, 1, AdsTransMode.OnChange, 100, 0, null);
            string s = GVTtgb + Temp000[0] + "_Error";
            hTemp000_Error = tcClient.AddDeviceNotification(s, dataStream, 1, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp001[0] + "_Error";
            hTemp001_Error = tcClient.AddDeviceNotification(s, dataStream, 2, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp002[0] + "_Error";
            hTemp002_Error = tcClient.AddDeviceNotification(s, dataStream, 3, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp003[0] + "_Error";
            hTemp003_Error = tcClient.AddDeviceNotification(s, dataStream, 4, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp004[0] + "_Error";
            hTemp004_Error = tcClient.AddDeviceNotification(s, dataStream, 5, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp005[0] + "_Error";
            hTemp005_Error = tcClient.AddDeviceNotification(s, dataStream, 6, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp006[0] + "_Error";
            hTemp006_Error = tcClient.AddDeviceNotification(s, dataStream, 7, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp007[0] + "_Error";
            hTemp007_Error = tcClient.AddDeviceNotification(s, dataStream, 8, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp008[0] + "_Error";
            hTemp008_Error = tcClient.AddDeviceNotification(s, dataStream, 9, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp009[0] + "_Error";
            hTemp009_Error = tcClient.AddDeviceNotification(s, dataStream, 10, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp010[0] + "_Error";
            hTemp010_Error = tcClient.AddDeviceNotification(s, dataStream, 11, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp011[0] + "_Error";
            hTemp011_Error = tcClient.AddDeviceNotification(s, dataStream, 12, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp012[0] + "_Error";
            hTemp012_Error = tcClient.AddDeviceNotification(s, dataStream, 13, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp013[0] + "_Error";
            hTemp013_Error = tcClient.AddDeviceNotification(s, dataStream, 14, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp014[0] + "_Error";
            hTemp014_Error = tcClient.AddDeviceNotification(s, dataStream, 15, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp015[0] + "_Error";
            hTemp015_Error = tcClient.AddDeviceNotification(s, dataStream, 16, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp016[0] + "_Error";
            hTemp016_Error = tcClient.AddDeviceNotification(s, dataStream, 17, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + AOut008[0] + "_Open";
            hDI000_Open = tcClient.AddDeviceNotification(s, dataStream, 18, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + AOut008[0] + "_Closed";
            hDI000_Closed = tcClient.AddDeviceNotification(s, dataStream, 19, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + AOut009[0] + "_Open";
            hDI001_Open = tcClient.AddDeviceNotification(s, dataStream, 20, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + AOut009[0] + "_Closed";
            hDI001_Closed = tcClient.AddDeviceNotification(s, dataStream, 21, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO000[0] + "_On";
            hDO000 = tcClient.AddDeviceNotification(s, dataStream, 22, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO001[0] + "_On";
            hDO001 = tcClient.AddDeviceNotification(s, dataStream, 23, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO002[0] + "_On";
            hDO002 = tcClient.AddDeviceNotification(s, dataStream, 24, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO003[0] + "_On";
            hDO003 = tcClient.AddDeviceNotification(s, dataStream, 25, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO004[0] + "_On";
            hDO004 = tcClient.AddDeviceNotification(s, dataStream, 26, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO004[0] + "_Open";
            hDO004_Open = tcClient.AddDeviceNotification(s, dataStream, 27, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO004[0] + "_Closed";
            hDO004_Closed = tcClient.AddDeviceNotification(s, dataStream, 28, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO005[0] + "_On";
            hDO005 = tcClient.AddDeviceNotification(s, dataStream, 29, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO005[0] + "_Open";
            hDO005_Open = tcClient.AddDeviceNotification(s, dataStream, 30, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO005[0] + "_Closed";
            hDO005_Closed = tcClient.AddDeviceNotification(s, dataStream, 30, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO006[0] + "_On";
            hDO006 = tcClient.AddDeviceNotification(s, dataStream, 32, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO006[0] + "_Open";
            hDO006_Open = tcClient.AddDeviceNotification(s, dataStream, 33, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO006[0] + "_Closed";
            hDO006_Closed = tcClient.AddDeviceNotification(s, dataStream, 34, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO007[0] + "_On";
            hDO007 = tcClient.AddDeviceNotification(s, dataStream, 35, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO007[0] + "_LSH";
            hDO007_LSH = tcClient.AddDeviceNotification(s, dataStream, 36, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO007[0] + "_LSL";
            hDO007_LSL = tcClient.AddDeviceNotification(s, dataStream, 37, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO008[0] + "_On";
            hDO008 = tcClient.AddDeviceNotification(s, dataStream, 38, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO008[0] + "_Open";
            hDO008_Open = tcClient.AddDeviceNotification(s, dataStream, 39, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO008[0] + "_Closed";
            hDO008_Closed = tcClient.AddDeviceNotification(s, dataStream, 40, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO009[0] + "_On";
            hDO009 = tcClient.AddDeviceNotification(s, dataStream, 41, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO009[0] + "_Open";
            hDO009_Open = tcClient.AddDeviceNotification(s, dataStream, 42, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO009[0] + "_Closed";
            hDO009_Closed = tcClient.AddDeviceNotification(s, dataStream, 43, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO010[0] + "_On";
            hDO010 = tcClient.AddDeviceNotification(s, dataStream, 44, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO011[0] + "_LSH";
            hDO011_LSH = tcClient.AddDeviceNotification(s, dataStream, 45, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO011[0] + "_On";
            hDO011 = tcClient.AddDeviceNotification(s, dataStream, 46, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO011[0] + "_LSL";
            hDO011_LSL = tcClient.AddDeviceNotification(s, dataStream, 47, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO012[0] + "_On";
            hDO012 = tcClient.AddDeviceNotification(s, dataStream, 48, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO013[0] + "_On";
            hDO013 = tcClient.AddDeviceNotification(s, dataStream, 49, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO013[0] + "_Open";
            hDO013_Open = tcClient.AddDeviceNotification(s, dataStream, 50, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO013[0] + "_Closed";
            hDO013_Closed = tcClient.AddDeviceNotification(s, dataStream, 51, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO014[0] + "_On";
            hDO014 = tcClient.AddDeviceNotification(s, dataStream, 52, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO014[0] + "_Open";
            hDO014_Open = tcClient.AddDeviceNotification(s, dataStream, 53, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO014[0] + "_Closed";
            hDO014_Closed = tcClient.AddDeviceNotification(s, dataStream, 54, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO015[0] + "_On";
            hDO015 = tcClient.AddDeviceNotification(s, dataStream, 55, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO016[0] + "_On";
            hDO016 = tcClient.AddDeviceNotification(s, dataStream, 56, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO016[0] + "_Open";
            hDO016_Open = tcClient.AddDeviceNotification(s, dataStream, 57, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO016[0] + "_Closed";
            hDO016_Closed = tcClient.AddDeviceNotification(s, dataStream, 58, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO017[0] + "_On";
            hDO017 = tcClient.AddDeviceNotification(s, dataStream, 59, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO017[0] + "_Open";
            hDO017_Open = tcClient.AddDeviceNotification(s, dataStream, 60, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO017[0] + "_Closed";
            hDO017_Closed = tcClient.AddDeviceNotification(s, dataStream, 61, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO018[0] + "_LSH";
            hDO018_LSH = tcClient.AddDeviceNotification(s, dataStream, 62, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO018[0] + "_On";
            hDO018 = tcClient.AddDeviceNotification(s, dataStream, 63, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO018[0] + "_LSL";
            hDO018_LSL = tcClient.AddDeviceNotification(s, dataStream, 64, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO019[0] + "_On";
            hDO019 = tcClient.AddDeviceNotification(s, dataStream, 65, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO020[0] + "_On";
            hDO020 = tcClient.AddDeviceNotification(s, dataStream, 66, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO021[0] + "_On";
            hDO021 = tcClient.AddDeviceNotification(s, dataStream, 67, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO021[0] + "_Open";
            hDO021_Open = tcClient.AddDeviceNotification(s, dataStream, 68, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO021[0] + "_Closed";
            hDO021_Closed = tcClient.AddDeviceNotification(s, dataStream, 69, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + DO022[0] + "_On";                                                                                  // 09/20/23
            hDO022 = tcClient.AddDeviceNotification(s, dataStream, 70, 1, AdsTransMode.OnChange, 100, 0, null);             // 09/20/23
            s = GVTtgb + DO022[0] + "_Open";                                                                                // 09/20/23
            hDO022_Open = tcClient.AddDeviceNotification(s, dataStream, 71, 1, AdsTransMode.OnChange, 100, 0, null);        // 09/20/23
            s = GVTtgb + DO022[0] + "_Closed";                                                                              // 09/20/23
            hDO022_Closed = tcClient.AddDeviceNotification(s, dataStream, 72, 1, AdsTransMode.OnChange, 100, 0, null);      // 09/20/23
            s = GVTtgb + AOut001[0] + "_On";
            hAOut001_On = tcClient.AddDeviceNotification(s, dataStream, 73, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + AOut002[0] + "_On";
            hAOut002_On = tcClient.AddDeviceNotification(s, dataStream, 74, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + AOut003[0] + "_On";
            hAOut003_On = tcClient.AddDeviceNotification(s, dataStream, 75, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + AOut004[0] + "_On";
            hAOut004_On = tcClient.AddDeviceNotification(s, dataStream, 76, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + AOut005[0] + "_On";
            hAOut005_On = tcClient.AddDeviceNotification(s, dataStream, 77, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + AOut006[0] + "_On";
            hAOut006_On = tcClient.AddDeviceNotification(s, dataStream, 78, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp017[0] + "_Error";
            hTemp017_Error = tcClient.AddDeviceNotification(s, dataStream, 79, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp018[0] + "_Error";
            hTemp018_Error = tcClient.AddDeviceNotification(s, dataStream, 80, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp019[0] + "_Error";
            hTemp019_Error = tcClient.AddDeviceNotification(s, dataStream, 81, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp020[0] + "_Error";
            hTemp020_Error = tcClient.AddDeviceNotification(s, dataStream, 82, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp021[0] + "_Error";
            hTemp021_Error = tcClient.AddDeviceNotification(s, dataStream, 83, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp022[0] + "_Error";
            hTemp022_Error = tcClient.AddDeviceNotification(s, dataStream, 84, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp023[0] + "_Error";
            hTemp023_Error = tcClient.AddDeviceNotification(s, dataStream, 85, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp024[0] + "_Error";
            hTemp024_Error = tcClient.AddDeviceNotification(s, dataStream, 86, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp025[0] + "_Error";
            hTemp025_Error = tcClient.AddDeviceNotification(s, dataStream, 87, 1, AdsTransMode.OnChange, 100, 0, null);
            s = GVTtgb + Temp026[0] + "_Error";
            hTemp026_Error = tcClient.AddDeviceNotification(s, dataStream, 88, 1, AdsTransMode.OnChange, 100, 0, null);
            hHS_1102_Enabled = tcClient.AddDeviceNotification(GVTtgb + "HS_1102_Enabled", dataStream, 89, 1, AdsTransMode.OnChange, 100, 0, null);
            hHS_1102_Running = tcClient.AddDeviceNotification(GVTtgb + "HS_1102_Running", dataStream, 90, 1, AdsTransMode.OnChange, 100, 0, null);
            hHmiActive = tcClient.AddDeviceNotification(GVTtgb + "Hmi_Active", dataStream, 91, 1, AdsTransMode.OnChange, 100, 0, null);
            hManualHeat = tcClient.AddDeviceNotification(GVTtgb + "Manual_Heater_Mode", dataStream, 92, 1, AdsTransMode.OnChange, 100, 0, null);
            hTestRunning = tcClient.AddDeviceNotification(GVTtgb + "Test_Running", dataStream, 93, 1, AdsTransMode.OnChange, 100, 0, null);
            hWarmupComplete = tcClient.AddDeviceNotification(GVTtgb + "Warmup_Complete", dataStream, 94, 1, AdsTransMode.OnChange, 100, 0, null);
            hAdsorpTimeCO2 = tcClient.AddDeviceNotification(GVTtgb + "Adsorption_CO2", dataStream, 95, 1, AdsTransMode.OnChange, 100, 0, null);
            hLocked = tcClient.AddDeviceNotification(GVTtgb + "Hmi_Locked", dataStream, 96, 1, AdsTransMode.OnChange, 100, 0, null);                               // 07/22/23 PS
            hStatus = tcClient.AddDeviceNotification(GVTtgi + "Status", dataStream, 97, 2, AdsTransMode.OnChange, 100, 0, null);
            hState = tcClient.AddDeviceNotification(GVTtgi + "Machine_State", dataStream, 99, 2, AdsTransMode.OnChange, 100, 0, null);
            hCycleCount = tcClient.AddDeviceNotification(GVTtgi + "Cycle_Count", dataStream, 101, 2, AdsTransMode.OnChange, 100, 0, null);
            hTotalCycleCount = tcClient.AddDeviceNotification(GVTtgi + "Total_Cycle_Count", dataStream, 103, 4, AdsTransMode.OnChange, 100, 0, null);
            hActiveAlarmChange = tcClient.AddDeviceNotification(GVTtgb + "Active_Alarm", dataStream, 107, 1, AdsTransMode.OnChange, 100, 0, null);
            hAOut000 = tcClient.AddDeviceNotification(GVTtgr + AOut000[0] + strOutput, dataStream, 108, 4, AdsTransMode.OnChange, 250, 0, null);
            hAOut007 = tcClient.AddDeviceNotification(GVTtgr + AOut007[0] + strOutput, dataStream, 112, 4, AdsTransMode.OnChange, 250, 0, null);
            hAOut008 = tcClient.AddDeviceNotification(GVTtgr + AOut008[0] + strOutput, dataStream, 116, 4, AdsTransMode.OnChange, 250, 0, null);
            hAOut009 = tcClient.AddDeviceNotification(GVTtgr + AOut009[0] + strOutput, dataStream, 120, 4, AdsTransMode.OnChange, 250, 0, null);
            hAOut010 = tcClient.AddDeviceNotification(GVTtgr + AOut010[0] + strOutput, dataStream, 124, 4, AdsTransMode.OnChange, 250, 0, null);
            hAlarmChange = tcClient.AddDeviceNotification(GVTtgs + "Alarm", dataStream, 128, 71, AdsTransMode.OnChange, 100, 0, null);
            hUserChange = tcClient.AddDeviceNotification(GVTtgs + "Current_User", dataStream, 199, 21, AdsTransMode.OnChange, 250, 0, null);    // 07/21/23 PS
            tcClient.AdsNotification += new AdsNotificationEventHandler(TcClient_OnNotification);
        }

        public static void TcClient_OnNotification(object sender, AdsNotificationEventArgs e)
        {
            try
            {
                if (e.NotificationHandle == hHalfSecPulse)
                {
                    ReadAdsValues();
                    MainWindow.AppWindow.UpdateValues();
                    bHalfSecPulse = binReader.ReadBoolean();
                    if (bFirstAdsRun)
                    {
                        SetTCPointValues();
                        SetTCSPValues();
                        SetAdsValue(GVTtgi + "Cycles_To_Run", iCyclesToRun);
                        SetAdsValue(GVTtgb + "ADS_Active", true);
                        bFirstAdsRun = false;
                    }
                    SetAdsValue(GVTtgb + "Watchdog", false);
                }
                else if (e.NotificationHandle == hState)
                {
                    iState = binReader.ReadInt16();
                    bInProcess = iState > (short)State.eIdle && iState <= (short)State.eAdsorption;
                    iLogging = iState == (short)State.eIdle ? (short)LogType.No_Log : iLogging;
                    MainWindow.AppWindow.UpdateValues();
                }
                else if (e.NotificationHandle == hActiveAlarmChange)
                {
                    bool alarm = binReader.ReadBoolean();
                    if (alarm)
                    {
                        strActiveAlarm = ReadAdsString(hActiveAlarm, 70);
                        string[] a = strActiveAlarm.Split('/');
                        iAlarm = (short)Convert.ToInt32(a[0]);
                        strActiveAlarm = "";
                        for (int i = 0; i < a.Length; i++)
                        {
                            strActiveAlarm += a[i];
                        }
                        bActiveAlarm = true;
                        iLogging = (short)LogType.No_Log;
                        bCycleLoggingOn = false;
                    }
                    else
                    {
                        iAlarm = 0;
                    }
                }
                else if (e.NotificationHandle == hAlarmChange)
                {
                    bErrorOn = true;
                    strAlarm = ReadAdsString(hAlarm, 70);
                    string[] a = strAlarm.Split('/');
                    if (strAlarm.Length == 2)
                    {
                        strAlarm = "";
                    }
                    else if (a[0] == "00")
                    {
                        strAlarm = a[1];
                        bErrorOn = false;
                    }
                    else
                    {
                        strAlarm = a[0] + a[1];
                    }
                }
                else if (e.NotificationHandle == hUserChange)                                                               // 07/20/23 PS
                {
                    strCurrentUser = ReadAdsString(hCurrentUser, 20);
                    strLogOnTime = ReadAdsString(hLogOnTime, 20);
                    bLoggedOn = strUser == strCurrentUser;
                }
                else if (e.NotificationHandle == hCycleCount)
                {
                    iCycleCount = binReader.ReadInt16();
                    iLogging = iCycleCount == 0 ? iLogging : (short)LogType.No_Log;
                }
                else if (e.NotificationHandle == hTotalCycleCount)
                {
                    iTotalCycleCount = binReader.ReadUInt32();
                    if (bTCStarted)
                    {
                        if (bFirstReadComplete)
                        {
                            iCartridgeCycles[iCartridgePointer] = iCartridgeCycles[iCartridgePointer] + 1;
                            Config.SaveCartridges();
                        }
                        else
                        {
                            bFirstReadComplete = true;
                        }
                    }
                }
                else if (e.NotificationHandle == hStatus)
                {
                    iStatus = binReader.ReadInt16();
                    strStatusLabel = ReadAdsString(hStatusLabel, 70);
                }
                else if (e.NotificationHandle == hTemp000_Error)
                {
                    bTemp000_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp001_Error)
                {
                    bTemp001_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp002_Error)
                {
                    bTemp002_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp003_Error)
                {
                    bTemp003_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp004_Error)
                {
                    bTemp004_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp005_Error)
                {
                    bTemp005_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp006_Error)
                {
                    bTemp006_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp007_Error)
                {
                    bTemp007_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp008_Error)
                {
                    bTemp008_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp009_Error)
                {
                    bTemp009_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp010_Error)
                {
                    bTemp010_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp011_Error)
                {
                    bTemp011_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp012_Error)
                {
                    bTemp012_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp013_Error)
                {
                    bTemp013_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp014_Error)
                {
                    bTemp014_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp015_Error)
                {
                    bTemp015_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp016_Error)
                {
                    bTemp016_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp017_Error)
                {
                    bTemp017_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp018_Error)
                {
                    bTemp018_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp019_Error)
                {
                    bTemp019_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp020_Error)
                {
                    bTemp020_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp021_Error)
                {
                    bTemp021_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp022_Error)
                {
                    bTemp022_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp023_Error)
                {
                    bTemp023_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp024_Error)
                {
                    bTemp024_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp025_Error)
                {
                    bTemp025_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTemp026_Error)
                {
                    bTemp026_Error = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDI000_Open)
                {
                    bDI000_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDI000_Closed)
                {
                    bDI000_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDI001_Open)
                {
                    bDI001_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDI001_Closed)
                {
                    bDI001_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO000)
                {
                    bDO000 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO001)
                {
                    bDO001 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO002)
                {
                    bDO002 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO003)
                {
                    bDO003 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO004)
                {
                    bDO004 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO004_Open)
                {
                    bDO004_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO004_Closed)
                {
                    bDO004_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO005)
                {
                    bDO005 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO005_Open)
                {
                    bDO005_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO005_Closed)
                {
                    bDO005_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO006)
                {
                    bDO006 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO006_Open)
                {
                    bDO006_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO006_Closed)
                {
                    bDO006_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO007)
                {
                    bDO007 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO007_LSH)
                {
                    bDO007_LSH = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO007_LSL)
                {
                    bDO007_LSL = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO008)
                {
                    bDO008 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO008_Open)
                {
                    bDO008_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO008_Closed)
                {
                    bDO008_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO009)
                {
                    bDO009 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO009_Open)
                {
                    bDO009_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO009_Closed)
                {
                    bDO009_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO010)
                {
                    bDO010 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO011)
                {
                    bDO011 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO011_LSH)
                {
                    bDO011_LSH = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO011_LSL)
                {
                    bDO011_LSL = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO012)
                {
                    bDO012 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO013)
                {
                    bDO013 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO013_Open)
                {
                    bDO013_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO013_Closed)
                {
                    bDO013_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO014)
                {
                    bDO014 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO014_Open)
                {
                    bDO014_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO014_Closed)
                {
                    bDO014_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO015)
                {
                    bDO015 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO016)
                {
                    bDO016 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO016_Open)
                {
                    bDO016_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO016_Closed)
                {
                    bDO016_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO017)
                {
                    bDO017 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO017_Open)
                {
                    bDO017_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO017_Closed)
                {
                    bDO017_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO018)
                {
                    bDO018 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO018_LSH)
                {
                    bDO018_LSH = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO018_LSL)
                {
                    bDO018_LSL = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO019)
                {
                    bDO019 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO020)
                {
                    bDO020 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO021)
                {
                    bDO021 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO021_Open)
                {
                    bDO021_Open = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO021_Closed)
                {
                    bDO021_Closed = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hDO022)                                                    // 09/20/23 PS
                {
                    bDO022 = binReader.ReadBoolean();                                                       // 09/20/23 PS
                }
                else if (e.NotificationHandle == hDO022_Open)                                               // 09/20/23 PS
                {
                    bDO022_Open = binReader.ReadBoolean();                                                  // 09/20/23 PS
                }
                else if (e.NotificationHandle == hDO022_Closed)                                             // 09/20/23 PS
                {
                    bDO022_Closed = binReader.ReadBoolean();                                                // 09/20/23 PS
                }
                else if (e.NotificationHandle == hAOut000)
                {
                    sAOut000_Out = binReader.ReadSingle();
                    bAOutChange = true;
                }
                else if (e.NotificationHandle == hAOut001_On)
                {
                    bAOut001_On = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hAOut002_On)
                {
                    bAOut002_On = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hAOut003_On)
                {
                    bAOut003_On = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hAOut004_On)
                {
                    bAOut004_On = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hAOut005_On)
                {
                    bAOut005_On = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hAOut006_On)
                {
                    bAOut006_On = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hAOut007)
                {
                    sAOut007_Out = binReader.ReadSingle();
                    bAOutChange = true;
                }
                else if (e.NotificationHandle == hAOut008)
                {
                    sAOut008_Out = binReader.ReadSingle();
                    bAOutChange = true;
                }
                else if (e.NotificationHandle == hAOut009)
                {
                    sAOut009_Out = binReader.ReadSingle();
                    bAOutChange = true;
                }
                else if (e.NotificationHandle == hAOut010)
                {
                    sAOut010_Out = binReader.ReadSingle();
                    bAOutChange = true;
                }
                else if (e.NotificationHandle == hHS_1102_Enabled)
                {
                    bHS_1102_Enabled = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hHS_1102_Running)
                {
                    bHS_1102_Running = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hHmiActive)
                {
                    bHmiActive = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hManualHeat)
                {
                    bManualHeat = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hTestRunning)
                {
                    bTestRunning = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hWarmupComplete)
                {
                    bWarmupComplete = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hAdsorpTimeCO2)
                {
                    bAdsorpTimeCO2 = binReader.ReadBoolean();
                }
                else if (e.NotificationHandle == hLocked)                                                                   // 07/22/23 PS
                {
                    bLocked = binReader.ReadBoolean();
                }
            }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                {
                    App.WPFMessageBoxOK(ex.Message + " TcAdsClientOnNotify " + e.NotificationHandle.ToString(), App.bgRed);
                }
            }
        }

        public static void TcAdsClose()
        {
            try
            {
                DeleteNotifications();
                tcClient.DeleteVariableHandle(hAIn000);
                tcClient.DeleteVariableHandle(hAIn001);
                tcClient.DeleteVariableHandle(hAIn002);
                tcClient.DeleteVariableHandle(hAIn003);
                tcClient.DeleteVariableHandle(hAIn004);
                tcClient.DeleteVariableHandle(hAIn005);
                tcClient.DeleteVariableHandle(hAIn006);
                tcClient.DeleteVariableHandle(hAIn007);
                tcClient.DeleteVariableHandle(hAIn008);
                tcClient.DeleteVariableHandle(hAIn009);
                tcClient.DeleteVariableHandle(hAIn010);
                tcClient.DeleteVariableHandle(hAIn011);
                tcClient.DeleteVariableHandle(hAIn012);
                tcClient.DeleteVariableHandle(hAIn013);
                tcClient.DeleteVariableHandle(hAIn014);
                tcClient.DeleteVariableHandle(hAIn015);
                tcClient.DeleteVariableHandle(hAIn016);
                tcClient.DeleteVariableHandle(hAIn017);
                tcClient.DeleteVariableHandle(hAIn018);
                tcClient.DeleteVariableHandle(hAIn019);
                tcClient.DeleteVariableHandle(hAIn020);
                tcClient.DeleteVariableHandle(hAIn021);
                tcClient.DeleteVariableHandle(hAIn022);
                tcClient.DeleteVariableHandle(hAIn023);
                tcClient.DeleteVariableHandle(hAIn024);
                tcClient.DeleteVariableHandle(hAIn040);
                tcClient.DeleteVariableHandle(hAIn041);
                tcClient.DeleteVariableHandle(hAIn042);
                tcClient.DeleteVariableHandle(hAIn043);
                tcClient.DeleteVariableHandle(hAIn044);
                tcClient.DeleteVariableHandle(hAIn045);
                tcClient.DeleteVariableHandle(hAIn046);
                tcClient.DeleteVariableHandle(hAIn050);
                tcClient.DeleteVariableHandle(hAIn051);
                tcClient.DeleteVariableHandle(hAIn052);
                tcClient.DeleteVariableHandle(hAIn053);
                tcClient.DeleteVariableHandle(hAIn054);
                tcClient.DeleteVariableHandle(hAIn055);
                tcClient.DeleteVariableHandle(hAIn025);
                tcClient.DeleteVariableHandle(hAIn026);
                tcClient.DeleteVariableHandle(hAIn027);
                tcClient.DeleteVariableHandle(hAIn028);

                tcClient.DeleteVariableHandle(hAOut000RPM);

                tcClient.DeleteVariableHandle(hIT_1);
                tcClient.DeleteVariableHandle(hIT_2);
                tcClient.DeleteVariableHandle(hIT_3);
                tcClient.DeleteVariableHandle(hVT_1);
                tcClient.DeleteVariableHandle(hVT_2);
                tcClient.DeleteVariableHandle(hVT_3);

                tcClient.DeleteVariableHandle(hAlarm);
                tcClient.DeleteVariableHandle(hCurrentUser);                                                                // 07/21/23 PS
                tcClient.DeleteVariableHandle(hLogOnTime);                                                                  // 07/22/23 PS
                tcClient.DeleteVariableHandle(hStatusLabel);
                tcClient.DeleteVariableHandle(hRemainingTime);
                tcClient.DeleteVariableHandle(hCurrentStep);

                tcClient.DeleteVariableHandle(hTemp000);
                tcClient.DeleteVariableHandle(hTemp001);
                tcClient.DeleteVariableHandle(hTemp002);
                tcClient.DeleteVariableHandle(hTemp003);
                tcClient.DeleteVariableHandle(hTemp004);
                tcClient.DeleteVariableHandle(hTemp005);
                tcClient.DeleteVariableHandle(hTemp006);
                tcClient.DeleteVariableHandle(hTemp007);
                tcClient.DeleteVariableHandle(hTemp008);
                tcClient.DeleteVariableHandle(hTemp009);
                tcClient.DeleteVariableHandle(hTemp010);
                tcClient.DeleteVariableHandle(hTemp011);
                tcClient.DeleteVariableHandle(hTemp012);
                tcClient.DeleteVariableHandle(hTemp013);
                tcClient.DeleteVariableHandle(hTemp014);
                tcClient.DeleteVariableHandle(hTemp015);
                tcClient.DeleteVariableHandle(hTemp016);
                tcClient.DeleteVariableHandle(hTemp017);
                tcClient.DeleteVariableHandle(hTemp018);
                tcClient.DeleteVariableHandle(hTemp019);
                tcClient.DeleteVariableHandle(hTemp020);
                tcClient.DeleteVariableHandle(hTemp021);
                tcClient.DeleteVariableHandle(hTemp022);
                tcClient.DeleteVariableHandle(hTemp023);
                tcClient.DeleteVariableHandle(hTemp024);
                tcClient.DeleteVariableHandle(hTemp025);
                tcClient.DeleteVariableHandle(hTemp026);
                tcClient.DeleteVariableHandle(hAOut001_Output);
                tcClient.DeleteVariableHandle(hAOut002_Output);
                tcClient.DeleteVariableHandle(hAOut003_Output);
                tcClient.DeleteVariableHandle(hAOut004_Output);
                tcClient.DeleteVariableHandle(hAOut005_Output);
                tcClient.DeleteVariableHandle(hAOut006_Output);
            }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                {
                    App.WPFMessageBoxOK(ex.Message + ", TcAdsClose", App.bgRed);
                }
            }

            try
            {
                tcClient.Dispose();
            }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                {
                    App.WPFMessageBoxOK(ex.Message + ", TcAdsClientDispose", App.bgRed);
                }
            }
            bTCStarted = false;
        }

        public static void DeleteNotifications()
        {
            tcClient.DeleteDeviceNotification(hHalfSecPulse);
            tcClient.DeleteDeviceNotification(hTemp000_Error);
            tcClient.DeleteDeviceNotification(hTemp001_Error);
            tcClient.DeleteDeviceNotification(hTemp002_Error);
            tcClient.DeleteDeviceNotification(hTemp003_Error);
            tcClient.DeleteDeviceNotification(hTemp004_Error);
            tcClient.DeleteDeviceNotification(hTemp005_Error);
            tcClient.DeleteDeviceNotification(hTemp006_Error);
            tcClient.DeleteDeviceNotification(hTemp007_Error);
            tcClient.DeleteDeviceNotification(hTemp008_Error);
            tcClient.DeleteDeviceNotification(hTemp009_Error);
            tcClient.DeleteDeviceNotification(hTemp010_Error);
            tcClient.DeleteDeviceNotification(hTemp011_Error);
            tcClient.DeleteDeviceNotification(hTemp012_Error);
            tcClient.DeleteDeviceNotification(hTemp013_Error);
            tcClient.DeleteDeviceNotification(hTemp014_Error);
            tcClient.DeleteDeviceNotification(hTemp015_Error);
            tcClient.DeleteDeviceNotification(hTemp016_Error);
            tcClient.DeleteDeviceNotification(hTemp017_Error);
            tcClient.DeleteDeviceNotification(hTemp018_Error);
            tcClient.DeleteDeviceNotification(hTemp019_Error);
            tcClient.DeleteDeviceNotification(hTemp020_Error);
            tcClient.DeleteDeviceNotification(hTemp021_Error);
            tcClient.DeleteDeviceNotification(hTemp022_Error);
            tcClient.DeleteDeviceNotification(hTemp023_Error);
            tcClient.DeleteDeviceNotification(hTemp024_Error);
            tcClient.DeleteDeviceNotification(hTemp025_Error);
            tcClient.DeleteDeviceNotification(hTemp026_Error);
            tcClient.DeleteDeviceNotification(hDI000_Open);
            tcClient.DeleteDeviceNotification(hDI000_Closed);
            tcClient.DeleteDeviceNotification(hDI001_Open);
            tcClient.DeleteDeviceNotification(hDI001_Closed);
            tcClient.DeleteDeviceNotification(hDO000);
            tcClient.DeleteDeviceNotification(hDO001);
            tcClient.DeleteDeviceNotification(hDO002);
            tcClient.DeleteDeviceNotification(hDO003);
            tcClient.DeleteDeviceNotification(hDO004);
            tcClient.DeleteDeviceNotification(hDO004_Open);
            tcClient.DeleteDeviceNotification(hDO004_Closed);
            tcClient.DeleteDeviceNotification(hDO005);
            tcClient.DeleteDeviceNotification(hDO005_Open);
            tcClient.DeleteDeviceNotification(hDO005_Closed);
            tcClient.DeleteDeviceNotification(hDO006);
            tcClient.DeleteDeviceNotification(hDO006_Open);
            tcClient.DeleteDeviceNotification(hDO006_Closed);
            tcClient.DeleteDeviceNotification(hDO007);
            tcClient.DeleteDeviceNotification(hDO007_LSH);
            tcClient.DeleteDeviceNotification(hDO007_LSL);
            tcClient.DeleteDeviceNotification(hDO008);
            tcClient.DeleteDeviceNotification(hDO008_Open);
            tcClient.DeleteDeviceNotification(hDO008_Closed);
            tcClient.DeleteDeviceNotification(hDO009);
            tcClient.DeleteDeviceNotification(hDO009_Open);
            tcClient.DeleteDeviceNotification(hDO009_Closed);
            tcClient.DeleteDeviceNotification(hDO010);
            tcClient.DeleteDeviceNotification(hDO011);
            tcClient.DeleteDeviceNotification(hDO011_LSH);
            tcClient.DeleteDeviceNotification(hDO011_LSL);
            tcClient.DeleteDeviceNotification(hDO012);
            tcClient.DeleteDeviceNotification(hDO013);
            tcClient.DeleteDeviceNotification(hDO013_Open);
            tcClient.DeleteDeviceNotification(hDO013_Closed);
            tcClient.DeleteDeviceNotification(hDO014);
            tcClient.DeleteDeviceNotification(hDO014_Open);
            tcClient.DeleteDeviceNotification(hDO014_Closed);
            tcClient.DeleteDeviceNotification(hDO015);
            tcClient.DeleteDeviceNotification(hDO016);
            tcClient.DeleteDeviceNotification(hDO016_Open);
            tcClient.DeleteDeviceNotification(hDO016_Closed);
            tcClient.DeleteDeviceNotification(hDO017);
            tcClient.DeleteDeviceNotification(hDO017_Open);
            tcClient.DeleteDeviceNotification(hDO017_Closed);
            tcClient.DeleteDeviceNotification(hDO018);
            tcClient.DeleteDeviceNotification(hDO018_LSH);
            tcClient.DeleteDeviceNotification(hDO018_LSL);
            tcClient.DeleteDeviceNotification(hDO019);
            tcClient.DeleteDeviceNotification(hDO020);
            tcClient.DeleteDeviceNotification(hDO021);
            tcClient.DeleteDeviceNotification(hDO021_Open);
            tcClient.DeleteDeviceNotification(hDO021_Closed);
            tcClient.DeleteDeviceNotification(hDO022);                                          // 09/20/23 PS
            tcClient.DeleteDeviceNotification(hDO022_Open);                                     // 09/20/23 PS
            tcClient.DeleteDeviceNotification(hDO022_Closed);                                   // 09/20/23 PS
            tcClient.DeleteDeviceNotification(hAOut000);
            tcClient.DeleteDeviceNotification(hAOut001_On);
            tcClient.DeleteDeviceNotification(hAOut002_On);
            tcClient.DeleteDeviceNotification(hAOut003_On);
            tcClient.DeleteDeviceNotification(hAOut004_On);
            tcClient.DeleteDeviceNotification(hAOut005_On);
            tcClient.DeleteDeviceNotification(hAOut006_On);
            tcClient.DeleteDeviceNotification(hAOut007);
            tcClient.DeleteDeviceNotification(hAOut008);
            tcClient.DeleteDeviceNotification(hAOut009);
            tcClient.DeleteDeviceNotification(hAOut010);
            tcClient.DeleteDeviceNotification(hHS_1102_Enabled);
            tcClient.DeleteDeviceNotification(hHS_1102_Running);
            tcClient.DeleteDeviceNotification(hHmiActive);
            tcClient.DeleteDeviceNotification(hManualHeat);
            tcClient.DeleteDeviceNotification(hTestRunning);
            tcClient.DeleteDeviceNotification(hWarmupComplete);
            tcClient.DeleteDeviceNotification(hStatus);
            tcClient.DeleteDeviceNotification(hState);
            tcClient.DeleteDeviceNotification(hCycleCount);
            tcClient.DeleteDeviceNotification(hTotalCycleCount);
            tcClient.DeleteDeviceNotification(hAlarmChange);
            tcClient.DeleteDeviceNotification(hUserChange);                                                                 // 07/21/23 PS
            tcClient.DeleteDeviceNotification(hAdsorpTimeCO2);
            tcClient.DeleteDeviceNotification(hLocked);                                                                     // 07/22/23 PS
        }
        #endregion

        #region Set TwinCAT Values

        public static void SetTCValues()
        {
            if (!AdsCommDown())
            {
                SetTCPointValues();
                SetTCHTValues();
                SetTCSPValues();
                SetAdsValue(GVTtgi + "Cycles_To_Run", iCyclesToRun);
                SetAdsValue(GVTtgb + "ADS_Active", true);
            }
        }

        public static void SetTCPointValues()
        {
            SetParameters(ref AIn000);
            SetParameters(ref AIn001);
            SetParameters(ref AIn002);
            SetParameters(ref AIn003);
            SetParameters(ref AIn004);
            SetParameters(ref AIn005);
            SetParameters(ref AIn006);
            SetParameters(ref AIn007);
            SetParameters(ref AIn008);
            SetParameters(ref AIn009);
            SetParameters(ref AIn010);
            SetParameters(ref AIn011);
            SetParameters(ref AIn012);
            SetParameters(ref AIn013);
            SetParameters(ref AIn014);
            SetParameters(ref AIn015);
            SetParameters(ref AIn016);
            SetParameters(ref AIn017);
            SetParameters(ref AIn018);
            SetParameters(ref AIn019);
            SetParameters(ref AIn020);
            SetParameters(ref AIn021);
            SetParameters(ref AIn022);
            SetParameters(ref AIn023);
            SetParameters(ref AIn024);
            SetParameters(ref AIn025);
            SetParameters(ref AIn026);
            SetParameters(ref AIn027);
            SetParameters(ref AIn028);
            SetParameters(ref AIn040);
            SetParameters(ref AIn041);
            SetParameters(ref AIn042);
            SetParameters(ref AIn043);
            SetParameters(ref AIn044);
            SetParameters(ref AIn045);
            SetParameters(ref AIn046);
        }

        private static void SetParameters(ref string[] point)
        {
            if (point[9] == "x")                                                                                           // Only set values that have changed - "x"
            {
                SetAdsValue(GP + point[0] + "_LOW", point[4]);
                SetAdsValue(GP + point[0] + "_HIGH", point[5]);
                SetAdsValue(GP + point[0] + "_MIN", point[6]);
                SetAdsValue(GP + point[0] + "_OFFSET", point[7]);
                point[8] = "";
            }
        }

        public static void SetTCHTValues()
        {
            SetAdsValue(GVTtgr + "HT_1_SP", ADS.sAOut001_SP);
            SetAdsValue(GVTtgr + "HT_1_Manual_Output", 0.0f);
            SetAdsValue(GVTtgr + "HT_2_SP", ADS.sAOut002_SP);
            SetAdsValue(GVTtgr + "HT_2_Manual_Output", 0.0f);
            SetAdsValue(GVTtgr + "HT_3_SP", ADS.sAOut003_SP);
            SetAdsValue(GVTtgr + "HT_3_Manual_Output", 0.0f);
            SetAdsValue(GVTtgr + "HT_4_SP", ADS.sAOut004_SP);
            SetAdsValue(GVTtgr + "HT_4_Manual_Output", 0.0f);
            SetAdsValue(GVTtgr + "HT_5_SP", ADS.sAOut005_SP);
            SetAdsValue(GVTtgr + "HT_5_Manual_Output", 0.0f);
            SetAdsValue(GVTtgr + "HT_6_SP", ADS.sAOut006_SP);
            SetAdsValue(GVTtgr + "HT_6_Manual_Output", 0.0f);
            SetAdsValue(GVTtgr + "HT_P_Value", sPFactor);
            SetAdsValue(GVTtgr + "HT_I_Value", sIFactor);
            SetAdsValue(GVTtgr + "HT_I_Range", sIRange);
        }

        public static void SetTCSPValues()
        {
            SetAdsValue(GVTtgr + "SP_Adsorption_Time", ADS.sSPAdsorptionTime);
            SetAdsValue(GVTtgr + "SP_Adsorption_CO2_Differential", ADS.sSPAdsorptionCO2);
            SetAdsValue(GVTtgr + "SP_Adsorption_CO2_Time", ADS.sSPAdsorptionCO2Time);
            SetAdsValue(GVTtgr + "SP_Boiler_Pressure", sSPBoilerPressure);
            SetAdsValue(GVTtgr + "SP_Min_LRP_Cooling_Loop_Flow", sSPMinLRPCoolingLoopFlow);
            SetAdsValue(GVTtgr + "SP_Evacuation_Target_Pressure", sSPEvacuationTargetPressure);
            SetAdsValue(GVTtgr + "SP_Max_Allowed_Pressure_Leakage", sSPMaxAllowedPressureLeakage);
            SetAdsValue(GVTtgr + "SP_Reactor_Repressurization_Pressure", sSPReactorRepressurizationPressure);
            SetAdsValue(GVTtgr + "SP_Adsorption_Temp", ADS.sSPAdsorptionTemp);
            SetAdsValue(GVTtgr + "SP_Adsorption_VFD_Output", sSPAdsorptionVFDOutput);
            SetAdsValue(GVTtgr + "SP_Min_Bypass_Steam_Flow", sSPMinBypassSteamFlow);
            SetAdsValue(GVTtgr + "SP_Min_Bypass_Steam_Temp", sSPMinBypassSteamTemp);
            SetAdsValue(GVTtgr + "SP_IP_1302B_Reactor_Low", sSPReactorPressureLow);
            SetAdsValue(GVTtgr + "SP_IP_1302B_Reactor_High", sSPReactorPressureHigh);
            SetAdsValue(GVTtgr + "SP_IP_1302B_Valve_Low", sSPReactorValveLowPos);
            SetAdsValue(GVTtgr + "SP_IP_1302B_Valve_High", sSPReactorValveHighPos);
            SetAdsValue(GVTtgr + "SP_Steam_Valve_Percent_Open", sSPSteamValvePercentOpen);
            SetAdsValue(GVTtgr + "SP_Max_Allowed_Sorbent_Temp", sSPMaxAllowedSorbentTemp);
            SetAdsValue(GVTtgr + "SP_Steam_Repressurization_Pressure", sSPSteamRepressurizationPressure);
            SetAdsValue(GVTtgr + "SP_Steam_Purge_Time", sSPSteamPurgeTime);
            SetAdsValue(GVTtgr + "SP_Steam_Purge_Cutoff_Temp", sSPSteamPurgeCutoffTemp);
            SetAdsValue(GVTtgr + "SP_Evap_Cooling_Cutoff_Temp", sSPEvapCoolingCutoffTemp);
            SetAdsValue(GVTtgr + "SP_Evap_Cooling_Target_Pressure", sSPEvapCoolingTargetPressure);
            SetAdsValue(GVTtgr + "SP_Leak_Check_Time", sSPLeakCheckTime);
            SetAdsValue(GVTtgr + "SP_Evacuation_Target_Pressure_Steam", sSPEvacuationTargetPressureSteam);                  // 07/14/23 PS - New set point
        }

        #endregion Set TC Values

        #region Read values

        public static void ReadAdsValues()
        {
            if (!bAdsDown)
            {
                try
                {
                    sAIn000 = ReadAdsSingle(hAIn000, "AIn000");
                    sAIn001 = ReadAdsSingle(hAIn001, "AIn001");
                    sAIn002 = ReadAdsSingle(hAIn002, "AIn002");
                    sAIn003 = ReadAdsSingle(hAIn003, "AIn003");
                    sAIn004 = ReadAdsSingle(hAIn004, "AIn004");
                    sAIn005 = ReadAdsSingle(hAIn005, "AIn005");
                    sAIn006 = ReadAdsSingle(hAIn006, "AIn006");
                    sAIn007 = ReadAdsSingle(hAIn007, "AIn007");
                    sAIn008 = ReadAdsSingle(hAIn008, "AIn008");
                    sAIn009 = ReadAdsSingle(hAIn009, "AIn009");
                    sAIn010 = ReadAdsSingle(hAIn010, "AIn010");
                    sAIn011 = ReadAdsSingle(hAIn011, "AIn011");
                    sAIn012 = ReadAdsSingle(hAIn012, "AIn012");
                    sAIn013 = ReadAdsSingle(hAIn013, "AIn013");
                    sAIn014 = ReadAdsSingle(hAIn014, "AIn014");
                    sAIn015 = ReadAdsSingle(hAIn015, "AIn015");
                    sAIn016 = ReadAdsSingle(hAIn016, "AIn016");
                    sAIn017 = ReadAdsSingle(hAIn017, "AIn017");
                    sAIn018 = ReadAdsSingle(hAIn018, "AIn018");
                    sAIn019 = ReadAdsSingle(hAIn019, "AIn019");
                    sAIn020 = ReadAdsSingle(hAIn020, "AIn020");
                    sAIn021 = ReadAdsSingle(hAIn021, "AIn021");
                    sAIn022 = ReadAdsSingle(hAIn022, "AIn022");
                    sAIn023 = ReadAdsSingle(hAIn023, "AIn023");
                    sAIn024 = ReadAdsSingle(hAIn024, "AIn024");
                    sAIn025 = ReadAdsSingle(hAIn025, "AIn025");
                    sAIn026 = ReadAdsSingle(hAIn026, "AIn026");
                    sAIn027 = ReadAdsSingle(hAIn027, "AIn027");
                    sAIn028 = ReadAdsSingle(hAIn028, "AIn028");

                    sAIn040 = ReadAdsSingle(hAIn040, "AIn040");
                    sAIn041 = ReadAdsSingle(hAIn041, "AIn041");
                    sAIn042 = ReadAdsSingle(hAIn042, "AIn042");
                    sAIn043 = ReadAdsSingle(hAIn043, "AIn043");
                    sAIn044 = ReadAdsSingle(hAIn044, "AIn044");
                    sAIn045 = ReadAdsSingle(hAIn045, "AIn045");
                    sAIn046 = ReadAdsSingle(hAIn046, "AIn046");
                    sAIn050 = ReadAdsSingle(hAIn050, "AIn050");
                    sAIn051 = ReadAdsSingle(hAIn051, "AIn051");
                    sAIn052 = ReadAdsSingle(hAIn052, "AIn052");
                    sAIn053 = ReadAdsSingle(hAIn053, "AIn053");
                    sAIn054 = ReadAdsSingle(hAIn054, "AIn054");
                    sAIn055 = ReadAdsSingle(hAIn055, "AIn055");

                    sAOut000RPM = ReadAdsSingle(hAOut000RPM, "AOut000RPM");

                    sIT_1 = ReadAdsSingle(hIT_1, "IT1");
                    sIT_2 = ReadAdsSingle(hIT_2, "IT2");
                    sIT_3 = ReadAdsSingle(hIT_3, "IT3");
                    sVT_1 = ReadAdsSingle(hVT_1, "VT1");
                    sVT_2 = ReadAdsSingle(hVT_2, "VT2");
                    sVT_3 = ReadAdsSingle(hVT_3, "VT3");

                    sTemp000 = ReadAdsSingle(hTemp000, "Temp000");
                    sTemp001 = ReadAdsSingle(hTemp001, "Temp001");
                    sTemp002 = ReadAdsSingle(hTemp002, "Temp002");
                    sTemp003 = ReadAdsSingle(hTemp003, "Temp003");
                    sTemp004 = ReadAdsSingle(hTemp004, "Temp004");
                    sTemp005 = ReadAdsSingle(hTemp005, "Temp005");
                    sTemp006 = ReadAdsSingle(hTemp006, "Temp006");
                    sTemp007 = ReadAdsSingle(hTemp007, "Temp007");
                    sTemp008 = ReadAdsSingle(hTemp008, "Temp008");
                    sTemp009 = ReadAdsSingle(hTemp009, "Temp009");
                    sTemp010 = ReadAdsSingle(hTemp010, "Temp010");
                    sTemp011 = ReadAdsSingle(hTemp011, "Temp011");
                    sTemp012 = ReadAdsSingle(hTemp012, "Temp012");
                    sTemp013 = ReadAdsSingle(hTemp013, "Temp013");
                    sTemp014 = ReadAdsSingle(hTemp014, "Temp014");
                    sTemp015 = ReadAdsSingle(hTemp015, "Temp015");
                    sTemp016 = ReadAdsSingle(hTemp016, "Temp016");
                    sTemp017 = ReadAdsSingle(hTemp017, "Temp017");
                    sTemp018 = ReadAdsSingle(hTemp018, "Temp018");
                    sTemp019 = ReadAdsSingle(hTemp019, "Temp019");
                    sTemp020 = ReadAdsSingle(hTemp020, "Temp020");
                    sTemp021 = ReadAdsSingle(hTemp021, "Temp021");
                    sTemp022 = ReadAdsSingle(hTemp022, "Temp022");
                    sTemp023 = ReadAdsSingle(hTemp023, "Temp023");
                    sTemp024 = ReadAdsSingle(hTemp024, "Temp024");
                    sTemp025 = ReadAdsSingle(hTemp025, "Temp025");
                    sTemp026 = ReadAdsSingle(hTemp026, "Temp026");

                    sRemainingTime = ReadAdsSingle(hRemainingTime, "Time");
                    iCurrentStep = ReadHandleInt(hCurrentStep);
                }
                catch (Exception ex)
                {
                    if (!AdsCommDown())
                    {
                        App.WPFMessageBoxOK(ex.Message + ", ReadAdsValues", App.bgRed);
                    }
                }
            }
        }

        #endregion

        #region Point Handles & Aliases

        public static void LoadPointCollection()
        {
            _PointCollection.Clear();
            _PointCollection.Add(new PointInfo { Point = strInactive, Handle = 0, Alias = strInactive, Units = "0", Decimals = "0" });
            LoadAIPointArrays();
            for (int i = 0; i < ciAICount; i++)                                             // Create collection in alphabetical order using sorted array
            {
                if (strAINames[i] == AIn000[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn000", Point = AIn000[0], Alias = AIn000[1], Handle = hAIn000, Units = AIn000[2], Decimals = AIn000[3], Low = Convert.ToSingle(AIn000[4]), High = Convert.ToSingle(AIn000[5]), Min = Convert.ToSingle(AIn000[6]), Offset = Convert.ToSingle(AIn000[7]) }); }
                else if (strAINames[i] == AIn001[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn001", Point = AIn001[0], Alias = AIn001[1], Handle = hAIn001, Units = AIn001[2], Decimals = AIn001[3], Low = Convert.ToSingle(AIn001[4]), High = Convert.ToSingle(AIn001[5]), Min = Convert.ToSingle(AIn001[6]), Offset = Convert.ToSingle(AIn001[7]) }); }
                else if (strAINames[i] == AIn002[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn002", Point = AIn002[0], Alias = AIn002[1], Handle = hAIn002, Units = AIn002[2], Decimals = AIn002[3], Low = Convert.ToSingle(AIn002[4]), High = Convert.ToSingle(AIn002[5]), Min = Convert.ToSingle(AIn002[6]), Offset = Convert.ToSingle(AIn002[7]) }); }
                else if (strAINames[i] == AIn003[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn003", Point = AIn003[0], Alias = AIn003[1], Handle = hAIn003, Units = AIn003[2], Decimals = AIn003[3], Low = Convert.ToSingle(AIn003[4]), High = Convert.ToSingle(AIn003[5]), Min = Convert.ToSingle(AIn003[6]), Offset = Convert.ToSingle(AIn003[7]) }); }
                else if (strAINames[i] == AIn004[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn004", Point = AIn004[0], Alias = AIn004[1], Handle = hAIn004, Units = AIn004[2], Decimals = AIn004[3], Low = Convert.ToSingle(AIn004[4]), High = Convert.ToSingle(AIn004[5]), Min = Convert.ToSingle(AIn004[6]), Offset = Convert.ToSingle(AIn004[7]) }); }
                else if (strAINames[i] == AIn005[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn005", Point = AIn005[0], Alias = AIn005[1], Handle = hAIn005, Units = AIn005[2], Decimals = AIn005[3], Low = Convert.ToSingle(AIn005[4]), High = Convert.ToSingle(AIn005[5]), Min = Convert.ToSingle(AIn005[6]), Offset = Convert.ToSingle(AIn005[7]) }); }
                else if (strAINames[i] == AIn006[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn006", Point = AIn006[0], Alias = AIn006[1], Handle = hAIn006, Units = AIn006[2], Decimals = AIn006[3], Low = Convert.ToSingle(AIn006[4]), High = Convert.ToSingle(AIn006[5]), Min = Convert.ToSingle(AIn006[6]), Offset = Convert.ToSingle(AIn006[7]) }); }
                else if (strAINames[i] == AIn007[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn007", Point = AIn007[0], Alias = AIn007[1], Handle = hAIn007, Units = AIn007[2], Decimals = AIn007[3], Low = Convert.ToSingle(AIn007[4]), High = Convert.ToSingle(AIn007[5]), Min = Convert.ToSingle(AIn007[6]), Offset = Convert.ToSingle(AIn007[7]) }); }
                else if (strAINames[i] == AIn008[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn008", Point = AIn008[0], Alias = AIn008[1], Handle = hAIn008, Units = AIn008[2], Decimals = AIn008[3], Low = Convert.ToSingle(AIn008[4]), High = Convert.ToSingle(AIn008[5]), Min = Convert.ToSingle(AIn008[6]), Offset = Convert.ToSingle(AIn008[7]) }); }
                else if (strAINames[i] == AIn009[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn009", Point = AIn009[0], Alias = AIn009[1], Handle = hAIn009, Units = AIn009[2], Decimals = AIn009[3], Low = Convert.ToSingle(AIn009[4]), High = Convert.ToSingle(AIn009[5]), Min = Convert.ToSingle(AIn009[6]), Offset = Convert.ToSingle(AIn009[7]) }); }
                else if (strAINames[i] == AIn010[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn010", Point = AIn010[0], Alias = AIn010[1], Handle = hAIn010, Units = AIn010[2], Decimals = AIn010[3], Low = Convert.ToSingle(AIn010[4]), High = Convert.ToSingle(AIn010[5]), Min = Convert.ToSingle(AIn010[6]), Offset = Convert.ToSingle(AIn010[7]) }); }
                else if (strAINames[i] == AIn011[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn011", Point = AIn011[0], Alias = AIn011[1], Handle = hAIn011, Units = AIn011[2], Decimals = AIn011[3], Low = Convert.ToSingle(AIn011[4]), High = Convert.ToSingle(AIn011[5]), Min = Convert.ToSingle(AIn011[6]), Offset = Convert.ToSingle(AIn011[7]) }); }
                else if (strAINames[i] == AIn012[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn012", Point = AIn012[0], Alias = AIn012[1], Handle = hAIn012, Units = AIn012[2], Decimals = AIn012[3], Low = Convert.ToSingle(AIn012[4]), High = Convert.ToSingle(AIn012[5]), Min = Convert.ToSingle(AIn012[6]), Offset = Convert.ToSingle(AIn012[7]) }); }
                else if (strAINames[i] == AIn013[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn013", Point = AIn013[0], Alias = AIn013[1], Handle = hAIn013, Units = AIn013[2], Decimals = AIn013[3], Low = Convert.ToSingle(AIn013[4]), High = Convert.ToSingle(AIn013[5]), Min = Convert.ToSingle(AIn013[6]), Offset = Convert.ToSingle(AIn013[7]) }); }
                else if (strAINames[i] == AIn014[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn014", Point = AIn014[0], Alias = AIn014[1], Handle = hAIn014, Units = AIn014[2], Decimals = AIn014[3], Low = Convert.ToSingle(AIn014[4]), High = Convert.ToSingle(AIn014[5]), Min = Convert.ToSingle(AIn014[6]), Offset = Convert.ToSingle(AIn014[7]) }); }
                else if (strAINames[i] == AIn015[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn015", Point = AIn015[0], Alias = AIn015[1], Handle = hAIn015, Units = AIn015[2], Decimals = AIn015[3], Low = Convert.ToSingle(AIn015[4]), High = Convert.ToSingle(AIn015[5]), Min = Convert.ToSingle(AIn015[6]), Offset = Convert.ToSingle(AIn015[7]) }); }
                else if (strAINames[i] == AIn016[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn016", Point = AIn016[0], Alias = AIn016[1], Handle = hAIn016, Units = AIn016[2], Decimals = AIn016[3], Low = Convert.ToSingle(AIn016[4]), High = Convert.ToSingle(AIn016[5]), Min = Convert.ToSingle(AIn016[6]), Offset = Convert.ToSingle(AIn016[7]) }); }
                else if (strAINames[i] == AIn017[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn017", Point = AIn017[0], Alias = AIn017[1], Handle = hAIn017, Units = AIn017[2], Decimals = AIn017[3], Low = Convert.ToSingle(AIn017[4]), High = Convert.ToSingle(AIn017[5]), Min = Convert.ToSingle(AIn017[6]), Offset = Convert.ToSingle(AIn017[7]) }); }
                else if (strAINames[i] == AIn018[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn018", Point = AIn018[0], Alias = AIn018[1], Handle = hAIn018, Units = AIn018[2], Decimals = AIn018[3], Low = Convert.ToSingle(AIn018[4]), High = Convert.ToSingle(AIn018[5]), Min = Convert.ToSingle(AIn018[6]), Offset = Convert.ToSingle(AIn018[7]) }); }
                else if (strAINames[i] == AIn019[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn019", Point = AIn019[0], Alias = AIn019[1], Handle = hAIn019, Units = AIn019[2], Decimals = AIn019[3], Low = Convert.ToSingle(AIn019[4]), High = Convert.ToSingle(AIn019[5]), Min = Convert.ToSingle(AIn019[6]), Offset = Convert.ToSingle(AIn019[7]) }); }
                else if (strAINames[i] == AIn020[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn020", Point = AIn020[0], Alias = AIn020[1], Handle = hAIn020, Units = AIn020[2], Decimals = AIn020[3], Low = Convert.ToSingle(AIn020[4]), High = Convert.ToSingle(AIn020[5]), Min = Convert.ToSingle(AIn020[6]), Offset = Convert.ToSingle(AIn020[7]) }); }
                else if (strAINames[i] == AIn021[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn021", Point = AIn021[0], Alias = AIn021[1], Handle = hAIn021, Units = AIn021[2], Decimals = AIn021[3], Low = Convert.ToSingle(AIn021[4]), High = Convert.ToSingle(AIn021[5]), Min = Convert.ToSingle(AIn021[6]), Offset = Convert.ToSingle(AIn021[7]) }); }
                else if (strAINames[i] == AIn022[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn022", Point = AIn022[0], Alias = AIn022[1], Handle = hAIn022, Units = AIn022[2], Decimals = AIn022[3], Low = Convert.ToSingle(AIn022[4]), High = Convert.ToSingle(AIn022[5]), Min = Convert.ToSingle(AIn022[6]), Offset = Convert.ToSingle(AIn022[7]) }); }
                else if (strAINames[i] == AIn023[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn023", Point = AIn023[0], Alias = AIn023[1], Handle = hAIn023, Units = AIn023[2], Decimals = AIn023[3], Low = Convert.ToSingle(AIn023[4]), High = Convert.ToSingle(AIn023[5]), Min = Convert.ToSingle(AIn023[6]), Offset = Convert.ToSingle(AIn023[7]) }); }
                else if (strAINames[i] == AIn024[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn024", Point = AIn024[0], Alias = AIn024[1], Handle = hAIn024, Units = AIn024[2], Decimals = AIn024[3], Low = Convert.ToSingle(AIn024[4]), High = Convert.ToSingle(AIn024[5]), Min = Convert.ToSingle(AIn024[6]), Offset = Convert.ToSingle(AIn024[7]) }); }
                else if (strAINames[i] == AIn025[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn025", Point = AIn025[0], Alias = AIn025[1], Handle = hAIn025, Units = AIn025[2], Decimals = AIn025[3], Low = Convert.ToSingle(AIn025[4]), High = Convert.ToSingle(AIn025[5]), Min = Convert.ToSingle(AIn025[6]), Offset = Convert.ToSingle(AIn025[7]) }); }
                else if (strAINames[i] == AIn026[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn026", Point = AIn026[0], Alias = AIn026[1], Handle = hAIn026, Units = AIn026[2], Decimals = AIn026[3], Low = Convert.ToSingle(AIn026[4]), High = Convert.ToSingle(AIn026[5]), Min = Convert.ToSingle(AIn026[6]), Offset = Convert.ToSingle(AIn026[7]) }); }
                else if (strAINames[i] == AIn027[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn027", Point = AIn027[0], Alias = AIn027[1], Handle = hAIn027, Units = AIn027[2], Decimals = AIn027[3], Low = Convert.ToSingle(AIn027[4]), High = Convert.ToSingle(AIn027[5]), Min = Convert.ToSingle(AIn027[6]), Offset = Convert.ToSingle(AIn027[7]) }); }
                else if (strAINames[i] == AIn028[0])
                { _PointCollection.Add(new PointInfo { Name = "AIn028", Point = AIn028[0], Alias = AIn028[1], Handle = hAIn028, Units = AIn028[2], Decimals = AIn028[3], Low = Convert.ToSingle(AIn028[4]), High = Convert.ToSingle(AIn028[5]), Min = Convert.ToSingle(AIn028[6]), Offset = Convert.ToSingle(AIn028[7]) }); }
            }
            _PointCollection.Add(new PointInfo { Name = "AIn040", Point = AIn040[0], Alias = AIn040[1], Handle = hAIn040, Units = AIn040[2], Decimals = AIn040[3], Low = Convert.ToSingle(AIn040[4]), High = Convert.ToSingle(AIn040[5]), Min = Convert.ToSingle(AIn040[6]), Offset = Convert.ToSingle(AIn040[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn041", Point = AIn041[0], Alias = AIn041[1], Handle = hAIn041, Units = AIn041[2], Decimals = AIn041[3], Low = Convert.ToSingle(AIn041[4]), High = Convert.ToSingle(AIn041[5]), Min = Convert.ToSingle(AIn041[6]), Offset = Convert.ToSingle(AIn041[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn042", Point = AIn042[0], Alias = AIn042[1], Handle = hAIn042, Units = AIn042[2], Decimals = AIn042[3], Low = Convert.ToSingle(AIn042[4]), High = Convert.ToSingle(AIn042[5]), Min = Convert.ToSingle(AIn042[6]), Offset = Convert.ToSingle(AIn042[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn043", Point = AIn043[0], Alias = AIn043[1], Handle = hAIn043, Units = AIn043[2], Decimals = AIn043[3], Low = Convert.ToSingle(AIn043[4]), High = Convert.ToSingle(AIn043[5]), Min = Convert.ToSingle(AIn043[6]), Offset = Convert.ToSingle(AIn043[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn044", Point = AIn044[0], Alias = AIn044[1], Handle = hAIn044, Units = AIn044[2], Decimals = AIn044[3], Low = Convert.ToSingle(AIn044[4]), High = Convert.ToSingle(AIn044[5]), Min = Convert.ToSingle(AIn044[6]), Offset = Convert.ToSingle(AIn044[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn045", Point = AIn045[0], Alias = AIn045[1], Handle = hAIn045, Units = AIn045[2], Decimals = AIn045[3], Low = Convert.ToSingle(AIn045[4]), High = Convert.ToSingle(AIn045[5]), Min = Convert.ToSingle(AIn045[6]), Offset = Convert.ToSingle(AIn045[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn046", Point = AIn046[0], Alias = AIn046[1], Handle = hAIn046, Units = AIn046[2], Decimals = AIn046[3], Low = Convert.ToSingle(AIn046[4]), High = Convert.ToSingle(AIn046[5]), Min = Convert.ToSingle(AIn046[6]), Offset = Convert.ToSingle(AIn046[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn050", Point = AIn050[0], Alias = AIn050[1], Handle = hAIn050, Units = AIn050[2], Decimals = AIn050[3], Low = Convert.ToSingle(AIn050[4]), High = Convert.ToSingle(AIn050[5]), Min = Convert.ToSingle(AIn050[6]), Offset = Convert.ToSingle(AIn050[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn051", Point = AIn051[0], Alias = AIn051[1], Handle = hAIn051, Units = AIn051[2], Decimals = AIn051[3], Low = Convert.ToSingle(AIn051[4]), High = Convert.ToSingle(AIn051[5]), Min = Convert.ToSingle(AIn051[6]), Offset = Convert.ToSingle(AIn051[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn052", Point = AIn052[0], Alias = AIn052[1], Handle = hAIn052, Units = AIn052[2], Decimals = AIn052[3], Low = Convert.ToSingle(AIn052[4]), High = Convert.ToSingle(AIn052[5]), Min = Convert.ToSingle(AIn052[6]), Offset = Convert.ToSingle(AIn052[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn053", Point = AIn053[0], Alias = AIn053[1], Handle = hAIn053, Units = AIn053[2], Decimals = AIn053[3], Low = Convert.ToSingle(AIn053[4]), High = Convert.ToSingle(AIn053[5]), Min = Convert.ToSingle(AIn053[6]), Offset = Convert.ToSingle(AIn053[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn054", Point = AIn054[0], Alias = AIn054[1], Handle = hAIn054, Units = AIn054[2], Decimals = AIn054[3], Low = Convert.ToSingle(AIn054[4]), High = Convert.ToSingle(AIn054[5]), Min = Convert.ToSingle(AIn054[6]), Offset = Convert.ToSingle(AIn054[7]) });
            _PointCollection.Add(new PointInfo { Name = "AIn055", Point = AIn055[0], Alias = AIn055[1], Handle = hAIn055, Units = AIn055[2], Decimals = AIn055[3], Low = Convert.ToSingle(AIn055[4]), High = Convert.ToSingle(AIn055[5]), Min = Convert.ToSingle(AIn055[6]), Offset = Convert.ToSingle(AIn055[7]) });

            _PointCollection.Add(new PointInfo { Point = AOut000[0], Handle = hAOut000, Alias = AOut000[1], Units = AOut000[2], Decimals = AOut000[3], Low = Convert.ToSingle(AOut000[4]), High = Convert.ToSingle(AOut000[5]), Min = Convert.ToSingle(AOut000[6]), Offset = Convert.ToSingle(AOut000[7]) });
            _PointCollection.Add(new PointInfo { Point = AOut000RPM[0], Handle = hAOut000RPM, Alias = AOut000RPM[1], Units = AOut000RPM[2], Decimals = AOut000RPM[3], Low = Convert.ToSingle(AOut000RPM[4]), High = Convert.ToSingle(AOut000RPM[5]) });
            _PointCollection.Add(new PointInfo { Point = AOut007[0], Handle = hAOut007, Alias = AOut007[1], Units = AOut007[2], Decimals = AOut007[3], Low = Convert.ToSingle(AOut007[4]), High = Convert.ToSingle(AOut007[5]), Min = Convert.ToSingle(AOut007[6]), Offset = Convert.ToSingle(AOut007[7]) });
            _PointCollection.Add(new PointInfo { Point = AOut008[0], Handle = hAOut008, Alias = AOut008[1], Units = AOut008[2], Decimals = AOut008[3], Low = Convert.ToSingle(AOut008[4]), High = Convert.ToSingle(AOut008[5]), Min = Convert.ToSingle(AOut008[6]), Offset = Convert.ToSingle(AOut008[7]) });
            _PointCollection.Add(new PointInfo { Point = AOut009[0], Handle = hAOut009, Alias = AOut009[1], Units = AOut009[2], Decimals = AOut009[3], Low = Convert.ToSingle(AOut009[4]), High = Convert.ToSingle(AOut009[5]), Min = Convert.ToSingle(AOut009[6]), Offset = Convert.ToSingle(AOut009[7]) });
            _PointCollection.Add(new PointInfo { Point = AOut010[0], Handle = hAOut010, Alias = AOut010[1], Units = AOut010[2], Decimals = AOut010[3], Low = Convert.ToSingle(AOut010[4]), High = Convert.ToSingle(AOut010[5]), Min = Convert.ToSingle(AOut010[6]), Offset = Convert.ToSingle(AOut010[7]) });

            _PointCollection.Add(new PointInfo { Point = Temp000[0], Handle = hTemp000, Alias = Temp000[1], Units = Temp000[2], Decimals = Temp000[3], Low = Convert.ToSingle(Temp000[4]), High = Convert.ToSingle(Temp000[5]), Min = Convert.ToSingle(Temp000[6]), Offset = Convert.ToSingle(Temp000[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp001[0], Handle = hTemp001, Alias = Temp001[1], Units = Temp001[2], Decimals = Temp001[3], Low = Convert.ToSingle(Temp001[4]), High = Convert.ToSingle(Temp001[5]), Min = Convert.ToSingle(Temp001[6]), Offset = Convert.ToSingle(Temp001[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp002[0], Handle = hTemp002, Alias = Temp002[1], Units = Temp002[2], Decimals = Temp002[3], Low = Convert.ToSingle(Temp002[4]), High = Convert.ToSingle(Temp002[5]), Min = Convert.ToSingle(Temp002[6]), Offset = Convert.ToSingle(Temp002[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp003[0], Handle = hTemp003, Alias = Temp003[1], Units = Temp003[2], Decimals = Temp003[3], Low = Convert.ToSingle(Temp003[4]), High = Convert.ToSingle(Temp003[5]), Min = Convert.ToSingle(Temp003[6]), Offset = Convert.ToSingle(Temp003[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp004[0], Handle = hTemp004, Alias = Temp004[1], Units = Temp004[2], Decimals = Temp004[3], Low = Convert.ToSingle(Temp004[4]), High = Convert.ToSingle(Temp004[5]), Min = Convert.ToSingle(Temp004[6]), Offset = Convert.ToSingle(Temp004[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp005[0], Handle = hTemp005, Alias = Temp005[1], Units = Temp005[2], Decimals = Temp005[3], Low = Convert.ToSingle(Temp005[4]), High = Convert.ToSingle(Temp005[5]), Min = Convert.ToSingle(Temp005[6]), Offset = Convert.ToSingle(Temp005[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp006[0], Handle = hTemp006, Alias = Temp006[1], Units = Temp006[2], Decimals = Temp006[3], Low = Convert.ToSingle(Temp006[4]), High = Convert.ToSingle(Temp006[5]), Min = Convert.ToSingle(Temp006[6]), Offset = Convert.ToSingle(Temp006[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp007[0], Handle = hTemp007, Alias = Temp007[1], Units = Temp007[2], Decimals = Temp007[3], Low = Convert.ToSingle(Temp007[4]), High = Convert.ToSingle(Temp007[5]), Min = Convert.ToSingle(Temp007[6]), Offset = Convert.ToSingle(Temp007[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp008[0], Handle = hTemp008, Alias = Temp008[1], Units = Temp008[2], Decimals = Temp008[3], Low = Convert.ToSingle(Temp008[4]), High = Convert.ToSingle(Temp008[5]), Min = Convert.ToSingle(Temp008[6]), Offset = Convert.ToSingle(Temp008[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp009[0], Handle = hTemp009, Alias = Temp009[1], Units = Temp009[2], Decimals = Temp009[3], Low = Convert.ToSingle(Temp009[4]), High = Convert.ToSingle(Temp009[5]), Min = Convert.ToSingle(Temp009[6]), Offset = Convert.ToSingle(Temp009[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp010[0], Handle = hTemp010, Alias = Temp010[1], Units = Temp010[2], Decimals = Temp010[3], Low = Convert.ToSingle(Temp010[4]), High = Convert.ToSingle(Temp010[5]), Min = Convert.ToSingle(Temp010[6]), Offset = Convert.ToSingle(Temp010[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp011[0], Handle = hTemp011, Alias = Temp011[1], Units = Temp011[2], Decimals = Temp011[3], Low = Convert.ToSingle(Temp011[4]), High = Convert.ToSingle(Temp011[5]), Min = Convert.ToSingle(Temp011[6]), Offset = Convert.ToSingle(Temp011[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp012[0], Handle = hTemp012, Alias = Temp012[1], Units = Temp012[2], Decimals = Temp012[3], Low = Convert.ToSingle(Temp012[4]), High = Convert.ToSingle(Temp012[5]), Min = Convert.ToSingle(Temp012[6]), Offset = Convert.ToSingle(Temp012[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp013[0], Handle = hTemp013, Alias = Temp013[1], Units = Temp013[2], Decimals = Temp013[3], Low = Convert.ToSingle(Temp013[4]), High = Convert.ToSingle(Temp013[5]), Min = Convert.ToSingle(Temp013[6]), Offset = Convert.ToSingle(Temp013[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp014[0], Handle = hTemp014, Alias = Temp014[1], Units = Temp014[2], Decimals = Temp014[3], Low = Convert.ToSingle(Temp014[4]), High = Convert.ToSingle(Temp014[5]), Min = Convert.ToSingle(Temp014[6]), Offset = Convert.ToSingle(Temp014[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp015[0], Handle = hTemp015, Alias = Temp015[1], Units = Temp015[2], Decimals = Temp015[3], Low = Convert.ToSingle(Temp015[4]), High = Convert.ToSingle(Temp015[5]), Min = Convert.ToSingle(Temp015[6]), Offset = Convert.ToSingle(Temp015[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp016[0], Handle = hTemp016, Alias = Temp016[1], Units = Temp016[2], Decimals = Temp016[3], Low = Convert.ToSingle(Temp016[4]), High = Convert.ToSingle(Temp016[5]), Min = Convert.ToSingle(Temp016[6]), Offset = Convert.ToSingle(Temp016[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp017[0], Handle = hTemp017, Alias = Temp017[1], Units = Temp017[2], Decimals = Temp017[3], Low = Convert.ToSingle(Temp017[4]), High = Convert.ToSingle(Temp017[5]), Min = Convert.ToSingle(Temp017[6]), Offset = Convert.ToSingle(Temp017[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp018[0], Handle = hTemp018, Alias = Temp018[1], Units = Temp018[2], Decimals = Temp018[3], Low = Convert.ToSingle(Temp018[4]), High = Convert.ToSingle(Temp018[5]), Min = Convert.ToSingle(Temp018[6]), Offset = Convert.ToSingle(Temp018[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp019[0], Handle = hTemp019, Alias = Temp019[1], Units = Temp019[2], Decimals = Temp019[3], Low = Convert.ToSingle(Temp019[4]), High = Convert.ToSingle(Temp019[5]), Min = Convert.ToSingle(Temp019[6]), Offset = Convert.ToSingle(Temp019[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp020[0], Handle = hTemp020, Alias = Temp020[1], Units = Temp020[2], Decimals = Temp020[3], Low = Convert.ToSingle(Temp020[4]), High = Convert.ToSingle(Temp020[5]), Min = Convert.ToSingle(Temp020[6]), Offset = Convert.ToSingle(Temp020[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp021[0], Handle = hTemp021, Alias = Temp021[1], Units = Temp021[2], Decimals = Temp021[3], Low = Convert.ToSingle(Temp021[4]), High = Convert.ToSingle(Temp021[5]), Min = Convert.ToSingle(Temp021[6]), Offset = Convert.ToSingle(Temp021[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp022[0], Handle = hTemp022, Alias = Temp022[1], Units = Temp022[2], Decimals = Temp022[3], Low = Convert.ToSingle(Temp022[4]), High = Convert.ToSingle(Temp022[5]), Min = Convert.ToSingle(Temp022[6]), Offset = Convert.ToSingle(Temp022[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp023[0], Handle = hTemp023, Alias = Temp023[1], Units = Temp023[2], Decimals = Temp023[3], Low = Convert.ToSingle(Temp023[4]), High = Convert.ToSingle(Temp023[5]), Min = Convert.ToSingle(Temp023[6]), Offset = Convert.ToSingle(Temp023[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp024[0], Handle = hTemp024, Alias = Temp024[1], Units = Temp024[2], Decimals = Temp024[3], Low = Convert.ToSingle(Temp024[4]), High = Convert.ToSingle(Temp024[5]), Min = Convert.ToSingle(Temp024[6]), Offset = Convert.ToSingle(Temp024[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp025[0], Handle = hTemp025, Alias = Temp025[1], Units = Temp025[2], Decimals = Temp025[3], Low = Convert.ToSingle(Temp025[4]), High = Convert.ToSingle(Temp025[5]), Min = Convert.ToSingle(Temp025[6]), Offset = Convert.ToSingle(Temp025[7]) });
            _PointCollection.Add(new PointInfo { Point = Temp026[0], Handle = hTemp026, Alias = Temp026[1], Units = Temp026[2], Decimals = Temp026[3], Low = Convert.ToSingle(Temp026[4]), High = Convert.ToSingle(Temp026[5]), Min = Convert.ToSingle(Temp026[6]), Offset = Convert.ToSingle(Temp026[7]) });

            LoadDOPointArrays();
        }

        public static void LoadAIPointArrays()
        {
            strAINames[0] = AIn000[0];
            strAINames[1] = AIn001[0];
            strAINames[2] = AIn002[0];
            strAINames[3] = AIn003[0];
            strAINames[4] = AIn004[0];
            strAINames[5] = AIn005[0];
            strAINames[6] = AIn006[0];
            strAINames[7] = AIn007[0];
            strAINames[8] = AIn008[0];
            strAINames[9] = AIn009[0];
            strAINames[10] = AIn010[0];
            strAINames[11] = AIn011[0];
            strAINames[12] = AIn012[0];
            strAINames[13] = AIn013[0];
            strAINames[14] = AIn014[0];
            strAINames[15] = AIn015[0];
            strAINames[16] = AIn016[0];
            strAINames[17] = AIn017[0];
            strAINames[18] = AIn018[0];
            strAINames[19] = AIn019[0];
            strAINames[20] = AIn020[0];
            strAINames[21] = AIn021[0];
            strAINames[22] = AIn022[0];
            strAINames[23] = AIn023[0];
            strAINames[24] = AIn024[0];
            strAINames[25] = AIn025[0];
            strAINames[26] = AIn026[0];
            strAINames[27] = AIn027[0];
            strAINames[28] = AIn028[0];
            strAIAliases[0] = AIn000[1];
            strAIAliases[1] = AIn001[1];
            strAIAliases[2] = AIn002[1];
            strAIAliases[3] = AIn003[1];
            strAIAliases[4] = AIn004[1];
            strAIAliases[5] = AIn005[1];
            strAIAliases[6] = AIn006[1];
            strAIAliases[7] = AIn007[1];
            strAIAliases[8] = AIn008[1];
            strAIAliases[9] = AIn009[1];
            strAIAliases[10] = AIn010[1];
            strAIAliases[11] = AIn011[1];
            strAIAliases[12] = AIn012[1];
            strAIAliases[13] = AIn013[1];
            strAIAliases[14] = AIn014[1];
            strAIAliases[15] = AIn015[1];
            strAIAliases[16] = AIn016[1];
            strAIAliases[17] = AIn017[1];
            strAIAliases[18] = AIn018[1];
            strAIAliases[19] = AIn019[1];
            strAIAliases[20] = AIn020[1];
            strAIAliases[21] = AIn021[1];
            strAIAliases[22] = AIn022[1];
            strAIAliases[23] = AIn023[1];
            strAIAliases[24] = AIn024[1];
            strAIAliases[25] = AIn025[1];
            strAIAliases[26] = AIn026[1];
            strAIAliases[27] = AIn027[1];
            strAIAliases[28] = AIn028[1];
            Array.Sort(strAINames);
            Array.Sort(strAIAliases);
        }

        public static void LoadDOPointArrays()
        {
            strDONames[0] = DO000[0];
            strDOAliases[0] = DO000[1];
            strDONames[1] = DO001[0];
            strDOAliases[1] = DO001[1];
            strDONames[2] = DO002[0];
            strDOAliases[2] = DO002[1];
            strDONames[3] = DO003[0];
            strDOAliases[3] = DO003[1];
            strDONames[4] = DO004[0];
            strDOAliases[4] = DO004[1];
            strDONames[5] = DO005[0];
            strDOAliases[5] = DO005[1];
            strDONames[6] = DO006[0];
            strDOAliases[6] = DO006[1];
            strDONames[7] = DO007[0];
            strDOAliases[7] = DO007[1];
            strDONames[8] = DO008[0];
            strDOAliases[8] = DO008[1];
            strDONames[9] = DO009[0];
            strDOAliases[9] = DO009[1];
            strDONames[10] = DO010[0];
            strDOAliases[10] = DO010[1];
            strDONames[11] = DO011[0];
            strDOAliases[11] = DO011[1];
            strDONames[12] = DO012[0];
            strDOAliases[12] = DO012[1];
            strDONames[13] = DO013[0];
            strDOAliases[13] = DO013[1];
            strDONames[14] = DO014[0];
            strDOAliases[14] = DO014[1];
            strDONames[15] = DO015[0];
            strDOAliases[15] = DO015[1];
            strDONames[16] = DO016[0];
            strDOAliases[16] = DO016[1];
            strDONames[17] = DO017[0];
            strDOAliases[17] = DO017[1];
            strDONames[18] = DO018[0];
            strDOAliases[18] = DO018[1];
            strDONames[19] = DO019[0];
            strDOAliases[19] = DO019[1];
            strDONames[20] = DO020[0];
            strDOAliases[20] = DO020[1];
            strDONames[21] = DO021[0];
            strDOAliases[21] = DO021[1];
            strDONames[22] = DO022[0];                                              // 09/20/23 PS
            strDOAliases[22] = DO022[1];                                            // 09/20/23 PS
            Array.Sort(strDONames);
            Array.Sort(strDOAliases);
        }

        public static void LoadGraphCombobox()
        {
            int count = _PointCollection.Count;
            string[] points = new string[count];
            int cnt = 0;
            foreach (PointInfo i in _PointCollection)
            {
                if (cnt > 0)
                {
                    points[cnt] = i.Point;
                }
                cnt++;
            }
            _cmbPointList.Add("Inactive");
            for (int i = 1; i < count; i++)
            {
                _cmbPointList.Add(points[i]);
            }
        }

        public static void LoadComboBoxes()
        {
            for (int i = 0; i < ciUnitsCount; i++)
            {
                if (!string.IsNullOrEmpty(Units.strUnits[i]))
                    _cmbUnitList.Add(Units.strUnits[i]);
            }
            _cmbDecimalList.Add("0");
            _cmbDecimalList.Add("1");
            _cmbDecimalList.Add("2");
            _cmbDecimalList.Add("3");
        }

        public static void LoadComboCartridges()                                                                            // 08/18/23 PS
        {
            _CartridgeList.Clear();
            for (int i = 0; i < iCartridgeCount; i++)
            {
                _CartridgeList.Add(strCartridgeName[i]);
            }
        }

        public static void LoadComboSchemas()                                                                            // 09/18/23 PS
        {
            _SchemaList.Clear();
            for (int i = 0; i < iSchemaCount; i++)
            {
                _SchemaList.Add(strSchemaName[i]);
            }
        }

        public static void GetGraphInfo()
        {
            for (int i = 0; i < 6; i++)
            {
                int k = 0;
                foreach (PointInfo j in _PointCollection)
                {
                    if (j.Point == strGraphPoint[i])
                    {
                        strGraphAlias[i] = j.Alias;
                        iGraphHandle[i] = j.Handle;
                        iSelectedItem[i] = k;
                        strUnits[i] = j.Units;
                        strDecimals[i] = j.Decimals;
                    }
                    k++;
                }
            }
        }

        public static float GetLowPoint(int index)
        {
            return _PointCollection[index].Low;
        }

        public static float GetHighPoint(int index)
        {
            return _PointCollection[index].High;
        }

        public class PointInfo
        {
            public string Point { get; set; }
            public string Alias { get; set; }
            public int Handle { get; set; }
            public string Units { get; set; }
            public string Decimals { get; set; }
            public float Low { get; set; }
            public float High { get; set; }
            public float Min { get; set; }
            public float Offset { get; set; }
            public string Name { get; set; }
        }

        #endregion

        #region ADS Read/Write Services

        public static void SetAdsValue(string name, object value)
        {
            if (bTCStarted && !bAdsDown)
            {
                try
                {
                    int handle = tcClient.CreateVariableHandle(name);
                    Type t = value.GetType();
                    if (t == typeof(double))
                    { WriteAdsDouble(handle, (double)value); }
                    else if (t == typeof(float))
                    { WriteAdsSingle(handle, (float)value); }
                    else if (t == typeof(bool))
                    { WriteAdsBool(handle, (bool)value); }
                    else if (t == typeof(short))
                    { WriteAdsInt16(handle, (short)value); }
                    else if (t == typeof(int))
                    { WriteAdsInt32(handle, (int)value); }
                    else if (t == typeof(string))
                    { WriteAdsSingle(handle, Convert.ToSingle((string)value)); }
                    tcClient.DeleteVariableHandle(handle);
                }
                catch (Exception ex)
                {
                    if (!AdsCommDown())
                    {
                        App.WPFMessageBoxOK(ex.Message + ", " + name, App.bgRed);
                    }
                }
            }
        }

        public static void WriteAdsBool(int handle, bool value)
        {
            try
            { tcClient.WriteAny(handle, value); }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                { App.WPFMessageBoxOK(ex.Message + ", WriteAdsBool_" + value.ToString(), App.bgRed); }
            }
        }

        public static void WriteAdsInt16(int handle, short value)
        {
            try
            { tcClient.WriteAny(handle, value); }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                { App.WPFMessageBoxOK(ex.Message + ", WriteAdsInt16_" + value.ToString(), App.bgRed); }
            }
        }

        public static void WriteAdsInt32(int handle, int value)
        {
            try
            { tcClient.WriteAny(handle, value); }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                { App.WPFMessageBoxOK(ex.Message + ", WriteAdsInt32_" + value.ToString(), App.bgRed); }
            }
        }

        public static void WriteAdsDouble(int handle, double value)
        {
            try
            { tcClient.WriteAny(handle, value); }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                { App.WPFMessageBoxOK(ex.Message + ", WriteAdsDouble_" + value.ToString(), App.bgRed); }
            }
        }

        public static void WriteAdsSingle(int handle, float value)
        {
            try
            { tcClient.WriteAny(handle, value); }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                { App.WPFMessageBoxOK(ex.Message + ", WriteAdsSingle_" + value.ToString(), App.bgRed); }
            }
        }

        public static void WriteAdsString(int handle, string value)
        {
            try
            {
                int len = value.Length;
                tcClient.WriteAny(handle, value, new int[] { len });
            }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                { App.WPFMessageBoxOK(ex.Message + ", WriteAdsString", App.bgRed); }
            }
        }

        public static void SetAdsString(string name, string value)
        {
            int handle = tcClient.CreateVariableHandle(name);
            WriteAdsString(handle, value);
        }

        public static bool AdsCommDown()
        {
            try
            {
                object test = tcClient.ReadState();
                bAdsDown = false;
            }
            catch
            {
                bAdsDown = true;
                bAdsOffline = true;
                iLogging = (short)LogType.No_Log;
            }
            return bAdsDown;
        }

        public static object GetAdsValue(string name, Type type)
        {
            try
            {
                object o = new object();
                int handle = tcClient.CreateVariableHandle(name);
                o = tcClient.ReadAny(handle, type);
                tcClient.DeleteVariableHandle(handle);
                return o;
            }
            catch (Exception ex)
            {
                if (!AdsCommDown())
                { App.WPFMessageBoxOK(ex.Message + ", GetAdsValue " + name, App.bgRed); }
            }
            return 0;
        }

        public static float ReadAdsSingle(int handle, string point)
        {
            if (!bAdsDown)
            {
                try
                { return (float)tcClient.ReadAny(handle, typeof(float)); }
                catch (Exception ex)
                {
                    if (!AdsCommDown())
                    { App.WPFMessageBoxOK(ex.Message + ", ReadAdsSingle " + point, App.bgRed); }
                }
            }
            return 0;
        }

        public static short ReadHandleInt(int handle)
        {
            if (!bAdsDown)
            {
                try
                { return (short)tcClient.ReadAny(handle, typeof(short)); }
                catch (Exception ex)
                {
                    if (!AdsCommDown())
                    { App.WPFMessageBoxOK(ex.Message + ", ReadHandleInt ", App.bgRed); }
                }
            }
            return 0;
        }

        public static string ReadAdsString(int handle, int len)
        {
            if (!bAdsDown)
            {
                try
                {
                    return tcClient.ReadAny(handle, typeof(string), new int[] { len }).ToString();
                }
                catch (Exception ex)
                {
                    if (!AdsCommDown())
                    {
                        App.WPFMessageBoxOK(ex.Message + ", ReadAdsString ", App.bgRed);
                    }
                }
            }
            return "";
        }

        #endregion

    }
}

