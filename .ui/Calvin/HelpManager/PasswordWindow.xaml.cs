// Carbon Capture - Calvin
// PasswordWindow.xaml.cs
// Rev 1.00 - June 9, 2023

using Calvin.ConfigManager;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Calvin.HelpManager
{
    public partial class PasswordWindow : Window
    {
        private string pw = "";
        private string stars = "";

        public PasswordWindow()
        {
            InitializeComponent();
            kpdPassword.Text = "";
            kpdPassword.Title = "Email password";
        }

        private void kpdPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(kpdPassword.PasswordText))
            {
                pw = kpdPassword.PasswordText;
                stars = "***********";
                btnSave.IsEnabled = true;
                btnSave.Foreground = Brushes.Black;
                btnSave.Background = Brushes.LightBlue;
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            ADS.bDisplayPassword = !ADS.bDisplayPassword;
            kpdPassword.Text = ADS.bDisplayPassword ? pw : stars;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ADS.strPassword = kpdPassword.PasswordText;
            Config.SaveUserFile();
            Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            ADS.bDisplayPassword = false;
            Close();
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            _ = labFocus.Focus();
        }

    }
}
