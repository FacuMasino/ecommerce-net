using System.Collections.Generic;
using System.Diagnostics;

namespace UtilitiesLayer
{
    public class Validator
    {
        public static bool IsNumber(string text)
        {
            foreach (char c in text)
            {
                if (!(char.IsNumber(c)))
                {
                    return false;
                }
            }

            return true;
        }

        public static string RemoveSpaces(string text)
        {
            string temp = "";
            foreach (char c in text)
            {
                if (!char.IsWhiteSpace(c))
                    temp += c;
            }
            return temp;
        }

        public static bool IsAlphanumeric(string text)
        {
            foreach (char c in RemoveSpaces(text))
            {
                if (!char.IsNumber(c) && !char.IsLetter(c))
                    return false;
            }
            return true;
        }

        public static bool IsDecimal(string text)
        {
            int commaCounter = 0;
            if (text[0] == '.' || text[0] == ',')
                return false;
            if (text[text.Length - 1] == '.' || text[text.Length - 1] == ',')
                return false;
            foreach (char c in text)
            {
                if (c == '.' || c == ',')
                    commaCounter++;
                if (!char.IsNumber(c) && (c != '.' && c != ','))
                    return false;
            }
            return (commaCounter <= 1);
        }

        public static bool HasData(string text, int minimumLength = 2, int maximumLength = 0)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            if (text == "")
            {
                return false;
            }

            if (text.Length < minimumLength)
            {
                return false;
            }

            if (maximumLength != 0 && maximumLength < text.Length)
            {
                return false;
            }

            return true;
        }

        public static bool IsGoodInput(InputWrapper input)
        {
            switch (input.InputType.ToString())
            {
                case "System.String":
                    if (!HasData(input.Control.Text, input.MinLength, input.MaxLength))
                    {
                        Debug.Print($"Campo Invalido: {input.Control.ClientID}");
                        return false;
                    }
                    break;
                case "System.Decimal":
                    // Si se ingresa un nro fuera de rango, decimal.TryParse va a dar false
                    if (
                        !HasData(input.Control.Text, input.MinLength)
                        || !IsDecimal(input.Control.Text)
                        || !decimal.TryParse(input.Control.Text, out decimal num)
                    )
                    {
                        Debug.Print($"Campo Invalido: {input.Control.ClientID}");
                        return false;
                    }
                    if (input.IsPositive && decimal.Parse(input.Control.Text) <= 0)
                        return false;
                    break;
                case "System.Int32":
                    if (
                        !HasData(input.Control.Text, input.MinLength)
                        || !IsNumber(input.Control.Text)
                    )
                    {
                        Debug.Print($"Campo Inválido:{input.Control.ClientID}");
                        return false;
                    }
                    break;
            }
            return true;
        }

        public static bool RunValidations(List<InputWrapper> inputs)
        {
            int invalids = 0;
            foreach (InputWrapper input in inputs)
            {
                if (!Validator.IsGoodInput(input))
                {
                    input.IsValid = false;
                    invalids++;
                }
                else
                {
                    input.IsValid = true;
                }
            }
            return invalids == 0;
        }
    }
}
