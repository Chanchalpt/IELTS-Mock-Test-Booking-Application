using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

namespace WpfApp6
{
    class Rule : ValidationRule
    {
        int min;
        int max;

        public int Minimum { get => min; set => min = value; }
        public int Maximum { get => max; set => max = value; }

        public override ValidationResult Validate(object value, CultureInfo culture)
        {
            int numVal = 0;
            string valueString = value.ToString();
            if (!int.TryParse(valueString, out numVal))
            {
                return new ValidationResult(false, "Invalid data");
            }

            if (numVal < min || numVal > max)
            {
                return new ValidationResult(false, "Invalid age Range");
            }

            return ValidationResult.ValidResult;
        }
    }
}
