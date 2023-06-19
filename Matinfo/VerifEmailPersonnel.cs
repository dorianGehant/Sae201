using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Matinfo
{
    public class VerifEmailPersonnel : ValidationRule
    {
        public VerifEmailPersonnel()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string email;
            if (value == null)
            {
                return new ValidationResult(false, "email obligatoire");
            }
            else if (((string)value).Length == 0)
            {
                return new ValidationResult(false, "email obligatoire");
            }
            else
            {
                try
                {
                    int val = int.Parse((string)value);
                }
                catch (Exception e)
                {
                    return ValidationResult.ValidResult;
                }
                return new ValidationResult(false, "l'email n'est pas un nombre");
            }
        }
    }
}
