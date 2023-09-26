// Carbon Capture - Calvin
// EmailListWindow.xaml.cs
// Rev 1.00 - June 11, 2023

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Calvin.ConfigManager;


namespace Calvin.HelpManager
{
    public partial class EmailListWindow : Window
    {
        private readonly string strEmail1;
        private readonly string strEmail2;
        private readonly string strEmail3;
        private readonly string strEmail4;
        private readonly string strEmail5;
        private readonly bool bRun;
        private bool bChanged;
        private bool bDelete1;
        private bool bDelete2;
        private bool bDelete3;
        private bool bDelete4;
        private bool bDelete5;

        public EmailListWindow()
        {
            InitializeComponent();
            kpdEmail1.Title = "Email 1";
            strEmail1 = ADS.strEmail[0];
            tbkEmail1.Text = strEmail1;
            kpdEmail2.Title = "Email 2";
            strEmail2 = ADS.strEmail[1];
            tbkEmail2.Text = strEmail2;
            kpdEmail3.Title = "Email 3";
            strEmail3 = ADS.strEmail[2];
            tbkEmail3.Text = strEmail3;
            kpdEmail4.Title = "Email 4";
            strEmail4 = ADS.strEmail[3];
            tbkEmail4.Text = strEmail4;
            kpdEmail5.Title = "Email 5";
            strEmail5 = ADS.strEmail[4];
            tbkEmail5.Text = strEmail5;
            bRun = true;
        }

        private void kpdEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bRun)
            {
                bChanged = true;
                EnableSave();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(kpdEmail1.Text))
            {
                ADS.strEmail[0] = kpdEmail1.Text;
                tbkEmail1.Text = kpdEmail1.Text;
            }
            else if (bDelete1)
            {
                ADS.strEmail[0] = "";
            }
            if (!string.IsNullOrEmpty(kpdEmail2.Text))
            {
                ADS.strEmail[1] = kpdEmail2.Text;
                tbkEmail2.Text = kpdEmail2.Text;
            }
            else if (bDelete2)
            {
                ADS.strEmail[1] = "";
            }
            if (!string.IsNullOrEmpty(kpdEmail3.Text))
            {
                ADS.strEmail[2] = kpdEmail3.Text;
                tbkEmail3.Text = kpdEmail3.Text;
            }
            else if (bDelete3)
            {
                ADS.strEmail[2] = "";
            }
            if (!string.IsNullOrEmpty(kpdEmail4.Text))
            {
                ADS.strEmail[3] = kpdEmail4.Text;
                tbkEmail4.Text = kpdEmail4.Text;
            }
            else if (bDelete4)
            {
                ADS.strEmail[3] = "";
            }
            if (!string.IsNullOrEmpty(kpdEmail5.Text))
            {
                ADS.strEmail[4] = kpdEmail5.Text;
                tbkEmail5.Text = kpdEmail5.Text;
            }
            else if (bDelete5)
            {
                ADS.strEmail[4] = "";
            }
            SortEmail();
            Config.SaveUserFile();
            Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEmail1_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(strEmail1))
            {
                bDelete1 = !bDelete1;
                tbkEmail1.Text = bDelete1 ? "" : strEmail1;
                EnableSave();
            }
        }

        private void btnEmail2_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(strEmail2))
            {
                bDelete2 = !bDelete2;
                tbkEmail2.Text = bDelete2 ? "" : strEmail2;
                EnableSave();
            }
        }

        private void btnEmail3_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(strEmail3))
            {
                bDelete3 = !bDelete3;
                tbkEmail3.Text = bDelete3 ? "" : strEmail3;
                EnableSave();
            }
        }

        private void btnEmail4_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(strEmail4))
            {
                bDelete4 = !bDelete4;
                tbkEmail4.Text = bDelete4 ? "" : strEmail4;
                EnableSave();
            }
        }

        private void btnEmail5_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(strEmail5))
            {
                bDelete5 = !bDelete5;
                tbkEmail5.Text = bDelete5 ? "" : strEmail5;
                EnableSave();
            }
        }

        private void EnableSave()
        {
            if (bChanged || bDelete1 || bDelete2 || bDelete3 || bDelete4 || bDelete5)
            {
                btnSave.IsEnabled = true;
                btnSave.Foreground = Brushes.Black;
                btnSave.Background = Brushes.LightBlue;
            }
            else
            {
                btnSave.IsEnabled = false;
                btnSave.Foreground = Brushes.Gray;
                btnSave.Background = Brushes.LightGray;
            }
        }

        private void SortEmail()
        {
            int i;
            int j = 0;
            for (i = 0; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(ADS.strEmail[i]))
                {
                    ADS.strEmail[j] = ADS.strEmail[i];
                    ADS.strEmail[i] = i == j ? ADS.strEmail[i] : "";
                    j++;
                }
            }
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            _ = labFocus.Focus();
        }

    }
}
