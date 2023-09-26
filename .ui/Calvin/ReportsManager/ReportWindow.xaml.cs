// Carbon Capture - Calvin
// ReportWindow.xaml.cs
// Rev 1.00 - July 5, 2023

using System;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.IO;
using System.Windows;

namespace Calvin.ReportsManager
{

    public partial class ReportWindow : Window
    {
        public static string[] db = new string[35];
        private static readonly ObservableCollection<LogInfo> _LogCollection = new ObservableCollection<LogInfo>();
        public ObservableCollection<LogInfo> LogCollection => _LogCollection;

        public ReportWindow(int count)
        {
            InitializeComponent();
            LoadConfiguration();
            ReadData(Reports.strQuery, count);
            DataContext = this;
        }

        private static void ReadData(string dbinfo, int count)
        {
            using (OleDbConnection UserDB = new OleDbConnection(App.strJet + App.strDB))
            {
                OleDbCommand command = new OleDbCommand(dbinfo, UserDB);
                try
                {
                    UserDB.Open();
                }
                catch (Exception ex)
                {
                    App.WPFMessageBoxOK(ex.Message + " - " + Reports.strTitle, App.bgRed);
                    return;
                }
                _LogCollection.Clear();
                try
                {
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < count; i++)
                        {
                            db[i] = reader[i].ToString();   // GetColumnInfo(reader[i], Reports.iUnits[i]);
                        }
                        _LogCollection.Add(new LogInfo
                        {
                            Column00 = db[0],
                            Column01 = db[1],
                            Column02 = db[2],
                            Column03 = db[3],
                            Column04 = db[4],
                            Column05 = db[5],
                            Column06 = db[6],
                            Column07 = db[7],
                            Column08 = db[8],
                            Column09 = db[9],
                            Column10 = db[10],
                            Column11 = db[11],
                            Column12 = db[12],
                            Column13 = db[13],
                            Column14 = db[14],
                            Column15 = db[15],
                            Column16 = db[16],
                            Column17 = db[17],
                            Column18 = db[18],
                            Column19 = db[19],
                            Column20 = db[20],
                            Column21 = db[21],
                            Column22 = db[22],
                            Column23 = db[23],
                            Column24 = db[24],
                            Column25 = db[25],
                            Column26 = db[26],
                            Column27 = db[27],
                            Column28 = db[28],
                            Column29 = db[29],
                            Column30 = db[30],
                            Column31 = db[31],
                            Column32 = db[32],
                            Column33 = db[33],
                            Column34 = db[34]
                        }
                        );
                    }
                }
                catch (Exception ex)
                {
                    App.WPFMessageBoxOK(ex.Message + " - " + Reports.strTitle, App.bgRed);
                }
                UserDB.Close();
            }
        }

        public static string GetColumnInfo(object rdr, int units)
        {
            return rdr.ToString();
            /*              switch (units)
                        {
                            case (int)UnitType.String:
                                return rdr.ToString();
                            case (int)UnitType.strF0:
                                return Units.strFormat(rdr, 0);
                            case (int)UnitType.strF1:
                                return Units.strFormat(rdr, 1);
                            case (int)UnitType.strF2:
                                return Units.strFormat(rdr, 2);
                            case (int)UnitType.level:
                                {
                                    string strLev = User.strLOGON_LEVEL[Convert.ToInt16(rdr)];
                                    strLev = strLev == "" ? Lang.strSystem : strLev;
                                    return strLev;
                                }
                            case (int)UnitType.strDFtoDC10:
                                return Units.strDFtoDC(rdr, 1, 0);
                            case (int)UnitType.strDFtoDC11:
                                return Units.strDFtoDC(rdr, 1, 1);
                            case (int)UnitType.strDFtoDC21:
                                return Units.strDFtoDC(rdr, 2, 1);
                            case (int)UnitType.strFtoC10:
                                return Units.strFtoC(rdr, 1, 0);
                            case (int)UnitType.strFtoC21:
                                return Units.strFtoC(rdr, 2, 1);
                            case (int)UnitType.strPtoU20:
                                return Units.strPtoU(rdr, 2, 0);
                            case (int)UnitType.strPtoU21:
                                return Units.strPtoU(rdr, 2, 1);
                            case (int)UnitType.strRptPtoU:
                                return Units.strRptPtoU(rdr);
                            case (int)UnitType.strItoU:
                                return Units.strItoU(rdr);
                            case (int)UnitType.strItoTF0:
                                return Units.strItoTF0(rdr);
                            case (int)UnitType.strItoTF1:
                                return Units.strItoTF1(rdr);
                            case (int)UnitType.maintenance:                                                                             // Rev 01
                                {
                                    short mnt = Convert.ToInt16(rdr);
                                    return mnt == 0 ? Lang.strTravel : mnt == 1 ? Lang.UpperInit(Lang.str_cycles) : Lang.strTime;
                                }
                            case (int)UnitType.truefalse:
                                return Lang.GetTrueFalse(Convert.ToString(rdr));
                            case (int)UnitType.paresepriv:
                                return Reports.ParsePrivilege(Convert.ToInt32(rdr));
                            default:
                                return rdr.ToString();
                        }   */
        }

        public class LogInfo
        {
            public string Column00 { get; set; }
            public string Column01 { get; set; }
            public string Column02 { get; set; }
            public string Column03 { get; set; }
            public string Column04 { get; set; }
            public string Column05 { get; set; }
            public string Column06 { get; set; }
            public string Column07 { get; set; }
            public string Column08 { get; set; }
            public string Column09 { get; set; }
            public string Column10 { get; set; }
            public string Column11 { get; set; }
            public string Column12 { get; set; }
            public string Column13 { get; set; }
            public string Column14 { get; set; }
            public string Column15 { get; set; }
            public string Column16 { get; set; }
            public string Column17 { get; set; }
            public string Column18 { get; set; }
            public string Column19 { get; set; }
            public string Column20 { get; set; }
            public string Column21 { get; set; }
            public string Column22 { get; set; }
            public string Column23 { get; set; }
            public string Column24 { get; set; }
            public string Column25 { get; set; }
            public string Column26 { get; set; }
            public string Column27 { get; set; }
            public string Column28 { get; set; }
            public string Column29 { get; set; }
            public string Column30 { get; set; }
            public string Column31 { get; set; }
            public string Column32 { get; set; }
            public string Column33 { get; set; }
            public string Column34 { get; set; }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            tbkSaveMessage.Text = "strSavingData - strPleaseWait";
            SaveReportData();
            btnSave.IsEnabled = false;
        }

        private void SaveReportData()
        {
            string c = ", ";
            try
            {
                StreamWriter sw = new StreamWriter(App.strDir + Reports.strTitle + ".csv", false);
                string line = Reports.strHeadings[0];
                for (int i = 1; i < 35; i++)
                {
                    line = Reports.bActive[i] ? line + c + Reports.strHeadings[i] : line;
                }
                sw.WriteLine(line);
                foreach (LogInfo i in _LogCollection)
                {
                    line = i.Column00;
                    line = Reports.bActive[1] ? line + c + i.Column01 : line;
                    line = Reports.bActive[2] ? line + c + i.Column02 : line;
                    line = Reports.bActive[3] ? line + c + i.Column03 : line;
                    line = Reports.bActive[4] ? line + c + i.Column04 : line;
                    line = Reports.bActive[5] ? line + c + i.Column05 : line;
                    line = Reports.bActive[6] ? line + c + i.Column06 : line;
                    line = Reports.bActive[7] ? line + c + i.Column07 : line;
                    line = Reports.bActive[8] ? line + c + i.Column08 : line;
                    line = Reports.bActive[9] ? line + c + i.Column09 : line;
                    line = Reports.bActive[10] ? line + c + i.Column10 : line;
                    line = Reports.bActive[11] ? line + c + i.Column11 : line;
                    line = Reports.bActive[12] ? line + c + i.Column12 : line;
                    line = Reports.bActive[13] ? line + c + i.Column13 : line;
                    line = Reports.bActive[14] ? line + c + i.Column14 : line;
                    line = Reports.bActive[15] ? line + c + i.Column15 : line;
                    line = Reports.bActive[16] ? line + c + i.Column16 : line;
                    line = Reports.bActive[17] ? line + c + i.Column17 : line;
                    line = Reports.bActive[18] ? line + c + i.Column18 : line;
                    line = Reports.bActive[19] ? line + c + i.Column19 : line;
                    line = Reports.bActive[20] ? line + c + i.Column20 : line;
                    line = Reports.bActive[21] ? line + c + i.Column21 : line;
                    line = Reports.bActive[22] ? line + c + i.Column22 : line;
                    line = Reports.bActive[23] ? line + c + i.Column23 : line;
                    line = Reports.bActive[24] ? line + c + i.Column24 : line;
                    line = Reports.bActive[25] ? line + c + i.Column25 : line;
                    line = Reports.bActive[26] ? line + c + i.Column26 : line;
                    line = Reports.bActive[27] ? line + c + i.Column27 : line;
                    line = Reports.bActive[28] ? line + c + i.Column28 : line;
                    line = Reports.bActive[29] ? line + c + i.Column29 : line;
                    line = Reports.bActive[30] ? line + c + i.Column30 : line;
                    line = Reports.bActive[31] ? line + c + i.Column31 : line;
                    line = Reports.bActive[32] ? line + c + i.Column32 : line;
                    line = Reports.bActive[33] ? line + c + i.Column33 : line;
                    line = Reports.bActive[34] ? line + c + i.Column34 : line;

                    sw.WriteLine(line);
                }
                sw.Close();
                tbkSaveMessage.Text = "strDataFileSaved";
            }
            catch (Exception ex)
            {
                App.WPFMessageBoxOK(ex.Message + " - " + Reports.strTitle, App.bgRed);
                tbkSaveMessage.Text = "";
            }
        }

        private void LoadConfiguration()
        {
            labTitle.Content = Reports.strTitle;
            btnClose.Content = "Close";
            btnSave.Content = "strSave";
            dgtColumn00.Header = Reports.strHeadings[0];
            dgtColumn01.Header = Reports.strHeadings[1];
            dgtColumn02.Header = Reports.strHeadings[2];
            dgtColumn03.Header = Reports.strHeadings[3];
            dgtColumn04.Header = Reports.strHeadings[4];
            dgtColumn05.Header = Reports.strHeadings[5];
            dgtColumn06.Header = Reports.strHeadings[6];
            dgtColumn07.Header = Reports.strHeadings[7];
            dgtColumn08.Header = Reports.strHeadings[8];
            dgtColumn09.Header = Reports.strHeadings[9];
            dgtColumn10.Header = Reports.strHeadings[10];
            dgtColumn11.Header = Reports.strHeadings[11];
            dgtColumn12.Header = Reports.strHeadings[12];
            dgtColumn13.Header = Reports.strHeadings[13];
            dgtColumn14.Header = Reports.strHeadings[14];
            dgtColumn15.Header = Reports.strHeadings[15];
            dgtColumn16.Header = Reports.strHeadings[16];
            dgtColumn17.Header = Reports.strHeadings[17];
            dgtColumn18.Header = Reports.strHeadings[18];
            dgtColumn19.Header = Reports.strHeadings[19];
            dgtColumn20.Header = Reports.strHeadings[20];
            dgtColumn21.Header = Reports.strHeadings[21];
            dgtColumn22.Header = Reports.strHeadings[22];
            dgtColumn23.Header = Reports.strHeadings[23];
            dgtColumn24.Header = Reports.strHeadings[24];
            dgtColumn25.Header = Reports.strHeadings[25];
            dgtColumn26.Header = Reports.strHeadings[26];
            dgtColumn27.Header = Reports.strHeadings[27];
            dgtColumn28.Header = Reports.strHeadings[28];
            dgtColumn29.Header = Reports.strHeadings[29];
            dgtColumn30.Header = Reports.strHeadings[30];
            dgtColumn31.Header = Reports.strHeadings[31];
            dgtColumn32.Header = Reports.strHeadings[32];
            dgtColumn33.Header = Reports.strHeadings[33];
            dgtColumn34.Header = Reports.strHeadings[34];

            dgtColumn03.Visibility = Reports.bActive[3] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn04.Visibility = Reports.bActive[4] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn05.Visibility = Reports.bActive[5] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn06.Visibility = Reports.bActive[6] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn07.Visibility = Reports.bActive[7] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn08.Visibility = Reports.bActive[8] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn09.Visibility = Reports.bActive[9] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn10.Visibility = Reports.bActive[10] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn11.Visibility = Reports.bActive[11] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn12.Visibility = Reports.bActive[12] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn13.Visibility = Reports.bActive[13] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn14.Visibility = Reports.bActive[14] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn15.Visibility = Reports.bActive[15] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn16.Visibility = Reports.bActive[16] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn17.Visibility = Reports.bActive[17] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn18.Visibility = Reports.bActive[18] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn19.Visibility = Reports.bActive[19] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn20.Visibility = Reports.bActive[20] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn21.Visibility = Reports.bActive[21] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn22.Visibility = Reports.bActive[22] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn23.Visibility = Reports.bActive[23] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn24.Visibility = Reports.bActive[24] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn25.Visibility = Reports.bActive[25] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn26.Visibility = Reports.bActive[26] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn27.Visibility = Reports.bActive[27] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn28.Visibility = Reports.bActive[28] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn29.Visibility = Reports.bActive[29] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn30.Visibility = Reports.bActive[30] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn31.Visibility = Reports.bActive[31] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn32.Visibility = Reports.bActive[32] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn33.Visibility = Reports.bActive[33] ? Visibility.Visible : Visibility.Collapsed;
            dgtColumn34.Visibility = Reports.bActive[34] ? Visibility.Visible : Visibility.Collapsed;

            for (int i = 0; i < 35; i++)
            {
                db[i] = "";
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
