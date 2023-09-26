// Carbon Capture - Calvin
// GraphConfig.xaml.cs
// Rev 1.00 - May 4, 2023

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Calvin.ConfigManager
{
    public partial class GraphConfig : Window
    {
        private string[] strGraphPoint = new string[6];
        private int[] iColors = new int[6];
        private float[] sGraphLow = new float[6];
        private float[] sGraphHigh = new float[6];
        private bool[] bColorLoaded = new bool[6];
        private bool bColorsLoaded = false;
        private bool bKpdCheck = false;
        private bool bFirstLoad = true;
        private ComboBox cb;
        private TextBox tb;
        public List<Brush> ColorList { get; set; }
        public Brush SelectedColor { get; set; }

        public GraphConfig()
        {
            InitializeComponent();
            LoadConfiguration();
        }

        private void LoadcmbColors()
        {
            cmbColor0.SelectedIndex = ADS.iColors[0];
            cmbColor1.SelectedIndex = ADS.iColors[1];
            cmbColor2.SelectedIndex = ADS.iColors[2];
            cmbColor3.SelectedIndex = ADS.iColors[3];
            cmbColor4.SelectedIndex = ADS.iColors[4];
            cmbColor5.SelectedIndex = ADS.iColors[5];
        }

        private void LoadKeypads()
        {
            bKpdCheck = false;
            kpdGraph0_Low.Text = sGraphLow[0].ToString("F2");
            kpdGraph0_High.Text = sGraphHigh[0].ToString("F2");
            kpdGraph1_Low.Text = sGraphLow[1].ToString("F2");
            kpdGraph1_High.Text = sGraphHigh[1].ToString("F2");
            kpdGraph2_Low.Text = sGraphLow[2].ToString("F2");
            kpdGraph2_High.Text = sGraphHigh[2].ToString("F2");
            kpdGraph3_Low.Text = sGraphLow[3].ToString("F2");
            kpdGraph3_High.Text = sGraphHigh[3].ToString("F2");
            kpdGraph4_Low.Text = sGraphLow[4].ToString("F2");
            kpdGraph4_High.Text = sGraphHigh[4].ToString("F2");
            kpdGraph5_Low.Text = sGraphLow[5].ToString("F2");
            kpdGraph5_High.Text = sGraphHigh[5].ToString("F2");
            bKpdCheck = true;
            bFirstLoad = false;
        }

        private void cmbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb = sender as ComboBox;                                                                                        // Comboboxes need three iterations to completely load (not sure why - binding?)
            if (cb.Name == "cmbColor0")
            {
                iColors[0] = !bFirstLoad ? cb.SelectedIndex : iColors[0];
                bColorLoaded[0] = true;
            }
            if (cb.Name == "cmbColor1")
            {
                iColors[1] = !bFirstLoad ? cb.SelectedIndex : iColors[1];
                bColorLoaded[1] = true;
            }
            if (cb.Name == "cmbColor2")
            {
                iColors[2] = !bFirstLoad ? cb.SelectedIndex : iColors[2];
                bColorLoaded[2] = true;
            }
            if (cb.Name == "cmbColor3")
            {
                iColors[3] = !bFirstLoad ? cb.SelectedIndex : iColors[3];
                bColorLoaded[3] = true;
            }
            if (cb.Name == "cmbColor4")
            {
                iColors[4] = !bFirstLoad ? cb.SelectedIndex : iColors[4];
                bColorLoaded[4] = true;
            }
            if (cb.Name == "cmbColor5")
            {
                iColors[5] = !bFirstLoad ? cb.SelectedIndex : iColors[5];
                bColorLoaded[5] = true;
                if (bFirstLoad)
                    cmbColor5.SelectedIndex = ADS.iColors[5];                                                               // Finish loading last combobox
            }
            bColorsLoaded = bColorLoaded[0] && bColorLoaded[1] && bColorLoaded[2] && bColorLoaded[3] && bColorLoaded[4] && bColorLoaded[5];
            if (!bColorsLoaded)
                LoadcmbColors();
            else if (!bFirstLoad)
                SetSaveButton();
        }

        private void cmbGraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            strGraphPoint[0] = (string)cmbGraph0.SelectedItem;
            strGraphPoint[1] = (string)cmbGraph1.SelectedItem;
            strGraphPoint[2] = (string)cmbGraph2.SelectedItem;
            strGraphPoint[3] = (string)cmbGraph3.SelectedItem;
            strGraphPoint[4] = (string)cmbGraph4.SelectedItem;
            strGraphPoint[5] = (string)cmbGraph5.SelectedItem;
            if (!bFirstLoad)
            {
                cb = sender as ComboBox;
                if (cb.Name == "cmbGraph0")
                {
                    sGraphLow[0] = ADS.GetLowPoint(cmbGraph0.SelectedIndex);
                    sGraphHigh[0] = ADS.GetHighPoint(cmbGraph0.SelectedIndex);
                }
                if (cb.Name == "cmbGraph1")
                {
                    sGraphLow[1] = ADS.GetLowPoint(cmbGraph1.SelectedIndex);
                    sGraphHigh[1] = ADS.GetHighPoint(cmbGraph1.SelectedIndex);
                }
                if (cb.Name == "cmbGraph2")
                {
                    sGraphLow[2] = ADS.GetLowPoint(cmbGraph2.SelectedIndex);
                    sGraphHigh[2] = ADS.GetHighPoint(cmbGraph2.SelectedIndex);
                }
                if (cb.Name == "cmbGraph3")
                {
                    sGraphLow[3] = ADS.GetLowPoint(cmbGraph3.SelectedIndex);
                    sGraphHigh[3] = ADS.GetHighPoint(cmbGraph3.SelectedIndex);
                }
                if (cb.Name == "cmbGraph4")
                {
                    sGraphLow[4] = ADS.GetLowPoint(cmbGraph4.SelectedIndex);
                    sGraphHigh[4] = ADS.GetHighPoint(cmbGraph4.SelectedIndex);
                }
                if (cb.Name == "cmbGraph5")
                {
                    sGraphLow[5] = ADS.GetLowPoint(cmbGraph5.SelectedIndex);
                    sGraphHigh[5] = ADS.GetHighPoint(cmbGraph5.SelectedIndex);
                }
                LoadKeypads();
                SetSaveButton();
            }
        }

        private void kpd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bKpdCheck)
            {
                tb = sender as TextBox;
                float val = Convert.ToSingle(tb.Text);

                if (tb.Name == "kpdGraph0_Low")
                {
                    if (val < Convert.ToSingle(kpdGraph0_High.Text))
                    {
                        sGraphLow[0] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph0_Low.Text = sGraphLow[0].ToString("F2");
                        bKpdCheck = true;
                    }
                }
                if (tb.Name == "kpdGraph0_High")
                {
                    if (val > Convert.ToSingle(kpdGraph0_Low.Text))
                    {
                        sGraphHigh[0] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph0_High.Text = sGraphHigh[0].ToString("F2");
                        bKpdCheck = true;
                    }
                }

                if (tb.Name == "kpdGraph1_Low")
                {
                    if (val < Convert.ToSingle(kpdGraph1_High.Text))
                    {
                        sGraphLow[1] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph1_Low.Text = sGraphLow[1].ToString("F2");
                        bKpdCheck = true;
                    }
                }
                if (tb.Name == "kpdGraph1_High")
                {
                    if (val > Convert.ToSingle(kpdGraph1_Low.Text))
                    {
                        sGraphHigh[1] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph1_High.Text = sGraphHigh[1].ToString("F2");
                        bKpdCheck = true;
                    }
                }

                if (tb.Name == "kpdGraph2_Low")
                {
                    if (val < Convert.ToSingle(kpdGraph2_High.Text))
                    {
                        sGraphLow[2] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph2_Low.Text = sGraphLow[2].ToString("F2");
                        bKpdCheck = true;
                    }
                }
                if (tb.Name == "kpdGraph2_High")
                {
                    if (val > Convert.ToSingle(kpdGraph2_Low.Text))
                    {
                        sGraphHigh[2] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph2_High.Text = sGraphHigh[2].ToString("F2");
                        bKpdCheck = true;
                    }
                }

                if (tb.Name == "kpdGraph3_Low")
                {
                    if (val < Convert.ToSingle(kpdGraph3_High.Text))
                    {
                        sGraphLow[3] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph3_Low.Text = sGraphLow[3].ToString("F2");
                        bKpdCheck = true;
                    }
                }
                if (tb.Name == "kpdGraph3_High")
                {
                    if (val > Convert.ToSingle(kpdGraph3_Low.Text))
                    {
                        sGraphHigh[3] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph3_High.Text = sGraphHigh[3].ToString("F2");
                        bKpdCheck = true;
                    }
                }

                if (tb.Name == "kpdGraph4_Low")
                {
                    if (val < Convert.ToSingle(kpdGraph4_High.Text))
                    {
                        sGraphLow[4] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph4_Low.Text = sGraphLow[4].ToString("F2");
                        bKpdCheck = true;
                    }
                }
                if (tb.Name == "kpdGraph4_High")
                {
                    if (val > Convert.ToSingle(kpdGraph4_Low.Text))
                    {
                        sGraphHigh[4] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph4_High.Text = sGraphHigh[4].ToString("F2");
                        bKpdCheck = true;
                    }
                }

                if (tb.Name == "kpdGraph5_Low")
                {
                    if (val < Convert.ToSingle(kpdGraph5_High.Text))
                    {
                        sGraphLow[5] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph5_Low.Text = sGraphLow[5].ToString("F2");
                        bKpdCheck = true;
                    }
                }
                if (tb.Name == "kpdGraph5_High")
                {
                    if (val > Convert.ToSingle(kpdGraph5_Low.Text))
                    {
                        sGraphHigh[5] = val;
                    }
                    else
                    {
                        bKpdCheck = false;
                        kpdGraph5_High.Text = sGraphHigh[5].ToString("F2");
                        bKpdCheck = true;
                    }
                }

                SetSaveButton();
            }
        }

        private void btnSaveGraph_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                ADS.bGraphChanged[i] = sGraphLow[i] != ADS.sGraphLow[i] || sGraphHigh[i] != ADS.sGraphHigh[i];
                ADS.bGraphChanged[6] = ADS.bGraphChanged[i] ? true : ADS.bGraphChanged[6];
            }
            for (int i = 0; i < 6; i++)
            {
                ADS.strGraphPoint[i] = strGraphPoint[i];
                ADS.iColors[i] = iColors[i];
                ADS.sGraphLow[i] = sGraphLow[i];
                ADS.sGraphHigh[i] = sGraphHigh[i];
            }
            ConfigManager.Config.SaveUserFile();
            ADS.bResetGraph = true;
            Close();
        }

        private void SetSaveButton()
        {
            App.ButtonEnable(ref btnSaveGraph, GraphValuesChanged());
        }

        private bool GraphValuesChanged()
        {
            if (!bFirstLoad)
            {
                if (!Enumerable.SequenceEqual(strGraphPoint, ADS.strGraphPoint)) return true;
                if (!Enumerable.SequenceEqual(iColors, ADS.iColors)) return true;
                if (!Enumerable.SequenceEqual(sGraphLow, ADS.sGraphLow)) return true;
                if (!Enumerable.SequenceEqual(sGraphHigh, ADS.sGraphHigh)) return true;
            }
            return false;
        }

        private void LoadConfiguration()
        {
            ColorList = new List<Brush>()
            {
                Brushes.Red,
                Brushes.Green,
                Brushes.Orange,
                Brushes.Blue,
                Brushes.Gray,
                Brushes.HotPink,
                Brushes.Black,
                Brushes.BlueViolet,
                Brushes.Brown,
                Brushes.Coral,
                Brushes.Crimson,
                Brushes.Yellow
            };
            DataContext = this;

            App.ButtonEnable(ref btnExitGraph, true);
            cmbGraph0.ItemsSource = ADS._cmbPointList;
            cmbGraph1.ItemsSource = ADS._cmbPointList;
            cmbGraph2.ItemsSource = ADS._cmbPointList;
            cmbGraph3.ItemsSource = ADS._cmbPointList;
            cmbGraph4.ItemsSource = ADS._cmbPointList;
            cmbGraph5.ItemsSource = ADS._cmbPointList;
            for (int i = 0; i < 6; i++)
            {
                strGraphPoint[i] = ADS.strGraphPoint[i];
                iColors[i] = ADS.iColors[i];
                sGraphLow[i] = ADS.sGraphLow[i];
                sGraphHigh[i] = ADS.sGraphHigh[i];
            }
            cmbGraph0.SelectedIndex = ADS.iSelectedItem[0];
            cmbGraph1.SelectedIndex = ADS.iSelectedItem[1];
            cmbGraph2.SelectedIndex = ADS.iSelectedItem[2];
            cmbGraph3.SelectedIndex = ADS.iSelectedItem[3];
            cmbGraph4.SelectedIndex = ADS.iSelectedItem[4];
            cmbGraph5.SelectedIndex = ADS.iSelectedItem[5];

            kpdGraph0_Low.Title = "Graph 1 Input 1 Low Value";
            kpdGraph0_High.Title = "Graph 1 Input 1 High Value";
            kpdGraph1_Low.Title = "Graph 1 Input 2 Low Value";
            kpdGraph1_High.Title = "Graph 1 Input 2 High Value";
            kpdGraph2_Low.Title = "Graph 1 Input 3 Low Value";
            kpdGraph2_High.Title = "Graph 1 Input 3 High Value";
            kpdGraph3_Low.Title = "Graph 2 Input 1 Low Value";
            kpdGraph3_High.Title = "Graph 2 Input 1 High Value";
            kpdGraph4_Low.Title = "Graph 2 Input 2 Low Value";
            kpdGraph4_High.Title = "Graph 2 Input 2 High Value";
            kpdGraph5_Low.Title = "Graph 2 Input 3 Low Value";
            kpdGraph5_High.Title = "Graph 2 Input 3 High Value";
        }

        private void btnExitGraph_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            _ = lblFocus.Focus();                                                                                           // 07/22/23 PS
            LoadcmbColors();
            LoadKeypads();
        }

    }
}
