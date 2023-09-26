// Carbon Capture - Calvin
// Reports.cs
// Rev 1.00 - July 5, 2023

using System.Windows;

namespace Calvin.ReportsManager
{
    public class Reports
    {
        private static string db;
        public static string strTitle;
        public static string strQuery;
        public static int iCount;
        public static int[] iUnits = new int[35];
        private static string[] qry = new string[35];
        public static string[] strHeadings = new string[35];
        public static bool[] bActive = new bool[35];

        public static void MachineErrorsReport()
        {
            iCount = 1;
            ClearArray(iCount);
            strTitle = "Errors Report";
            strHeadings[3] = "Error";
            db = "ErrorLog";
            qry[3] = "dbError";
            CreateReport(iCount);
        }


        private static void ClearArray(int count)
        {
            strHeadings[0] = "Date";
            strHeadings[1] = "User";
            strHeadings[2] = "Level";
            for (int i = 3; i < 35; i++)
            {
                strHeadings[i] = "";
                bActive[i] = i < count + 3;
            }
        }

        private static void CreateReport(int count)
        {
            count += 3;
            strQuery = "SELECT dbDate, dbUser, dbUserLevel";
            for (int i = 3; i < count; i++)
            {
                strQuery += ", " + qry[i];
            }
            strQuery += " FROM " + db + " ORDER BY dbDate DESC";
            Window rpt = new ReportWindow(count);
            _ = rpt.ShowDialog();
        }

    }
}

