// Carbon Capture - Calvin
// SchemaWindow.xaml.cs
// Rev 1.00 - September 19, 2023

using Calvin.ConfigManager;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Calvin.HelpManager
{
    public partial class SchemaWindow : Window
    {
        private readonly bool bRun;

        public SchemaWindow()
        {
            InitializeComponent();
            kpdSchema.Text = "";
            kpdSchema.Title = "Schema name";
            bRun = true;
        }

        private void kpdSchema_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bRun)
            {
                btnSave.IsEnabled = true;
                btnSave.Foreground = Brushes.Black;
                btnSave.Background = Brushes.LightBlue;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ADS.strSchema = kpdSchema.Text;
            ADS.bSchemaSaved = true;
            Config.SaveUserFile();
            Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            _ = labFocus.Focus();
        }

    }
}
