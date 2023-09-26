// Carbon Capture - Calvin
// MainWindow_Graphing.xaml.cs
// Rev 1.00 - July 11, 2023

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Calvin
{
    public partial class MainWindow
    {
        private const short icWidth = 700;
        private const short icHeight = 220;
        private const short icPointWidth = 5;
        private const short iOffset1 = 2;
        private const short iOffset2 = -2;
        private const short icPoints = (icWidth / icPointWidth) + 1;

        private float[] v0 = new float[icPoints];
        private float[] v1 = new float[icPoints];
        private float[] v2 = new float[icPoints];
        private float[] v3 = new float[icPoints];
        private float[] v4 = new float[icPoints];
        private float[] v5 = new float[icPoints];
        private Point[] v0pts = new Point[icPoints];
        private Point[] v1pts = new Point[icPoints];
        private Point[] v2pts = new Point[icPoints];
        private Point[] v3pts = new Point[icPoints];
        private Point[] v4pts = new Point[icPoints];
        private Point[] v5pts = new Point[icPoints];
        private PointCollection v0pc = new PointCollection();
        private PointCollection v1pc = new PointCollection();
        private PointCollection v2pc = new PointCollection();
        private PointCollection v3pc = new PointCollection();
        private PointCollection v4pc = new PointCollection();
        private PointCollection v5pc = new PointCollection();
        private string[] strUD0 = new string[3];
        private string[] strUD1 = new string[3];
        private string[] strUD2 = new string[3];
        private string[] strUD3 = new string[3];
        private string[] strUD4 = new string[3];
        private string[] strUD5 = new string[3];
        private SolidColorBrush[] scbColor = new SolidColorBrush[6];
        private DateTime[] htimes = new DateTime[icPoints];
        private int[] iPosition = new int[6];
        private bool[] bActive = new bool[6];

        private bool bRun;
        private short iLine = 0;
        private DateTime dtmNow;

        public List<Brush> ColorList { get; set; }
        public Brush SelectedColor { get; set; }

        private void GraphTimer()
        {
            bRun = !bRun;                                          // Run graph every second
            if (bRun)
                UpdateGraph();
        }

        private void UpdateGraph()
        {
            if (ADS.bResetGraph)
                LoadConfiguration_Graph();
            if (ADS.bGraphChanged[6])
                RecalcYValues();
            dtmNow = DateTime.Now;
            WriteDataLabels();
            v0pc.Clear();
            v1pc.Clear();
            v2pc.Clear();
            v3pc.Clear();
            v4pc.Clear();
            v5pc.Clear();

            if (iLine < icPoints)
            {
                for (int i = iLine; i >= 0; i--)
                {
                    if (i > 0)
                    {
                        double x = v0pts[i - 1].X - icPointWidth;
                        v0pts[i].Y = v0pts[i - 1].Y;
                        v0[i] = v0[i - 1];
                        v0pts[i].X = x;
                        v1pts[i].Y = v1pts[i - 1].Y;
                        v1[i] = v1[i - 1];
                        v1pts[i].X = x;
                        v2pts[i].Y = v2pts[i - 1].Y;
                        v2[i] = v2[i - 1];
                        v2pts[i].X = x;
                        v3pts[i].Y = v3pts[i - 1].Y;
                        v3[i] = v3[i - 1];
                        v3pts[i].X = x;
                        v4pts[i].Y = v4pts[i - 1].Y;
                        v4[i] = v4[i - 1];
                        v4pts[i].X = x;
                        v5pts[i].Y = v5pts[i - 1].Y;
                        v5[i] = v5[i - 1];
                        v5pts[i].X = x;
                    }
                    else
                    {
                        v0pts[i].X = icWidth;
                        v1pts[i].X = icWidth;
                        v2pts[i].X = icWidth;
                        v3pts[i].X = icWidth;
                        v4pts[i].X = icWidth;
                        v5pts[i].X = icWidth;
                        GetData();
                    }
                    v0pc.Add(v0pts[i]);
                    v1pc.Add(v1pts[i]);
                    v2pc.Add(v2pts[i]);
                    v3pc.Add(v3pts[i]);
                    v4pc.Add(v4pts[i]);
                    v5pc.Add(v5pts[i]);
                }
                iLine++;
            }
            else
            {
                for (int i = icPoints - 1; i > 0; i--)
                {
                    v0pts[i].Y = v0pts[i - 1].Y;
                    v1pts[i].Y = v1pts[i - 1].Y;
                    v2pts[i].Y = v2pts[i - 1].Y;
                    v3pts[i].Y = v3pts[i - 1].Y;
                    v4pts[i].Y = v4pts[i - 1].Y;
                    v5pts[i].Y = v5pts[i - 1].Y;
                    v0pc.Add(v0pts[i]);
                    v1pc.Add(v1pts[i]);
                    v2pc.Add(v2pts[i]);
                    v3pc.Add(v3pts[i]);
                    v4pc.Add(v4pts[i]);
                    v5pc.Add(v5pts[i]);
                }
                GetData();

                v0pc.Add(v0pts[0]);
                v1pc.Add(v1pts[0]);
                v2pc.Add(v2pts[0]);
                v3pc.Add(v3pts[0]);
                v4pc.Add(v4pts[0]);
                v5pc.Add(v5pts[0]);
            }

            for (int i = icPoints - 1; i > 0; i--)
            {
                htimes[i] = htimes[i - 1];
            }
            htimes[0] = dtmNow;

            pln0.Points = ADS.iGraphHandle[0] > 0 ? v0pc : pln0.Points;
            pln1.Points = ADS.iGraphHandle[1] > 0 ? v1pc : pln1.Points;
            pln2.Points = ADS.iGraphHandle[2] > 0 ? v2pc : pln2.Points;
            pln3.Points = ADS.iGraphHandle[3] > 0 ? v3pc : pln3.Points;
            pln4.Points = ADS.iGraphHandle[4] > 0 ? v4pc : pln4.Points;
            pln5.Points = ADS.iGraphHandle[5] > 0 ? v5pc : pln5.Points;

            lblTime0.Content = htimes[0].ToString(@"HH\:mm\:ss");
            lblTime1.Content = htimes[20].ToString(@"HH\:mm\:ss");
            lblTime2.Content = htimes[40].ToString(@"HH\:mm\:ss");
            lblTime3.Content = htimes[60].ToString(@"HH\:mm\:ss");
            lblTime4.Content = htimes[80].ToString(@"HH\:mm\:ss");
            lblTime5.Content = htimes[100].ToString(@"HH\:mm\:ss");
            lblTime6.Content = htimes[120].ToString(@"HH\:mm\:ss");
            lblTime7.Content = htimes[140].ToString(@"HH\:mm\:ss");
        }

        private void GetData()
        {
            v0[0] = ADS.iGraphHandle[0] > 0 ? ADS.ReadAdsSingle(ADS.iGraphHandle[0], "Graph1") : 0;         // Incoming data - Read once per cycle
            v1[0] = ADS.iGraphHandle[1] > 0 ? ADS.ReadAdsSingle(ADS.iGraphHandle[1], "Graph2") : 0;
            v2[0] = ADS.iGraphHandle[2] > 0 ? ADS.ReadAdsSingle(ADS.iGraphHandle[2], "Graph3") : 0;
            v3[0] = ADS.iGraphHandle[3] > 0 ? ADS.ReadAdsSingle(ADS.iGraphHandle[3], "Graph4") : 0;
            v4[0] = ADS.iGraphHandle[4] > 0 ? ADS.ReadAdsSingle(ADS.iGraphHandle[4], "Graph5") : 0;
            v5[0] = ADS.iGraphHandle[5] > 0 ? ADS.ReadAdsSingle(ADS.iGraphHandle[5], "Graph6") : 0;

            iPosition[0] = GetPosition(v0[0], ADS.sGraphLow[0], ADS.sGraphHigh[0], 0);
            iPosition[1] = GetPosition(v1[0], ADS.sGraphLow[1], ADS.sGraphHigh[1], iOffset1);
            iPosition[2] = GetPosition(v2[0], ADS.sGraphLow[2], ADS.sGraphHigh[2], iOffset2);
            iPosition[3] = GetPosition(v3[0], ADS.sGraphLow[3], ADS.sGraphHigh[3], 0);
            iPosition[4] = GetPosition(v4[0], ADS.sGraphLow[4], ADS.sGraphHigh[4], iOffset1);
            iPosition[5] = GetPosition(v5[0], ADS.sGraphLow[5], ADS.sGraphHigh[5], iOffset2);

            v0pts[0].Y = iPosition[0];
            v1pts[0].Y = iPosition[1];
            v2pts[0].Y = iPosition[2];
            v3pts[0].Y = iPosition[3];
            v4pts[0].Y = iPosition[4];
            v5pts[0].Y = iPosition[5];
        }

        private int GetPosition(float t, float low, float high, float offset)
        {
            float pos = 0;
            float range = high - low;
            if (high == 60.0)
            {
                ;
            }
            if (range != 0)
            {
                t = t - low;
                pos = icHeight - (t / range * icHeight);
            }
            if (pos < 0)
            {
                pos = 0;
            }
            else if (pos > icHeight)
            {
                pos = icHeight;
            }
            return Convert.ToInt32(pos - offset);
        }

        private void WriteDataLabels()
        {
            lblGraph1_1.Content = bActive[0] ? Units.UnitDisplay1(v0[0], strUD0) : "";
            lblGraph1_2.Content = bActive[1] ? Units.UnitDisplay1(v1[0], strUD1) : "";
            lblGraph1_3.Content = bActive[2] ? Units.UnitDisplay1(v2[0], strUD2) : "";
            lblGraph2_1.Content = bActive[3] ? Units.UnitDisplay1(v3[0], strUD3) : "";
            lblGraph2_2.Content = bActive[4] ? Units.UnitDisplay1(v4[0], strUD4) : "";
            lblGraph2_3.Content = bActive[5] ? Units.UnitDisplay1(v5[0], strUD5) : "";
        }
        public void LoadConfiguration_Graph()
        {
            ADS.GetGraphInfo();
            LoadPoints();

            lbl0_0.Foreground = ColorList[ADS.iColors[0]];
            lbl1_0.Foreground = ColorList[ADS.iColors[1]];
            lbl2_0.Foreground = ColorList[ADS.iColors[2]];
            lbl3_0.Foreground = ColorList[ADS.iColors[3]];
            lbl4_0.Foreground = ColorList[ADS.iColors[4]];
            lbl5_0.Foreground = ColorList[ADS.iColors[5]];

            float sRange = (ADS.sGraphHigh[0] - ADS.sGraphLow[0]) / 10;
            lbl0_0.Content = Units.strFormat(ADS.sGraphLow[0], ADS.strDecimals[0]);
            lbl0_1.Content = Units.strFormat(ADS.sGraphHigh[0] - (sRange * 9), ADS.strDecimals[0]);
            lbl0_2.Content = Units.strFormat(ADS.sGraphHigh[0] - (sRange * 8), ADS.strDecimals[0]);
            lbl0_3.Content = Units.strFormat(ADS.sGraphHigh[0] - (sRange * 7), ADS.strDecimals[0]);
            lbl0_4.Content = Units.strFormat(ADS.sGraphHigh[0] - (sRange * 6), ADS.strDecimals[0]);
            lbl0_5.Content = Units.strFormat(ADS.sGraphHigh[0] - (sRange * 5), ADS.strDecimals[0]);
            lbl0_6.Content = Units.strFormat(ADS.sGraphHigh[0] - (sRange * 4), ADS.strDecimals[0]);
            lbl0_7.Content = Units.strFormat(ADS.sGraphHigh[0] - (sRange * 3), ADS.strDecimals[0]);
            lbl0_8.Content = Units.strFormat(ADS.sGraphHigh[0] - (sRange * 2), ADS.strDecimals[0]);
            lbl0_9.Content = Units.strFormat(ADS.sGraphHigh[0] - sRange, ADS.strDecimals[0]);
            lbl0_10.Content = Units.strFormat(ADS.sGraphHigh[0], ADS.strDecimals[0]);

            sRange = (ADS.sGraphHigh[1] - ADS.sGraphLow[1]) / 10;
            lbl1_0.Content = Units.strFormat(ADS.sGraphLow[1], ADS.strDecimals[1]);
            lbl1_1.Content = Units.strFormat(ADS.sGraphHigh[1] - (sRange * 9), ADS.strDecimals[1]);
            lbl1_2.Content = Units.strFormat(ADS.sGraphHigh[1] - (sRange * 8), ADS.strDecimals[1]);
            lbl1_3.Content = Units.strFormat(ADS.sGraphHigh[1] - (sRange * 7), ADS.strDecimals[1]);
            lbl1_4.Content = Units.strFormat(ADS.sGraphHigh[1] - (sRange * 6), ADS.strDecimals[1]);
            lbl1_5.Content = Units.strFormat(ADS.sGraphHigh[1] - (sRange * 5), ADS.strDecimals[1]);
            lbl1_6.Content = Units.strFormat(ADS.sGraphHigh[1] - (sRange * 4), ADS.strDecimals[1]);
            lbl1_7.Content = Units.strFormat(ADS.sGraphHigh[1] - (sRange * 3), ADS.strDecimals[1]);
            lbl1_8.Content = Units.strFormat(ADS.sGraphHigh[1] - (sRange * 2), ADS.strDecimals[1]);
            lbl1_9.Content = Units.strFormat(ADS.sGraphHigh[1] - sRange, ADS.strDecimals[1]);
            lbl1_10.Content = Units.strFormat(ADS.sGraphHigh[1], ADS.strDecimals[1]);

            sRange = (ADS.sGraphHigh[2] - ADS.sGraphLow[2]) / 10;
            lbl2_0.Content = Units.strFormat(ADS.sGraphLow[2], ADS.strDecimals[2]);
            lbl2_1.Content = Units.strFormat(ADS.sGraphHigh[2] - (sRange * 9), ADS.strDecimals[2]);
            lbl2_2.Content = Units.strFormat(ADS.sGraphHigh[2] - (sRange * 8), ADS.strDecimals[2]);
            lbl2_3.Content = Units.strFormat(ADS.sGraphHigh[2] - (sRange * 7), ADS.strDecimals[2]);
            lbl2_4.Content = Units.strFormat(ADS.sGraphHigh[2] - (sRange * 6), ADS.strDecimals[2]);
            lbl2_5.Content = Units.strFormat(ADS.sGraphHigh[2] - (sRange * 5), ADS.strDecimals[2]);
            lbl2_6.Content = Units.strFormat(ADS.sGraphHigh[2] - (sRange * 4), ADS.strDecimals[2]);
            lbl2_7.Content = Units.strFormat(ADS.sGraphHigh[2] - (sRange * 3), ADS.strDecimals[2]);
            lbl2_8.Content = Units.strFormat(ADS.sGraphHigh[2] - (sRange * 2), ADS.strDecimals[2]);
            lbl2_9.Content = Units.strFormat(ADS.sGraphHigh[2] - sRange, ADS.strDecimals[2]);
            lbl2_10.Content = Units.strFormat(ADS.sGraphHigh[2], ADS.strDecimals[2]);

            sRange = (ADS.sGraphHigh[3] - ADS.sGraphLow[3]) / 10;
            lbl3_0.Content = Units.strFormat(ADS.sGraphLow[3], ADS.strDecimals[3]);
            lbl3_1.Content = Units.strFormat(ADS.sGraphHigh[3] - (sRange * 9), ADS.strDecimals[3]);
            lbl3_2.Content = Units.strFormat(ADS.sGraphHigh[3] - (sRange * 8), ADS.strDecimals[3]);
            lbl3_3.Content = Units.strFormat(ADS.sGraphHigh[3] - (sRange * 7), ADS.strDecimals[3]);
            lbl3_4.Content = Units.strFormat(ADS.sGraphHigh[3] - (sRange * 6), ADS.strDecimals[3]);
            lbl3_5.Content = Units.strFormat(ADS.sGraphHigh[3] - (sRange * 5), ADS.strDecimals[3]);
            lbl3_6.Content = Units.strFormat(ADS.sGraphHigh[3] - (sRange * 4), ADS.strDecimals[3]);
            lbl3_7.Content = Units.strFormat(ADS.sGraphHigh[3] - (sRange * 3), ADS.strDecimals[3]);
            lbl3_8.Content = Units.strFormat(ADS.sGraphHigh[3] - (sRange * 2), ADS.strDecimals[3]);
            lbl3_9.Content = Units.strFormat(ADS.sGraphHigh[3] - sRange, ADS.strDecimals[3]);
            lbl3_10.Content = Units.strFormat(ADS.sGraphHigh[3], ADS.strDecimals[3]);

            sRange = (ADS.sGraphHigh[4] - ADS.sGraphLow[4]) / 10;
            lbl4_0.Content = Units.strFormat(ADS.sGraphLow[4], ADS.strDecimals[4]);
            lbl4_1.Content = Units.strFormat(ADS.sGraphHigh[4] - (sRange * 9), ADS.strDecimals[4]);
            lbl4_2.Content = Units.strFormat(ADS.sGraphHigh[4] - (sRange * 8), ADS.strDecimals[4]);
            lbl4_3.Content = Units.strFormat(ADS.sGraphHigh[4] - (sRange * 7), ADS.strDecimals[4]);
            lbl4_4.Content = Units.strFormat(ADS.sGraphHigh[4] - (sRange * 6), ADS.strDecimals[4]);
            lbl4_5.Content = Units.strFormat(ADS.sGraphHigh[4] - (sRange * 5), ADS.strDecimals[4]);
            lbl4_6.Content = Units.strFormat(ADS.sGraphHigh[4] - (sRange * 4), ADS.strDecimals[4]);
            lbl4_7.Content = Units.strFormat(ADS.sGraphHigh[4] - (sRange * 3), ADS.strDecimals[4]);
            lbl4_8.Content = Units.strFormat(ADS.sGraphHigh[4] - (sRange * 2), ADS.strDecimals[4]);
            lbl4_9.Content = Units.strFormat(ADS.sGraphHigh[4] - sRange, ADS.strDecimals[4]);
            lbl4_10.Content = Units.strFormat(ADS.sGraphHigh[4], ADS.strDecimals[4]);

            sRange = (ADS.sGraphHigh[5] - ADS.sGraphLow[5]) / 10;
            lbl5_0.Content = Units.strFormat(ADS.sGraphLow[5], ADS.strDecimals[5]);
            lbl5_1.Content = Units.strFormat(ADS.sGraphHigh[5] - (sRange * 9), ADS.strDecimals[5]);
            lbl5_2.Content = Units.strFormat(ADS.sGraphHigh[5] - (sRange * 8), ADS.strDecimals[5]);
            lbl5_3.Content = Units.strFormat(ADS.sGraphHigh[5] - (sRange * 7), ADS.strDecimals[5]);
            lbl5_4.Content = Units.strFormat(ADS.sGraphHigh[5] - (sRange * 6), ADS.strDecimals[5]);
            lbl5_5.Content = Units.strFormat(ADS.sGraphHigh[5] - (sRange * 5), ADS.strDecimals[5]);
            lbl5_6.Content = Units.strFormat(ADS.sGraphHigh[5] - (sRange * 4), ADS.strDecimals[5]);
            lbl5_7.Content = Units.strFormat(ADS.sGraphHigh[5] - (sRange * 3), ADS.strDecimals[5]);
            lbl5_8.Content = Units.strFormat(ADS.sGraphHigh[5] - (sRange * 2), ADS.strDecimals[5]);
            lbl5_9.Content = Units.strFormat(ADS.sGraphHigh[5] - sRange, ADS.strDecimals[5]);
            lbl5_10.Content = Units.strFormat(ADS.sGraphHigh[5], ADS.strDecimals[5]);

            ADS.bResetGraph = false;
        }

        private void LoadPoints()
        {
            strUD0[1] = ADS.strUnits[0];
            strUD0[2] = ADS.strDecimals[0];
            strUD1[1] = ADS.strUnits[1];
            strUD1[2] = ADS.strDecimals[1];
            strUD2[1] = ADS.strUnits[2];
            strUD2[2] = ADS.strDecimals[2];
            strUD3[1] = ADS.strUnits[3];
            strUD3[2] = ADS.strDecimals[3];
            strUD4[1] = ADS.strUnits[4];
            strUD4[2] = ADS.strDecimals[4];
            strUD5[1] = ADS.strUnits[5];
            strUD5[2] = ADS.strDecimals[5];

            bActive[0] = ADS.strGraphAlias[0] != ADS.strInactive;
            bActive[1] = ADS.strGraphAlias[1] != ADS.strInactive;
            bActive[2] = ADS.strGraphAlias[2] != ADS.strInactive;
            bActive[3] = ADS.strGraphAlias[3] != ADS.strInactive;
            bActive[4] = ADS.strGraphAlias[4] != ADS.strInactive;
            bActive[5] = ADS.strGraphAlias[5] != ADS.strInactive;

            tbkGraph1_1.Text = bActive[0] ? ADS.strGraphAlias[0] : "";
            tbkGraph1_2.Text = bActive[1] ? ADS.strGraphAlias[1] : "";
            tbkGraph1_3.Text = bActive[2] ? ADS.strGraphAlias[2] : "";
            tbkGraph2_1.Text = bActive[3] ? ADS.strGraphAlias[3] : "";
            tbkGraph2_2.Text = bActive[4] ? ADS.strGraphAlias[4] : "";
            tbkGraph2_3.Text = bActive[5] ? ADS.strGraphAlias[5] : "";

            tbkGraph1_1.Foreground = ColorList[ADS.iColors[0]];
            pln0.Stroke = bActive[0] ? ColorList[ADS.iColors[0]] : Brushes.Transparent;
            tbkGraph1_2.Foreground = ColorList[ADS.iColors[1]];
            pln1.Stroke = bActive[1] ? ColorList[ADS.iColors[1]] : Brushes.Transparent;
            tbkGraph1_3.Foreground = ColorList[ADS.iColors[2]];
            pln2.Stroke = bActive[2] ? ColorList[ADS.iColors[2]] : Brushes.Transparent;
            tbkGraph2_1.Foreground = ColorList[ADS.iColors[3]];
            pln3.Stroke = bActive[3] ? ColorList[ADS.iColors[3]] : Brushes.Transparent;
            tbkGraph2_2.Foreground = ColorList[ADS.iColors[4]];
            pln4.Stroke = bActive[4] ? ColorList[ADS.iColors[4]] : Brushes.Transparent;
            tbkGraph2_3.Foreground = ColorList[ADS.iColors[5]];
            pln5.Stroke = bActive[5] ? ColorList[ADS.iColors[5]] : Brushes.Transparent;
        }

        public void RecalcYValues()
        {
            if (ADS.bGraphChanged[0])
                v0pc.Clear();
            if (ADS.bGraphChanged[1])
                v1pc.Clear();
            if (ADS.bGraphChanged[2])
                v2pc.Clear();
            if (ADS.bGraphChanged[3])
                v3pc.Clear();
            if (ADS.bGraphChanged[4])
                v4pc.Clear();
            if (ADS.bGraphChanged[5])
                v5pc.Clear();

            for (int i = 0; i < icPoints; i++)
            {
                if (ADS.bGraphChanged[0])
                {
                    v0pts[i].Y = GetPosition(v0[i], ADS.sGraphLow[0], ADS.sGraphHigh[0], 0);
                    v0pc.Add(v0pts[i]);
                }
                if (ADS.bGraphChanged[1])
                {
                    v1pts[i].Y = GetPosition(v1[i], ADS.sGraphLow[1], ADS.sGraphHigh[1], iOffset1);
                    v1pc.Add(v1pts[i]);
                }
                if (ADS.bGraphChanged[2])
                {
                    v2pts[i].Y = GetPosition(v2[i], ADS.sGraphLow[2], ADS.sGraphHigh[2], iOffset2);
                    v2pc.Add(v2pts[i]);
                }
                if (ADS.bGraphChanged[3])
                {
                    v3pts[i].Y = GetPosition(v3[i], ADS.sGraphLow[3], ADS.sGraphHigh[3], 0);
                    v3pc.Add(v3pts[i]);
                }
                if (ADS.bGraphChanged[4])
                {
                    v4pts[i].Y = GetPosition(v4[i], ADS.sGraphLow[4], ADS.sGraphHigh[4], iOffset1);
                    v4pc.Add(v4pts[i]);
                }
                if (ADS.bGraphChanged[5])
                {
                    v5pts[i].Y = GetPosition(v5[i], ADS.sGraphLow[5], ADS.sGraphHigh[5], iOffset2);
                    v5pc.Add(v5pts[i]);
                }
            }
            for (int i = 0; i < 7; i++)
            {
                ADS.bGraphChanged[i] = false;
            }
        }
    }
}
