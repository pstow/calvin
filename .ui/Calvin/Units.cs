// Carbon Capture - Calvin
// Units.cs
// Rev 1.00 - March 31, 2023

using System;

namespace Calvin
{
    class Units
    {
        public static string[] F = new string[] { "0", "F1", "F2", "F3", "F4", "F5", "F6", "F7" };
        public static string[] strUnits = new string[30];

        public static string UnitString(string[] ud)
        {
            return strUnits[Convert.ToInt32(ud[2])];
        }

        public static string UnitDisplay1(float f, string[] ud)
        {
            string s = strFormat(f, Convert.ToInt32(ud[2])) + " " + strUnits[Convert.ToInt32(ud[1])];
            return s;
        }

        public static string UnitDisplay2(float f, string[] ud)
        {
            string s = strFormat(f, Convert.ToInt32(ud[3])) + "  " + strUnits[Convert.ToInt32(ud[2])];
            return s;
        }

        public static string UnitDisplay3(float f, int d, int u)
        {
            string s = strFormat(f, d) + " " + strUnits[u];
            return s;
        }

        public static string KeyboardTitle(string s, object min, object max, object decimals, object units)
        {
            int d = Convert.ToInt32(decimals);
            int i = Convert.ToInt32(units);
            string u = i == 0 ? "" : " " + strUnits[i];
            return s + "  (" + strFormat(min, d) + " - " + strFormat(max, d) + u + ")";
        }

        public static string strFormat(object value, int decimals)
        {
            double ret = (!string.IsNullOrEmpty(value.ToString())) ? Convert.ToDouble(value) : 0;
            //string f = String.Format("F{0:D}", decimals);
            return ret.ToString(F[decimals]);
        }

        public static string strFormat(object value, string decimals)
        {
            double ret = (!string.IsNullOrEmpty(value.ToString())) ? Convert.ToDouble(value) : 0;
            return ret.ToString(F[Convert.ToInt32(decimals)]);
        }

        public static string strFormatc(object value, string decimals)
        {
            double ret = (!string.IsNullOrEmpty(value.ToString())) ? Convert.ToDouble(value) : 0;
            return ret.ToString(F[Convert.ToInt32(decimals)]) + ",";
        }
    }
}
