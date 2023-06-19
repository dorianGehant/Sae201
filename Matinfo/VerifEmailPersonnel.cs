using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Matinfo
{
    /// <summary>
    /// Vérification de l'email
    /// </summary>
    public class VerifEmailPersonnel : ValidationRule
    {
        public VerifEmailPersonnel()
        {

        }
        /// <summary>
        /// Vérification du format de l' email
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>La validité du résultat</returns>
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
