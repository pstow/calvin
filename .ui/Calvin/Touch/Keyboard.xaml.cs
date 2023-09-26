using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
// Rev 1.00 - June 9, 2023

namespace Calvin.Touch
{
    public enum KeyboardMode : int
    {
        Integer = 0,                            // Integers only
        Integer0,                               // Integers only with zero allowed
        Real,                                   // Real numbers (decimal point - number allowed depends on iDecimal (1 or 2))
        RealNeg,                                // Real numbers (negative, decimal point - number allowed depends on iDecimal (1 or 2))
        Password,                               // All keys except space
        AlphaNumeric,                           // All keys except space, hyphen & comma
        Text,                                   // All keys except underscore
        TextEmpty                               // Allow empty value to be entered
    }

    public partial class Keyboard : Window
    {
        public static string strLabel;
        public static bool bPassword;
        private readonly string strSavedValue;
        private readonly KeyboardMode kbMode;
        private readonly bool[] bSpaceChar = new bool[40];
        private readonly int iMinLength;
        private readonly int iDecimals;
        private string strPassword;
        private string strVal;
        private string k;
        private string hyphen;
        private bool bSave = false;
        private bool bDotEntered = false;
        private bool bHyphenEntered = false;
        private bool bShift;
        private int iShift;
        private int iLength;
        private int iDecCount;

        public Keyboard(KeyboardMode mode, string title, int minlen, int maxlen, string savedValue, int decimals)
        {
            InitializeComponent();
            lblTitle.Content = "Enter " + title;
            btnBackSpace.Content = "Backspace";
            btnEnter.Content = "Enter";
            btnShiftL.Content = "Shift";
            btnSpace.Content = "Space";
            iDecimals = decimals;
            iDecCount = 0;
            kbMode = mode;
            bPassword = mode == KeyboardMode.Password;
            if (kbMode <= KeyboardMode.RealNeg)
            {
                DisableAlphas();
                btnDot.IsEnabled = kbMode > KeyboardMode.Integer0;
            }
            else
            {
                btnSpace.IsEnabled = kbMode == KeyboardMode.Text;
                btnComma.IsEnabled = kbMode != KeyboardMode.AlphaNumeric;
                btnUS_Hyphen.Content = kbMode == KeyboardMode.AlphaNumeric ? "_" : "-";
            }
            iMinLength = minlen;
            iLength = maxlen;
            strLabel = "";
            strSavedValue = savedValue;
            ResetKeyboard();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            k = e.Key.ToString();
            if (k == "Escape")
            {
                btnExit_Update();
            }
            else if (k == "LeftShift" || k == "RightShift")
            {
                bShift = true;
            }
            else if (btnEnter.IsEnabled && k == "Return")
            {
                btnEnter_Update();
            }
            else if (btnSpace.IsEnabled && k == "Space")
            {
                btnSpace_Update();
            }
            else if (btnBackSpace.IsEnabled && k == "Back")
            {
                btnBackSpace_Update();
            }
            else if (btn1.IsEnabled && (k == "D1" || k == "NumPad1"))
            {
                if (!bShift || k == "NumPad1")
                {
                    addKey("1");
                }
                else if (kbMode == KeyboardMode.Password)
                {
                    addKey("!");
                }
            }
            else if (btn2.IsEnabled && (k == "D2" || k == "NumPad2"))
            {
                if (!bShift || k == "NumPad2")
                {
                    addKey("2");
                }
                else if (kbMode == KeyboardMode.Password || kbMode == KeyboardMode.AlphaNumeric)
                {
                    addKey("@");
                }
            }
            else if (btn3.IsEnabled && (k == "D3" || k == "NumPad3"))
            {
                if (!bShift || k == "NumPad3")
                {
                    addKey("3");
                }
                else if (kbMode == KeyboardMode.Password)
                {
                    addKey("#");
                }
            }
            else if (btn4.IsEnabled && (k == "D4" || k == "NumPad4"))
            {
                if (!bShift || k == "NumPad4")
                {
                    addKey("4");
                }
                else if (kbMode == KeyboardMode.Password)
                {
                    addKey("$");
                }
            }
            else if (btn5.IsEnabled && (k == "D5" || k == "NumPad5"))
            {
                if (!bShift || k == "NumPad5")
                {
                    addKey("5");
                }
                else if (kbMode == KeyboardMode.Password)
                {
                    addKey("%");
                }
            }
            else if (btn6.IsEnabled && (k == "D6" || k == "NumPad6"))
            {
                if (!bShift || k == "NumPad6")
                {
                    addKey("6");
                }
                else if (kbMode == KeyboardMode.Password)
                {
                    addKey("^");
                }
            }
            else if (btn7.IsEnabled && (k == "D7" || k == "NumPad7"))
            {
                if (!bShift || k == "NumPad7")
                {
                    addKey("7");
                }
                else if (kbMode == KeyboardMode.Password)
                {
                    addKey("&");
                }
            }
            else if (btn8.IsEnabled && (k == "D8" || k == "NumPad8"))
            {
                if (!bShift || k == "NumPad8")
                {
                    addKey("8");
                }
                else if (kbMode == KeyboardMode.Password)
                {
                    addKey("*");
                }
            }
            else if (btn9.IsEnabled && (k == "D9" || k == "NumPad9"))
            {
                if (!bShift || k == "NumPad9")
                {
                    addKey("9");
                }
                else if (kbMode == KeyboardMode.Password)
                {
                    addKey("(");
                }
            }
            else if (btn0.IsEnabled && (k == "D0" || k == "NumPad0"))
            {
                if (!bShift || k == "NumPad0")
                {
                    addKey("0");
                }
                else if (kbMode == KeyboardMode.Password)
                {
                    addKey(")");
                }
            }
            else if (btnSlash.IsEnabled && k == "OemQuestion")
            {
                addKey("/");
            }
            else if (btnColon.IsEnabled && k == "Oem1")
            {
                addKey(":");
            }
            else if (btnComma.IsEnabled && k == "OemComma")
            {
                addKey(",");
            }
            else if (btnDot.IsEnabled && (k == "OemPeriod" || k == "Decimal"))
            {
                btnDot_Update();
            }
            else if (btnUS_Hyphen.IsEnabled && (k == "OemMinus" || k == "Subtract"))
            {
                k = (kbMode == KeyboardMode.AlphaNumeric) ? "_" : "-";
                btnUS_Hyphen_Update(k);
            }
            else if (btnA.IsEnabled && k == "A")
            {
                btnAlpha_Update(k);
            }
            else if (btnB.IsEnabled && k == "B")
            {
                btnAlpha_Update(k);
            }
            else if (btnC.IsEnabled && k == "C")
            {
                btnAlpha_Update(k);
            }
            else if (btnD.IsEnabled && k == "D")
            {
                btnAlpha_Update(k);
            }
            else if (btnE.IsEnabled && k == "E")
            {
                btnAlpha_Update(k);
            }
            else if (btnF.IsEnabled && k == "F")
            {
                btnAlpha_Update(k);
            }
            else if (btnG.IsEnabled && k == "G")
            {
                btnAlpha_Update(k);
            }
            else if (btnH.IsEnabled && k == "H")
            {
                btnAlpha_Update(k);
            }
            else if (btnI.IsEnabled && k == "I")
            {
                btnAlpha_Update(k);
            }
            else if (btnJ.IsEnabled && k == "J")
            {
                btnAlpha_Update(k);
            }
            else if (btnK.IsEnabled && k == "K")
            {
                btnAlpha_Update(k);
            }
            else if (btnL.IsEnabled && k == "L")
            {
                btnAlpha_Update(k);
            }
            else if (btnM.IsEnabled && k == "M")
            {
                btnAlpha_Update(k);
            }
            else if (btnN.IsEnabled && k == "N")
            {
                btnAlpha_Update(k);
            }
            else if (btnO.IsEnabled && k == "O")
            {
                btnAlpha_Update(k);
            }
            else if (btnP.IsEnabled && k == "P")
            {
                btnAlpha_Update(k);
            }
            else if (btnQ.IsEnabled && k == "Q")
            {
                btnAlpha_Update(k);
            }
            else if (btnR.IsEnabled && k == "R")
            {
                btnAlpha_Update(k);
            }
            else if (btnS.IsEnabled && k == "S")
            {
                btnAlpha_Update(k);
            }
            else if (btnT.IsEnabled && k == "T")
            {
                btnAlpha_Update(k);
            }
            else if (btnU.IsEnabled && k == "U")
            {
                btnAlpha_Update(k);
            }
            else if (btnV.IsEnabled && k == "V")
            {
                btnAlpha_Update(k);
            }
            else if (btnW.IsEnabled && k == "W")
            {
                btnAlpha_Update(k);
            }
            else if (btnX.IsEnabled && k == "X")
            {
                btnAlpha_Update(k);
            }
            else if (btnY.IsEnabled && k == "Y")
            {
                btnAlpha_Update(k);
            }
            else if (btnZ.IsEnabled && k == "Z")
            {
                btnAlpha_Update(k);
            }
            e.Handled = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)                                                     // Rev 01 (All)
        {
            k = e.Key.ToString();
            if (k == "LeftShift" || k == "RightShift")
            {
                bShift = false;
            }
        }

        private void addKey(string key)
        {
            iShift = iShift != 2 ? 0 : 2;
            if (bDotEntered)
            {
                if (iDecCount < iDecimals)
                {
                    strLabel += key;
                    iDecCount++;
                    tbxString.Text = strLabel;
                    setShiftKeys(false);
                }
            }
            else
            {
                if (strLabel.Length != tbxString.Text.Length)
                {
                    strLabel = tbxString.Text;
                }
                if (strLabel.Length < iLength || (key == "." && kbMode <= KeyboardMode.RealNeg))
                {
                    strLabel += key;
                    strPassword += "*";
                    tbxString.Text = bPassword && !ADS.bDisplayPassword ? strPassword : strLabel;
                    setShiftKeys(false);
                    tbxString.CaretIndex = strLabel.Length;
                    if (kbMode == KeyboardMode.RealNeg)
                    {
                        btnUS_Hyphen.IsEnabled = false;
                    }
                }
            }
            ResetKeyboard();
        }

        private void btnNumber_Click(object sender, RoutedEventArgs e)
        {
            string digit = ((Button)sender).Content.ToString();
            addKey(digit);
        }

        private void btnDot_Click(object sender, RoutedEventArgs e)
        {
            btnDot_Update();
        }

        private void btnDot_Update()
        {
            addKey(".");
            if (kbMode <= KeyboardMode.RealNeg)
            {
                bDotEntered = true;
                btnDot.IsEnabled = false;
            }
        }

        private void btnUS_Hyphen_Click(object sender, RoutedEventArgs e)
        {
            hyphen = ((Button)sender).Content.ToString();
            btnUS_Hyphen_Update(hyphen);
        }

        private void btnUS_Hyphen_Update(string s)
        {
            addKey(s);
            if (s == "-" && kbMode == KeyboardMode.RealNeg)
            {
                bHyphenEntered = true;
                btnUS_Hyphen.IsEnabled = false;
                iLength++;
            }
        }

        private void btnComma_Click(object sender, RoutedEventArgs e)
        {
            addKey(",");
        }

        private void btnAlpha_Click(object sender, RoutedEventArgs e)
        {
            if (kbMode >= KeyboardMode.RealNeg)
            {
                string alpha = ((Button)sender).Content.ToString();
                alpha = (iShift == 0) ? alpha.ToLower() : alpha;
                addKey(alpha);
            }
            else
            {
                tbxString.CaretIndex = strLabel.Length;
                ResetKeyboard();
            }
        }

        private void btnAlpha_Update(string s)
        {
            if (kbMode >= KeyboardMode.RealNeg)
            {
                s = bShift ? s : s.ToLower();
                addKey(s);
            }
            else
            {
                tbxString.CaretIndex = strLabel.Length;
                ResetKeyboard();
            }
        }

        private void btnSpace_Click(object sender, RoutedEventArgs e)
        {
            btnSpace_Update();
        }

        private void btnSpace_Update()
        {
            if (kbMode == KeyboardMode.Text)
            {
                string alpha = "_";
                bSpaceChar[strLabel.Length] = true;
                addKey(alpha);
            }
            else
            {
                tbxString.CaretIndex = strLabel.Length;
                ResetKeyboard();
            }
        }

        private void btnBackSpace_Click(object sender, RoutedEventArgs e)
        {
            btnBackSpace_Update();
        }

        private void btnBackSpace_Update()
        {
            iShift = iShift != 2 ? 0 : 2;
            if (bDotEntered)
            {
                strLabel = strLabel.Substring(0, strLabel.Length - 1);
                if (iDecCount == 0)
                {
                    bDotEntered = false;
                    btnDot.IsEnabled = true;
                }
                else
                {
                    iDecCount--;
                }
                tbxString.Text = strLabel;
                setShiftKeys(false);
                tbxString.CaretIndex = strLabel.Length;
                if (strLabel.Length == 0 && kbMode == KeyboardMode.RealNeg)
                {
                    btnUS_Hyphen.IsEnabled = true;
                }
                ResetKeyboard();
            }
            else
            {
                if (strLabel.Length > 0)
                {
                    if (strLabel.Length == 1)
                    {
                        if (bHyphenEntered)
                        {
                            bHyphenEntered = false;
                            btnUS_Hyphen.IsEnabled = true;
                            iLength--;
                        }
                        else if (kbMode == KeyboardMode.RealNeg)
                        {
                            btnUS_Hyphen.IsEnabled = true;
                        }
                    }
                    if (strLabel.Substring(strLabel.Length - 1, 1) == "_")
                    {
                        bSpaceChar[strLabel.Length - 1] = false;
                    }
                    strLabel = strLabel.Substring(0, strLabel.Length - 1);
                    strPassword = strPassword.Substring(0, strPassword.Length - 1);
                    tbxString.Text = bPassword && !ADS.bDisplayPassword ? strPassword : strLabel;
                    setShiftKeys(false);
                    tbxString.CaretIndex = strLabel.Length;
                }
                else
                {
                    tbxString.CaretIndex = 0;
                }
                ResetKeyboard();
            }
        }

        private void btnShift_Click(object sender, RoutedEventArgs e)
        {
            btnShift_Update();
        }

        private void btnShift_Update()
        {
            iShift++;
            iShift = iShift > 2 ? 0 : iShift;
            setShiftKeys(true);
        }

        private void setShiftKeys(bool bShift)
        {
            if (iShift > 0)
            {
                btnUS_Hyphen.Content = kbMode == KeyboardMode.AlphaNumeric ? "_" : "-";
                btn1.Content = kbMode == KeyboardMode.Password ? "!" : "1";
                btn2.Content = kbMode == KeyboardMode.Password ? "@" : "2";
                btn3.Content = kbMode == KeyboardMode.Password ? "#" : "3";
                btn4.Content = kbMode == KeyboardMode.Password ? "$" : "4";
                btn5.Content = kbMode == KeyboardMode.Password ? "%" : "5";
                btn6.Content = kbMode == KeyboardMode.Password ? "^" : "6";
                btn7.Content = kbMode == KeyboardMode.Password ? "&" : "7";
                btn8.Content = kbMode == KeyboardMode.Password ? "*" : "8";
                btn9.Content = kbMode == KeyboardMode.Password ? "(" : "9";
                btn0.Content = kbMode == KeyboardMode.Password ? ")" : "0";
                btnQ.Content = "Q";
                btnW.Content = "W";
                btnE.Content = "E";
                btnR.Content = "R";
                btnT.Content = "T";
                btnY.Content = "Y";
                btnU.Content = "U";
                btnI.Content = "I";
                btnO.Content = "O";
                btnP.Content = "P";
                btnAT.Content = "@";
                btnA.Content = "A";
                btnS.Content = "S";
                btnD.Content = "D";
                btnF.Content = "F";
                btnG.Content = "G";
                btnH.Content = "H";
                btnJ.Content = "J";
                btnK.Content = "K";
                btnL.Content = "L";
                btnZ.Content = "Z";
                btnX.Content = "X";
                btnC.Content = "C";
                btnV.Content = "V";
                btnB.Content = "B";
                btnN.Content = "N";
                btnM.Content = "M";
            }
            else
            {
                btnUS_Hyphen.Content = (kbMode >= KeyboardMode.Text || kbMode == KeyboardMode.RealNeg) ? "-" : "_";
                btn1.Content = "1";
                btn2.Content = "2";
                btn3.Content = "3";
                btn4.Content = "4";
                btn5.Content = "5";
                btn6.Content = "6";
                btn7.Content = "7";
                btn8.Content = "8";
                btn9.Content = "9";
                btn0.Content = "0";
                btnQ.Content = "q";
                btnW.Content = "w";
                btnE.Content = "e";
                btnR.Content = "r";
                btnT.Content = "t";
                btnY.Content = "y";
                btnU.Content = "u";
                btnI.Content = "i";
                btnO.Content = "o";
                btnP.Content = "p";
                btnAT.Content = "@";
                btnA.Content = "a";
                btnS.Content = "s";
                btnD.Content = "d";
                btnF.Content = "f";
                btnG.Content = "g";
                btnH.Content = "h";
                btnJ.Content = "j";
                btnK.Content = "k";
                btnL.Content = "l";
                btnZ.Content = "z";
                btnX.Content = "x";
                btnC.Content = "c";
                btnV.Content = "v";
                btnB.Content = "b";
                btnN.Content = "n";
                btnM.Content = "m";
            }
            if (iShift == 2)
            {
                lblCaps.Visibility = Visibility.Visible;
            }
            else
            {
                lblCaps.Visibility = Visibility.Hidden;
                iShift = bShift == false ? 0 : iShift;
            }
            ResetKeyboard();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            btnEnter_Update();
        }

        private void btnEnter_Update()
        {
            bSave = true;
            if (!string.IsNullOrEmpty(strLabel))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < strLabel.Length; i++)
                {
                    _ = bSpaceChar[i] ? sb.Append(" ") : sb.Append(strLabel[i]);
                }
                strLabel = sb.ToString();
            }
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!bSave || (string.IsNullOrEmpty(strLabel) && iMinLength > 0))
            {
                strLabel = strSavedValue;
            }
            else if (string.IsNullOrEmpty(strLabel) && iMinLength == 0)
            {
                strLabel = "";
            }
            else if (kbMode == KeyboardMode.Real || kbMode == KeyboardMode.RealNeg)
            {
                try
                {
                    float x = Convert.ToSingle(strLabel);
                    strLabel = iDecimals == 2 ? x.ToString("F2") : x.ToString("F1");
                }
                catch (Exception ex)
                {
                    App.WPFMessageBoxOK(ex.Message + " - kbd1", App.bgRed);
                }
            }
        }

        private void tbxString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                strLabel = tbxString.Text;
                bSave = true;
                Close();
            }
            else
            {
                strPassword += "*";
            }
        }

        private void ResetKeyboard()
        {
            strVal = tbxString.Text;
            if ((strVal == "." || strVal == "-" || strVal == "-.") && (kbMode == KeyboardMode.Real || kbMode == KeyboardMode.RealNeg))
            {
                btnEnter.IsEnabled = false;
                btnBackSpace.IsEnabled = true;
            }
            else
            {
                btnEnter.IsEnabled = iMinLength <= strLabel.Length || kbMode == KeyboardMode.TextEmpty;
                btnBackSpace.IsEnabled = iMinLength <= strLabel.Length;
                btn0.IsEnabled = kbMode != KeyboardMode.Integer || iMinLength <= strLabel.Length;
            }
        }

        private void DisableAlphas()
        {
            btnAT.IsEnabled = false;
            btnA.IsEnabled = false;
            btnB.IsEnabled = false;
            btnC.IsEnabled = false;
            btnD.IsEnabled = false;
            btnE.IsEnabled = false;
            btnF.IsEnabled = false;
            btnG.IsEnabled = false;
            btnH.IsEnabled = false;
            btnI.IsEnabled = false;
            btnJ.IsEnabled = false;
            btnK.IsEnabled = false;
            btnL.IsEnabled = false;
            btnM.IsEnabled = false;
            btnN.IsEnabled = false;
            btnO.IsEnabled = false;
            btnP.IsEnabled = false;
            btnQ.IsEnabled = false;
            btnR.IsEnabled = false;
            btnS.IsEnabled = false;
            btnT.IsEnabled = false;
            btnU.IsEnabled = false;
            btnV.IsEnabled = false;
            btnW.IsEnabled = false;
            btnX.IsEnabled = false;
            btnY.IsEnabled = false;
            btnZ.IsEnabled = false;
            btnUS_Hyphen.IsEnabled = kbMode == KeyboardMode.RealNeg;
            btnSlash.IsEnabled = false;
            btnColon.IsEnabled = false;
            btnComma.IsEnabled = false;
            btnShiftL.IsEnabled = false;
            btnSpace.IsEnabled = false;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            _ = lblTitle.Focus();
            _ = App.SetCursorPos(Convert.ToInt32(Left + (Width / 2)), Convert.ToInt32(Top + 20));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            btnExit_Update();
        }

        private void btnExit_Update()
        {
            bSave = false;
            Close();
        }

    }
}
