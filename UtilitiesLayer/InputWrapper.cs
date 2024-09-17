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
        public bool IsAlphanumeric { get; set; }
        public bool IsPassword { get; set; }
        public bool Bypass { get; set; }

        public InputWrapper(
            TextBox textBox,
            Type inputType,
            int minLength = 2,
            int maxLength = 0,
            bool positiveNumber = true,
            bool alphanumeric = false,
            bool password = false,
            bool bypass = false
        )
        {
            this.Control = textBox;
            this.InputType = inputType;
            MinLength = minLength;
            MaxLength = maxLength;
            IsPositive = positiveNumber;
            IsValid = true;
            IsAlphanumeric = alphanumeric;
            IsPassword = password;
            Bypass = bypass;
        }
    }
}
