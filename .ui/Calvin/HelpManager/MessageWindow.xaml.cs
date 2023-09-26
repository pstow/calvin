using System;
using System.Windows;
// Rev 1.00 - July 5, 2023

namespace Calvin.HelpManager
{
    public partial class MessageWindow : Window
    {
        public static bool MessageYesNo = false;
        public static int MessageYesNoCancel = (int)MsgResponse.No;
        private static string[] bg = new string[] { "styleWindowGray", "styleWindowSeaGreen", "styleWindowRed" };
        private static bool bOkOn;

        public MessageWindow(string message, int buttons, int background)
        {
            InitializeComponent();
            tbkText.Text = message;
            btnYes.Content = "Yes";
            btnNo.Content = "No";
            Style = (Style)FindResource(bg[background]);

            if (buttons == (int)MsgButtons.OK)
            {
                btnOKCancel.Content = "OK";
                bOkOn = true;
                btnNo.Visibility = Visibility.Hidden;
                btnYes.Visibility = Visibility.Hidden;
            }
            else if (buttons == (int)MsgButtons.YesNo)
            {
                GridLengthConverter glc = new GridLengthConverter();
                MWCol03.Width = (GridLength)glc.ConvertFromString("30");
                btnOKCancel.Visibility = Visibility.Hidden;
            }
            else if (buttons == (int)MsgButtons.YesNoCancel)
            {
                btnOKCancel.Content = "Cancel";
            }
            Visibility = Visibility.Visible;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            MessageYesNo = false;
            MessageYesNoCancel = (int)MsgResponse.No;
            Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            MessageYesNo = true;
            MessageYesNoCancel = (int)MsgResponse.Yes;
            Close();
        }

        private void btnOKCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageYesNoCancel = (int)MsgResponse.Cancel;
            Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Left = ((SystemParameters.PrimaryScreenWidth - this.ActualWidth) / 2);
            //App.SetCursorPos(400, 160);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)                                     // 11/11/2021
        {
            if (bOkOn && e.Key.ToString() == "Return")
            {
                MessageYesNoCancel = (int)MsgResponse.Cancel;
                Close();
            }
        }
    }
}
