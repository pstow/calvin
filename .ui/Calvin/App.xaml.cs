// Carbon Capture - Calvin
// App.xaml.cs
// Rev 1.00 - September 19, 2023

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Calvin.HelpManager;

namespace Calvin
{
    public partial class App : Application
    {
        public static int iCommCheckTime = 1000;
        public static string strDir = "C:/CarbonCapture/";
        public static string strUser = "Calvin";
        public static string strReportsDir = strDir + "Reports/";
        public static string strUserFile = strDir + "CalvinUser_00.xml";
        public static string strPointFile = strDir + "CalvinPoints_00.xml";
        public static string strCartridgeFile = strDir + "CalvinCartridges_00.xml";
        public static string strSchemaFile = strDir + "CalvinSchemas_00.xml";
        public static string strDB = strDir + "CalvinData_00.mdb";
        public static string strCompactDB = strDir + "CompactDB.mdb";
        public static string strExportDrive = "D:\\";
        public static string strJet = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
        //public static string strJet = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";

        public static bool bSave;
        public static bool bMouseReset;
        public static bool bKpdCheckOn;
        public static bool bColorsLoaded;
        public static int bgGray = 0;
        public static int bgYellow = 1;
        public static int bgRed = 2;
        public const string strOn = "On";
        public const string strOff = "Off";
        public const string strOpen = "Open";
        public const string strClose = "Close";
        public const string strClosed = "Closed";
        public const string strTransit = "Transit";
        public const string strStart = "Start";
        public const string strStop = "Stop";
        public const string strEnable = "Enable";
        public const string strDisable = "Disable";

        [DllImport("User32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        public static void ClearButtonTouch()
        {
            _ = SetCursorPos(400, 80);
        }

        public static SolidColorBrush GetPromptColor(int color)
        {
            return color == 1 ? Brushes.Blue : color == 2 ? Brushes.Orange : color == 3 ? Brushes.Black : Brushes.Green;
        }

        public static SolidColorBrush ValveStatus(bool open, bool closed)
        {
            return open ? Brushes.Green : closed ? Brushes.Blue : Brushes.Orange;
        }

        public static SolidColorBrush LabelStatus(bool alarm, bool ready)
        {
            return alarm ? Brushes.Red : ready ? Brushes.Green : Brushes.Blue;
        }

        public static SolidColorBrush ActiveState(bool engaged)
        {
            return engaged ? Brushes.Green : Brushes.Black;
        }

        public static SolidColorBrush WarmupState(bool complete, bool engaged)
        {
            return complete ? Brushes.Gray : engaged ? Brushes.Green : Brushes.Black;
        }

        public static SolidColorBrush TempError(bool error)
        {
            return error ? Brushes.Red : Brushes.Black;
        }

        public static SolidColorBrush ForegroundOnOff(bool stop)
        {
            return stop ? Brushes.Green : Brushes.Blue;
        }

        public static SolidColorBrush ElpStatus(bool on, bool off)
        {
            return on && off ? Brushes.Red : on ? Brushes.Green : off ? Brushes.Blue : Brushes.Orange;
        }

        public static SolidColorBrush ElpFill(bool on)
        {
            return on ? Brushes.Green : Brushes.LightBlue;
        }

        public static Visibility ButtonVisibility(bool hidden)
        {
            return hidden ? Visibility.Hidden : Visibility.Visible;
        }

        public static string OpenCloseTransit(bool open, bool closed)
        {
            return open ? strOpen : closed ? strClose : strTransit;
        }

        public static string OpenCloseTransitc(bool open, bool closed)
        {
            return open ? strOpen + "," : closed ? strClose + "," : strTransit + ",";
        }

        public static string OnOff(bool on)
        {
            return on ? strOff : strOn;
        }

        public static string OnOffc(bool off)
        {
            return off ? strOff + "," : strOn + ",";
        }

        public static void KpdBoldBlue(ref Touch.TextBoxKeyboard tbox, bool bold)
        {
            if (bold)
            {
                tbox.Foreground = Brushes.Blue;
                tbox.FontWeight = FontWeights.Bold;
            }
            else
            {
                tbox.Foreground = Brushes.Black;
                tbox.FontWeight = FontWeights.Normal;
            }
        }

        public static void TbkBoldBlue(ref TextBlock tblock, bool bold)
        {
            if (bold)
            {
                tblock.Foreground = Brushes.Blue;
                tblock.FontSize = 16;
            }
            else
            {
                tblock.Foreground = Brushes.Black;
                tblock.FontSize = 14;
            }
        }

        public static void ButtonEnable(ref Button button, bool enabled)
        {
            if (enabled)
            {
                button.Foreground = Brushes.Black;
                button.Background = Brushes.LightBlue;
            }
            else
            {
                button.Foreground = Brushes.Gray;
                button.Background = Brushes.LightGray;
            }
            button.IsEnabled = enabled;
        }

        public static void ButtonBlinkEnable(ref Button button, bool enabled)
        {
            if (enabled && ADS.bHalfSecPulse)
            {
                button.Foreground = Brushes.Black;
                button.Background = Brushes.Orange;
            }
            else
            {
                button.Foreground = Brushes.Gray;
                button.Background = Brushes.LightGray;
            }
            button.IsEnabled = enabled;
        }

        public static void ButtonStopStartEnable(ref Button button, bool enabled, bool stop)
        {
            if (enabled)
            {
                button.Foreground = Brushes.Black;
                button.Background = Brushes.LightBlue;
            }
            else
            {
                button.Foreground = Brushes.Gray;
                button.Background = Brushes.LightGray;
            }
            button.IsEnabled = enabled;
            button.Content = stop ? strStop : strStart;
        }

        public static void ButtonOffOnEnable(ref Button button, bool enabled, bool off)
        {
            if (enabled)
            {
                button.Foreground = Brushes.Black;
                button.Background = Brushes.LightBlue;
            }
            else
            {
                button.Foreground = Brushes.Gray;
                button.Background = Brushes.LightGray;
            }
            button.IsEnabled = enabled;
            button.Content = off ? strOff : strOn;
        }

        public static void ButtonOpenCloseEnable(ref Button button, bool enabled, bool close)
        {
            if (enabled)
            {
                button.Foreground = Brushes.Black;
                button.Background = Brushes.LightBlue;
            }
            else
            {
                button.Foreground = Brushes.Gray;
                button.Background = Brushes.LightGray;
            }
            button.IsEnabled = enabled;
            button.Content = close ? strClose : strOpen;
        }

        public static void ButtonContentEnable(ref Button button, bool enabled, bool on, string onstring, string offstring)
        {
            if (enabled)
            {
                button.Foreground = Brushes.Black;
                button.Background = Brushes.LightBlue;
            }
            else
            {
                button.Foreground = Brushes.Gray;
                button.Background = Brushes.LightGray;
            }
            button.IsEnabled = enabled;
            button.Content = on ? onstring : offstring;
        }

        public static bool Test_MinMax(string name, object units, string text, object min, object max, object decimals)
        {
            int i = Convert.ToInt32(units);
            return ValidEntryValue(name, Units.strUnits[i], text, min, max, Convert.ToInt32(decimals));
        }

        public static bool Test_MinMaxDateTime(string name, string lbl, string text, float min, float max, int decimals)
        {
            return ValidEntryValue(name, lbl, text, min, max, decimals);
        }

        public static void WPFMessageBoxOK(string message, int background)
        {
            Window WinMessage = new MessageWindow(message, (int)MsgButtons.OK, background);
            _ = WinMessage.ShowDialog();
        }

        public static bool WPFMessageBox_OK(string message, int background)
        {
            Window WinMessage = new MessageWindow(message, (int)MsgButtons.OK, background);
            _ = WinMessage.ShowDialog();
            return true;
        }

        public static bool WPFMessageBoxYesNo(string message, int background)
        {
            Window WinMessage = new MessageWindow(message + "?", (int)MsgButtons.YesNo, background);
            _ = WinMessage.ShowDialog();
            return MessageWindow.MessageYesNo;
        }

        public static int WPFMessageBoxYesNoCancel(string message, int background)
        {
            Window WinMessage = new MessageWindow(message + "?", (int)MsgButtons.YesNoCancel, background);
            _ = WinMessage.ShowDialog();
            return MessageWindow.MessageYesNoCancel;
        }

        public static int WPFMessageCommentBoxYesNoCancel(string message, int background)
        {
            Window WinMessage = new MessageCommentWindow(message + "?", background);
            _ = WinMessage.ShowDialog();
            return MessageCommentWindow.MessageYesNoCancel;
        }

        public static string DateAndTime()
        {
            return string.Format("{0:MM/dd/yyyy HH:mm:ss}", DateTime.Now);
        }

        /// <summary>
        /// Checks if "value" is between "min" and "max" values.  Returns true if value within boundries.
        /// Displays error and returns false if outside of boundries.
        /// </summary>
        /// <param name="label">Name of item being validated</param>
        /// <param name="units">Units of item being validated</param>
        /// <param name="text">New value to be tested</param>
        /// <param name="min">Minimum value allowed</param>
        /// <param name="max">Maximum value allowed</param>
        /// <returns>bValid true if in range, bValid false if out of range</returns>
        public static bool ValidEntryValue(string label, string units, object value, object min, object max, int dec)
        {
            float val = Convert.ToSingle(value);
            float sMin = Convert.ToSingle(min);
            float sMax = Convert.ToSingle(max);
            bool bValid = ((val <= sMax) && (val >= sMin)) ? true : false;
            if (!bValid)
            {
                bSave = false;
                string mn = Units.strFormat(min, dec);
                string mx = Units.strFormat(max, dec);
                WPFMessageBoxOK(label + " must be between " + mn + " and " + mx + " " + units + "!", bgYellow);
            }
            else
            {
                bSave = true;
            }
            return bValid;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(Window), UIElement.PreviewMouseDownEvent, new MouseButtonEventHandler(OnPreviewMouseDown));
            base.OnStartup(e);
        }

        private static void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            bMouseReset = true;
        }

        public static void CloseAllWindows()
        {
            for (int intCounter = Current.Windows.Count - 1; intCounter >= 1; intCounter--)
            {
                Current.Windows[intCounter].Close();
            }
        }

    }
}
