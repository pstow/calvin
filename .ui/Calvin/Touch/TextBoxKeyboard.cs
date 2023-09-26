using System.Windows;
using System.Windows.Controls;
// Rev 1.00 - June 9, 2023

namespace Calvin.Touch
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:Keypad="clr-namespace:Calvin.Touch"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:Keypad="clr-namespace:Carbon_capture.Touch;assembly=Calvin.Touch"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <Keypad:TextBoxKeyboard/>
    ///
    /// </summary>
    public class TextBoxKeyboard : TextBox
    {
        private string name;                        // String to display at top of keyboard

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            // Save textbox value
            string temp = e.OriginalSource.ToString();
            if (string.IsNullOrEmpty(Title))
            {
                // Remove any underscores from name
                name = Name.Replace("_", " ");
            }
            else
            {
                // Override name with Title
                name = Title;
            }
            // Check for colon - signifies actual value in textbox
            int colon = temp.IndexOf(':') + 2;
            // Save value or clear if no current value
            SavedValue = (colon == 1) ? "" : temp.Substring(colon, temp.Length - colon);
            //base.OnGotFocus(e);

            Window kb = new Keyboard(KBMode, name, MinStringLength, MaxStringLength, SavedValue, Decimals);
            _ = kb.ShowDialog();
            PasswordText = Keyboard.strLabel;
            Text = KBMode != KeyboardMode.Password ? Keyboard.strLabel : !ADS.bDisplayPassword ? "************" : PasswordText;
        }

        public KeyboardMode KBMode { get; set; }

        public int MinStringLength { get; set; } = 1;

        public int MaxStringLength { get; set; } = 25;

        public string Title { get; set; }

        public string PasswordText { get; set; }

        public string SavedValue { get; set; }

        public int Decimals { get; set; } = 1;
    }
}
