using System;
using System.Web.UI.WebControls;

namespace UtilitiesLayer
{
    public class InputWrapper
    {
        public TextBox Control { get; set; }
        public Type InputType { get; set; }

        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public bool IsPositive { get; set; }
        public bool IsValid { get; set; }

        public InputWrapper(
            TextBox textBox,
            Type inputType,
            int minLength = 2,
            int maxLength = 0,
            bool positive = true
        )
        {
            this.Control = textBox;
            this.InputType = inputType;
            MinLength = minLength;
            MaxLength = maxLength;
            IsPositive = positive;
            IsValid = true;
        }
    }
}
