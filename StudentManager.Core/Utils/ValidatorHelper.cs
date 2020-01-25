using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace StudentManager.Core.Utils
{
    public static class ValidatorHelper
    {
        public static bool ValidateIntInTextBox(TextBox textBox, out int textBoxValue)
        {
            if (int.TryParse(textBox.Text, out textBoxValue) && !string.IsNullOrEmpty(textBox.Text)) return true;

            textBox.Text = "Incorrect value!";

            return false;

        }

        public static bool ValidateStringInTextBox(TextBox textBox)
        {
            if (!string.IsNullOrEmpty(textBox.Text) && Regex.IsMatch(textBox.Text, @"^[a-zA-Z]+$")) return true;

            textBox.Text = "Incorrect value!";

            return false;

        }
    }
}
