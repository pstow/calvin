using System;
using System.Windows;
// Rev 1.00 - July 5, 2023

namespace Calvin.HelpManager
{
    public partial class MessageCommentWindow : Window
    {
        public static int MessageYesNoCancel = (int)MsgResponse.No;
        private static readonly string[] bg = new string[] { "styleWindowGray", "styleWindowSeaGreen", "styleWindowRed" };

        public MessageCommentWindow(string message, int background)
        {
            InitializeComponent();
            tbkText.Text = message;
            btnYes.Content = "Yes";
            btnNo.Content = "No";
            btnOKCancel.Content = "Cancel";
            kpdComment.Title = "cycle comment";
            Style = (Style)FindResource(bg[background]);
        }

        private void kpdComment_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ADS.strComment = kpdComment.Text;
            _ = btnYes.Focus();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            ADS.strComment = "";
            MessageYesNoCancel = (int)MsgResponse.No;
            Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            MessageYesNoCancel = (int)MsgResponse.Yes;
            Close();
        }

        private void btnOKCancel_Click(object sender, RoutedEventArgs e)
        {
            ADS.strComment = "";
            MessageYesNoCancel = (int)MsgResponse.Cancel;
            Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Left = ((SystemParameters.PrimaryScreenWidth - this.ActualWidth) / 2);
            //App.SetCursorPos(400, 160);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                MessageYesNoCancel = (int)MsgResponse.Cancel;
                Close();
            }
        }

    }
}
