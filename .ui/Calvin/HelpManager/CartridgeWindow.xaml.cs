// Carbon Capture - Calvin
// CartridgeWindow.xaml.cs
// Rev 1.00 - June 9, 2023

using Calvin.ConfigManager;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Calvin.HelpManager
{
    public partial class CartridgeWindow : Window
    {
        private readonly bool bRun;

        public CartridgeWindow()
        {
            InitializeComponent();
            kpdCartridge.Text = "";
            kpdCartridge.Title = "Cartridge name";
            bRun = true;
        }

        private void kpdCartridge_TextChanged(object sender, TextChangedEventArgs e)
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
            ADS.strCartridge = kpdCartridge.Text;
            ADS.bCartridgeSaved = true;
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
