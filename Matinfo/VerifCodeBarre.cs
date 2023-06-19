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
    /// Vérification code barre
    /// </summary>
    public class VerifCodeBarre : ValidationRule
    {
        public VerifCodeBarre()
        {

        }
        /// <summary>
        /// Vérification du format du Code barre
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>La validité du résultat</returns>

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string codeBarre;
            if (value == null)
            {
                return new ValidationResult(false, "code barre obligatoire");
            }
            else if (((string)value).Length == 0)
            {
                return new ValidationResult(false, "code barre obligatoire");
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
                return new ValidationResult(false, "code barre est alphanumérique");
            }
        }
    }
}
