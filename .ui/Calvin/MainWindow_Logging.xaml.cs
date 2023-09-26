// Carbon Capture - Calvin
// MainWindow_Logging.xaml.cs
// Rev 1.00 - September 22, 2023

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace Calvin
{
    public partial class MainWindow
    {
        private const string name = "calvin_";
        private const string c = ",";
        private const string lsh = "_LSH,";                                                                                 // 09/19/23 PS
        private const string lsl = "_LSL,";                                                                                 // 09/19/23 PS
        private const string aws1 = "s3://carbon-capture/reactor-data/calvin/raw/";
        private const string aws2 = "s3://carbon-capture/reactor-data/calvin/cycle-metadata/";
        private const string strState = "cycle_stage";                                                                      // 07/14/23 PS - Add current state to file
        private const int iCount = 200;

        private int iCounter;
        private bool bLoggingStarted;
        private bool bReportCreated;
        private bool bWriteToCloud;
        private string strReport1;
        private string strReport2;
        private readonly string[] strReportName = new string[9] { "no_log", "manual", "warmup", "evacuation", "steam_bypass", "desorption", "repressurization", "adsorption", "cycle" };
        private string line1;
        private string line2;

        private static readonly ObservableCollection<LogInfo> _LogCollection = new ObservableCollection<LogInfo>();
        public ObservableCollection<LogInfo> LogCollection => _LogCollection;

        private void LogValues()
        {
            if (bWriteToCloud)
            {
                WriteToCloud(aws1, strReport1);
                if (bWriteToCloud)
                {
                    WriteToCloud(aws2, strReport2);
                }
                bWriteToCloud = false;
                ADS.bUploading = false;
            }
            if (ADS.iLogging > (short)LogType.No_Log)
            {
                bLoggingStarted = true;
                iLogCount++;
                if (iLogCount >= (ADS.iSampling * 2))
                {
                    lblTime.Content = DateTime.Now.ToString(@"HH\:mm\:ss");
                    _LogCollection.Add(new LogInfo
                    {
                        DateTime = DateTime.Now.ToString(@"yyyy_MM_dd HH_mm_ss") + c,
                        D1 = Units.strFormatc(ADS.sAOut000RPM, ADS.AOut000RPM[3]),
                        D2 = Units.strFormatc(ADS.sAOut000, ADS.AOut000[3]),
                        D3 = Units.strFormatc(ADS.sAIn000, ADS.AIn000[3]),
                        D4 = Units.strFormatc(ADS.sAIn001, ADS.AIn001[3]),
                        D5 = Units.strFormatc(ADS.sAIn002, ADS.AIn002[3]),
                        D6 = Units.strFormatc(ADS.sAIn003, ADS.AIn003[3]),
                        D7 = Units.strFormatc(ADS.sAIn004, ADS.AIn004[3]),
                        D8 = Units.strFormatc(ADS.sAIn005, ADS.AIn005[3]),
                        D9 = Units.strFormatc(ADS.sAIn006, ADS.AIn006[3]),
                        D10 = Units.strFormatc(ADS.sAIn007, ADS.AIn007[3]),
                        D11 = Units.strFormatc(ADS.sAIn008, ADS.AIn008[3]),
                        D12 = Units.strFormatc(ADS.sAIn009, ADS.AIn009[3]),
                        D13 = Units.strFormatc(ADS.sAIn010, ADS.AIn010[3]),
                        D14 = Units.strFormatc(ADS.sAIn011, ADS.AIn011[3]),
                        D15 = Units.strFormatc(ADS.sAIn012, ADS.AIn012[3]),
                        D16 = Units.strFormatc(ADS.sAIn013, ADS.AIn013[3]),
                        D17 = Units.strFormatc(ADS.sAIn014, ADS.AIn014[3]),
                        D18 = Units.strFormatc(ADS.sAIn015, ADS.AIn015[3]),
                        D19 = Units.strFormatc(ADS.sAIn016, ADS.AIn016[3]),
                        D20 = Units.strFormatc(ADS.sAIn017, ADS.AIn017[3]),
                        D21 = Units.strFormatc(ADS.sAIn018, ADS.AIn018[3]),
                        D22 = Units.strFormatc(ADS.sAIn019, ADS.AIn019[3]),
                        D23 = Units.strFormatc(ADS.sAIn020, ADS.AIn020[3]),
                        D24 = Units.strFormatc(ADS.sAIn021, ADS.AIn021[3]),
                        D25 = Units.strFormatc(ADS.sAIn022, ADS.AIn022[3]),
                        D26 = Units.strFormatc(ADS.sAIn023, ADS.AIn023[3]),
                        D27 = Units.strFormatc(ADS.sAIn024, ADS.AIn024[3]),
                        D28 = Units.strFormatc(ADS.sAIn025, ADS.AIn025[3]),
                        D29 = Units.strFormatc(ADS.sAIn026, ADS.AIn026[3]),
                        D30 = Units.strFormatc(ADS.sAIn027, ADS.AIn027[3]),
                        D31 = Units.strFormatc(ADS.sAIn028, ADS.AIn028[3]),
                        D32 = Units.strFormatc(ADS.sAIn040, ADS.AIn040[3]),
                        D33 = Units.strFormatc(ADS.sAIn041, ADS.AIn041[3]),
                        D34 = Units.strFormatc(ADS.sAIn042, ADS.AIn042[3]),
                        D35 = Units.strFormatc(ADS.sAIn043, ADS.AIn043[3]),
                        D36 = Units.strFormatc(ADS.sAIn044, ADS.AIn044[3]),
                        D37 = Units.strFormatc(ADS.sAIn045, ADS.AIn045[3]),
                        D38 = Units.strFormatc(ADS.sAIn046, ADS.AIn046[3]),
                        D39 = Units.strFormatc(ADS.sAIn050, ADS.AIn050[3]),
                        D40 = Units.strFormatc(ADS.sAIn051, ADS.AIn051[3]),
                        D41 = Units.strFormatc(ADS.sAIn052, ADS.AIn052[3]),
                        D42 = Units.strFormatc(ADS.sAIn053, ADS.AIn053[3]),
                        D43 = Units.strFormatc(ADS.sAIn054, ADS.AIn054[3]),
                        D44 = Units.strFormatc(ADS.sAIn055, ADS.AIn055[3]),
                        D45 = Units.strFormatc(ADS.sTemp000, ADS.Temp000[3]),
                        D46 = Units.strFormatc(ADS.sTemp001, ADS.Temp001[3]),
                        D47 = Units.strFormatc(ADS.sTemp002, ADS.Temp002[3]),
                        D48 = Units.strFormatc(ADS.sTemp003, ADS.Temp003[3]),
                        D49 = Units.strFormatc(ADS.sTemp004, ADS.Temp004[3]),
                        D50 = Units.strFormatc(ADS.sTemp005, ADS.Temp005[3]),
                        D51 = Units.strFormatc(ADS.sTemp006, ADS.Temp006[3]),
                        D52 = Units.strFormatc(ADS.sTemp007, ADS.Temp007[3]),
                        D53 = Units.strFormatc(ADS.sTemp008, ADS.Temp008[3]),
                        D54 = Units.strFormatc(ADS.sTemp009, ADS.Temp009[3]),
                        D55 = Units.strFormatc(ADS.sTemp010, ADS.Temp010[3]),
                        D56 = Units.strFormatc(ADS.sTemp011, ADS.Temp011[3]),
                        D57 = Units.strFormatc(ADS.sTemp012, ADS.Temp012[3]),
                        D58 = Units.strFormatc(ADS.sTemp013, ADS.Temp013[3]),
                        D59 = Units.strFormatc(ADS.sTemp014, ADS.Temp014[3]),
                        D60 = Units.strFormatc(ADS.sTemp015, ADS.Temp015[3]),
                        D61 = Units.strFormatc(ADS.sTemp016, ADS.Temp016[3]),
                        D62 = Units.strFormatc(ADS.sTemp017, ADS.Temp017[3]),
                        D63 = Units.strFormatc(ADS.sTemp018, ADS.Temp018[3]),
                        D64 = Units.strFormatc(ADS.sTemp019, ADS.Temp019[3]),
                        D65 = Units.strFormatc(ADS.sTemp020, ADS.Temp020[3]),
                        D66 = Units.strFormatc(ADS.sTemp021, ADS.Temp021[3]),
                        D67 = Units.strFormatc(ADS.sTemp022, ADS.Temp022[3]),
                        D68 = Units.strFormatc(ADS.sTemp023, ADS.Temp023[3]),
                        D69 = Units.strFormatc(ADS.sTemp024, ADS.Temp024[3]),
                        D70 = Units.strFormatc(ADS.sTemp025, ADS.Temp025[3]),
                        D71 = Units.strFormatc(ADS.sTemp026, ADS.Temp026[3]),
                        D72 = App.OnOffc(!ADS.bDO000),
                        D73 = App.OnOffc(!ADS.bDO001),
                        D74 = App.OnOffc(!ADS.bDO002),
                        D75 = App.OnOffc(!ADS.bDO003),
                        D76 = App.OpenCloseTransitc(ADS.bDO004_Open, ADS.bDO004_Closed),
                        D77 = App.OpenCloseTransitc(ADS.bDO005_Open, ADS.bDO005_Closed),
                        D78 = App.OpenCloseTransitc(ADS.bDO006_Open, ADS.bDO006_Closed),
                        D79 = App.OnOffc(!ADS.bDO007),
                        D80 = App.OnOffc(!ADS.bDO007_LSH),
                        D81 = App.OnOffc(!ADS.bDO007_LSL),
                        D82 = App.OpenCloseTransitc(ADS.bDO008_Open, ADS.bDO008_Closed),
                        D83 = App.OpenCloseTransitc(ADS.bDO009_Open, ADS.bDO009_Closed),
                        D84 = App.OnOffc(!ADS.bDO010),
                        D85 = App.OnOffc(!ADS.bDO011),
                        D86 = App.OnOffc(!ADS.bDO011_LSH),
                        D87 = App.OnOffc(!ADS.bDO011_LSL),
                        D88 = App.OnOffc(!ADS.bDO012),
                        D89 = App.OpenCloseTransitc(ADS.bDO013_Open, ADS.bDO013_Closed),
                        D90 = App.OpenCloseTransitc(ADS.bDO014_Open, ADS.bDO014_Closed),
                        D91 = App.OnOffc(!ADS.bDO015),
                        D92 = App.OpenCloseTransitc(ADS.bDO016_Open, ADS.bDO016_Closed),
                        D93 = App.OpenCloseTransitc(ADS.bDO017_Open, ADS.bDO017_Closed),
                        D94 = App.OnOffc(!ADS.bDO018),
                        D95 = App.OnOffc(!ADS.bDO018_LSH),
                        D96 = App.OnOffc(!ADS.bDO018_LSL),
                        D97 = App.OnOffc(!ADS.bDO019),
                        D98 = App.OnOffc(!ADS.bDO020),
                        D99 = App.OnOffc(!ADS.bDO021),
                        D100 = App.OnOffc(!ADS.bDO022),                                                                 // 09/20/23 PS
                        D101 = Units.strFormatc(ADS.sAOut007_Out, ADS.AOut007[3]),
                        D102 = Units.strFormatc(ADS.sAOut008_Out, ADS.AOut008[3]),
                        D103 = Units.strFormatc(ADS.sAOut009_Out, ADS.AOut009[3]),
                        D104 = Units.strFormatc(ADS.sAOut010_Out, ADS.AOut010[3]),
                        D105 = GetStep()                                                                                // 08/18/23 PS - Set current action
                    });
                    iLogCount = 0;
                    iCounter++;
                    if (!bReportCreated)
                    {
                        CreateReport();
                    }
                }
                if (iCounter > iCount)
                {
                    AppendReport();
                    iCounter = 0;
                }
            }
            else
            {
                if (bLoggingStarted)
                {
                    if (iCounter > 0)
                    {
                        AppendReport();
                    }
                    bLoggingStarted = false;
                    bReportCreated = false;
                    iCounter = 0;
                    iLogCount = 0;
                    bWriteToCloud = ADS.bProcessData;                                                                       // 08/20/23 PS
                    if (ADS.bCycleLoggingOn)                                                                                // 06/06/23 PS - Write to cloud only if auto cycle is on
                    {
                        if (ADS.iState == (short)State.eIdle)
                        {
                            ADS.bCycleLoggingOn = false;
                        }
                        else
                        {
                            ADS.iLogging = (short)LogType.Cycle;
                        }
                        ADS.bUploading = true;
                        UpdateAutoValues();
                    }
                    else
                    {
                        ADS.iLogging = (short)LogType.No_Log;
                    }
                }
                else
                {
                    ADS.bProcessData = false;                                                                               // 08/20/23 PS
                }
            }
        }

        private string GetStep()
        {
            int iStep = ADS.iCurrentStep;
            string cs = "transition";
            switch (ADS.iState)
            {
                case (int)State.eAdsorption:
                    {
                        if (iStep == 3 || iStep == 4)
                        {
                            cs = "pre_adsorption_flow";
                        }
                        else if (iStep == 5 || iStep == 6)
                        {
                            cs = "adsorption";
                        }
                        break;
                    }

                case (int)State.eEvacuation:
                    {
                        if (iStep == 12)
                        {
                            cs = "evacuation";
                        }
                        break;
                    }

                case (int)State.eSteamBypass:
                    {
                        if (iStep >= 5 || iStep <= 9)
                        {
                            cs = "steam_bypass";
                        }
                        break;
                    }

                case (int)State.eDesorption:
                    {
                        if (iStep >= 8 || iStep <= 16)
                        {
                            cs = "pressurization";
                        }
                        else if (iStep >= 17 || iStep <= 20)
                        {
                            cs = "purge";
                        }
                        else if (iStep >= 21 || iStep <= 23)
                        {
                            cs = "cooling";
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
            return cs;
        }

        private void CreateReport()
        {
            bReportCreated = true;
            string date = DateTime.Now.ToString(@"yyyy_MM_dd HH_mm_ss");
            string _date = DateTime.Now.ToString(@"yyyy_MM_dd_HH_mm_ss");
            //if (ADS.iLogging == (short)LogType.Cycle)
            if (ADS.bProcessData)
            {
                strReport1 = name + "cycle_sensor_data_" + _date + ".csv";
                strReport2 = name + "cycle_metadata_" + _date + ".csv";
                line1 = "reactor_schema,sorbent_cartridge_id,cartridge_cycle_number,cycle_begin_time,reactor_location,notes,";      // 09/19/23 PS
                line1 += "sensor_readouts_filepath,ambient_temperature_c,ambient_pressure_bar,ambient_relative_humidity,";          // 09/19/23 PS
                line1 += "is_pressure_drop_test,post_process_data";                                                                 // 09/19/23 PS
                line2 = ADS.strSchemaName[ADS.iSchemaPointer] + c + ADS.strCartridgeName[ADS.iCartridgePointer] + c;                // 09/19/23 PS
                line2 += ADS.iCartridgeCycles[ADS.iCartridgePointer].ToString() + c + date + c + ADS.strLocation + c;               // 09/19/23 PS
                line2 += ADS.strComment + c + aws1 + strReport1 + c + Units.strFormat(ADS.sAIn043, ADS.AIn043[3]) + c;              // 09/19/23 PS
                line2 += Units.strFormat(ADS.sAIn043, ADS.AIn043[3]) + c + Units.strFormat(ADS.sAIn043, ADS.AIn043[3]) + c;         // 09/19/23 PS
                line2 += ADS.bPressureDropTest.ToString() + c + ADS.bProcessData.ToString();                                        // 09/19/23 PS
                try
                {
                    StreamWriter sw = new StreamWriter(App.strReportsDir + strReport2, false, System.Text.Encoding.UTF8);
                    sw.WriteLine(line1);
                    sw.WriteLine(line2);
                    sw.Close();
                }
                catch (Exception ex)
                {
                    App.WPFMessageBoxOK(ex.Message + " - Meta Report", App.bgRed);
                }
            }
            else
            {
                strReport1 = name + strReportName[ADS.iLogging] + "_" + _date + ".csv";
            }

            line1 = "date time," + ADS.AOut000RPM[0] + c + ADS.AOut000[0] + c + ADS.AIn000[0] + c + ADS.AIn001[0] + c;
            line1 += ADS.AIn002[0] + c + ADS.AIn003[0] + c + ADS.AIn004[0] + c + ADS.AIn005[0] + c + ADS.AIn006[0] + c;
            line1 += ADS.AIn007[0] + c + ADS.AIn008[0] + c + ADS.AIn009[0] + c + ADS.AIn010[0] + c + ADS.AIn011[0] + c;
            line1 += ADS.AIn012[0] + c + ADS.AIn013[0] + c + ADS.AIn014[0] + c + ADS.AIn015[0] + c + ADS.AIn016[0] + c;
            line1 += ADS.AIn017[0] + c + ADS.AIn018[0] + c + ADS.AIn019[0] + c + ADS.AIn020[0] + c + ADS.AIn021[0] + c;
            line1 += ADS.AIn022[0] + c + ADS.AIn023[0] + c + ADS.AIn024[0] + c + ADS.AIn025[0] + c + ADS.AIn026[0] + c;
            line1 += ADS.AIn027[0] + c + ADS.AIn028[0] + c + ADS.AIn040[0] + c + ADS.AIn041[0] + c;
            line1 += ADS.AIn042[0] + c + ADS.AIn043[0] + c + ADS.AIn044[0] + c + ADS.AIn045[0] + c + ADS.AIn046[0] + c;
            line1 += ADS.AIn050[0] + c + ADS.AIn051[0] + c + ADS.AIn052[0] + c + ADS.AIn053[0] + c + ADS.AIn054[0] + c;
            line1 += ADS.AIn055[0] + c + ADS.Temp000[0] + c + ADS.Temp001[0] + c + ADS.Temp002[0] + c + ADS.Temp003[0] + c;
            line1 += ADS.Temp004[0] + c + ADS.Temp005[0] + c + ADS.Temp006[0] + c + ADS.Temp007[0] + c + ADS.Temp008[0] + c;
            line1 += ADS.Temp009[0] + c + ADS.Temp010[0] + c + ADS.Temp011[0] + c + ADS.Temp012[0] + c + ADS.Temp013[0] + c;
            line1 += ADS.Temp014[0] + c + ADS.Temp015[0] + c + ADS.Temp016[0] + c + ADS.Temp017[0] + c + ADS.Temp018[0] + c;
            line1 += ADS.Temp019[0] + c + ADS.Temp020[0] + c + ADS.Temp021[0] + c + ADS.Temp022[0] + c + ADS.Temp023[0] + c;
            line1 += ADS.Temp024[0] + c + ADS.Temp025[0] + c + ADS.Temp026[0] + c;
            line1 += ADS.DO000[0] + c + ADS.DO001[0] + c + ADS.DO002[0] + c + ADS.DO003[0] + c + ADS.DO004[0] + c;
            line1 += ADS.DO005[0] + c + ADS.DO006[0] + c + ADS.DO007[0] + c + ADS.DO007[0] + lsh + ADS.DO007[0] + lsl;
            line1 += ADS.DO008[0] + c + ADS.DO009[0] + c + ADS.DO010[0] + c + ADS.DO011[0] + c + ADS.DO011[0] + lsh;
            line1 += ADS.DO011[0] + lsl + ADS.DO012[0] + c + ADS.DO013[0] + c + ADS.DO014[0] + c + ADS.DO015[0] + c;
            line1 += ADS.DO016[0] + c + ADS.DO017[0] + c + ADS.DO018[0] + c + ADS.DO018[0] + lsh + ADS.DO018[0] + lsl;
            line1 += ADS.DO019[0] + c + ADS.DO020[0] + c + ADS.DO021[0] + c + ADS.DO022[0] + c;                             // 09/20/23 PS
            line1 += ADS.AOut007[0] + c + ADS.AOut008[0] + c + ADS.AOut009[0] + c + ADS.AOut010[0] + c + strState;          // 07/14/23 PS - Add current state to file

            try
            {
                StreamWriter sw = new StreamWriter(App.strReportsDir + strReport1, false, System.Text.Encoding.UTF8);
                sw.WriteLine(line1);
                LogLines(sw);
                sw.Close();
                _LogCollection.Clear();
            }
            catch (Exception ex)
            {
                App.WPFMessageBoxOK(ex.Message + " - Create Report", App.bgRed);
            }
        }

        private void AppendReport()
        {
            try
            {
                using (StreamWriter sw = File.AppendText(App.strReportsDir + strReport1))
                {
                    LogLines(sw);
                    sw.Close();
                }
                _LogCollection.Clear();
            }
            catch (Exception ex)
            {
                App.WPFMessageBoxOK(ex.Message + " - Append Report", App.bgRed);
            }
        }

        private void LogLines(StreamWriter s)
        {
            foreach (LogInfo i in _LogCollection)
            {
                line1 = i.DateTime + i.D1 + i.D2 + i.D3 + i.D4 + i.D5 + i.D6 + i.D7 + i.D8 + i.D9 + i.D10 + i.D11 + i.D12 + i.D13 + i.D14 + i.D15 + i.D16 + i.D17 + i.D18 + i.D19 + i.D20;
                line1 += i.D21 + i.D22 + i.D23 + i.D24 + i.D25 + i.D26 + i.D27 + i.D28 + i.D29 + i.D30 + i.D31 + i.D32 + i.D33 + i.D34 + i.D35 + i.D36 + i.D37 + i.D38 + i.D39 + i.D40;
                line1 += i.D41 + i.D42 + i.D43 + i.D44 + i.D45 + i.D46 + i.D47 + i.D48 + i.D49 + i.D50 + i.D51 + i.D52 + i.D53 + i.D54 + i.D55 + i.D56 + i.D57 + i.D58 + i.D59 + i.D60;
                line1 += i.D61 + i.D62 + i.D63 + i.D64 + i.D65 + i.D66 + i.D67 + i.D68 + i.D69 + i.D70 + i.D71 + i.D72 + i.D73 + i.D74 + i.D75 + i.D76 + i.D77 + i.D78 + i.D79 + i.D80;
                line1 += i.D81 + i.D82 + i.D83 + i.D84 + i.D85 + i.D86 + i.D87 + i.D88 + i.D89 + i.D90 + i.D91 + i.D92 + i.D93 + i.D94 + i.D95 + i.D96 + i.D97 + i.D98 + i.D99 + i.D100;    // 09/19/23 PS
                line1 += i.D101 + i.D102 + i.D103 + i.D104 + i.D105;                                                                                                                        // 09/20/23 PS
                s.WriteLine(line1);
            }
        }

        private void WriteToCloud(string dir, string report)
        {
            string rpt = "s3 cp " + App.strReportsDir + report + " " + dir + report;
            string upload = ExecuteCommand(rpt);
            if (upload.IndexOf("failed") != -1)
            {
                ADS.SetAdsValue(ADS.GVTtgb + "File_Save_Error", true);
            }
        }

        private string ExecuteCommand(string command)
        {
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("aws", command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true
                //RedirectStandardOutput = true
            };

            process = Process.Start(processInfo);
            process.WaitForExit(3000);

            // *** Read the streams *** Warning: This approach can lead to deadlocks
            //string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            //exitCode = process.ExitCode;

            //process.Close();
            process.Dispose();
            return error;
        }

        public class LogInfo
        {
            public string DateTime { get; set; }
            public string D1 { get; set; }
            public string D2 { get; set; }
            public string D3 { get; set; }
            public string D4 { get; set; }
            public string D5 { get; set; }
            public string D6 { get; set; }
            public string D7 { get; set; }
            public string D8 { get; set; }
            public string D9 { get; set; }
            public string D10 { get; set; }
            public string D11 { get; set; }
            public string D12 { get; set; }
            public string D13 { get; set; }
            public string D14 { get; set; }
            public string D15 { get; set; }
            public string D16 { get; set; }
            public string D17 { get; set; }
            public string D18 { get; set; }
            public string D19 { get; set; }
            public string D20 { get; set; }
            public string D21 { get; set; }
            public string D22 { get; set; }
            public string D23 { get; set; }
            public string D24 { get; set; }
            public string D25 { get; set; }
            public string D26 { get; set; }
            public string D27 { get; set; }
            public string D28 { get; set; }
            public string D29 { get; set; }
            public string D30 { get; set; }
            public string D31 { get; set; }
            public string D32 { get; set; }
            public string D33 { get; set; }
            public string D34 { get; set; }
            public string D35 { get; set; }
            public string D36 { get; set; }
            public string D37 { get; set; }
            public string D38 { get; set; }
            public string D39 { get; set; }
            public string D40 { get; set; }
            public string D41 { get; set; }
            public string D42 { get; set; }
            public string D43 { get; set; }
            public string D44 { get; set; }
            public string D45 { get; set; }
            public string D46 { get; set; }
            public string D47 { get; set; }
            public string D48 { get; set; }
            public string D49 { get; set; }
            public string D50 { get; set; }
            public string D51 { get; set; }
            public string D52 { get; set; }
            public string D53 { get; set; }
            public string D54 { get; set; }
            public string D55 { get; set; }
            public string D56 { get; set; }
            public string D57 { get; set; }
            public string D58 { get; set; }
            public string D59 { get; set; }
            public string D60 { get; set; }
            public string D61 { get; set; }
            public string D62 { get; set; }
            public string D63 { get; set; }
            public string D64 { get; set; }
            public string D65 { get; set; }
            public string D66 { get; set; }
            public string D67 { get; set; }
            public string D68 { get; set; }
            public string D69 { get; set; }
            public string D70 { get; set; }
            public string D71 { get; set; }
            public string D72 { get; set; }
            public string D73 { get; set; }
            public string D74 { get; set; }
            public string D75 { get; set; }
            public string D76 { get; set; }
            public string D77 { get; set; }
            public string D78 { get; set; }
            public string D79 { get; set; }
            public string D80 { get; set; }
            public string D81 { get; set; }
            public string D82 { get; set; }
            public string D83 { get; set; }
            public string D84 { get; set; }
            public string D85 { get; set; }
            public string D86 { get; set; }
            public string D87 { get; set; }
            public string D88 { get; set; }
            public string D89 { get; set; }
            public string D90 { get; set; }
            public string D91 { get; set; }
            public string D92 { get; set; }
            public string D93 { get; set; }
            public string D94 { get; set; }
            public string D95 { get; set; }
            public string D96 { get; set; }
            public string D97 { get; set; }
            public string D98 { get; set; }
            public string D99 { get; set; }                                                                                  // 09/19/23 PS
            public string D100 { get; set; }                                                                                 // 09/19/23 PS
            public string D101 { get; set; }                                                                                 // 09/19/23 PS
            public string D102 { get; set; }                                                                                 // 09/19/23 PS
            public string D103 { get; set; }                                                                                 // 09/19/23 PS
            public string D104 { get; set; }                                                                                 // 09/20/23 PS - Add current state to file
            public string D105 { get; set; }                                                                                 // 07/14/23 PS - Add current state to file
        }
    }
}
