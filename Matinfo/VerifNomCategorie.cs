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
    /// Vérification de la catégorie
    /// </summary>
    public class VerifNomCategorie : ValidationRule
    {
        public VerifNomCategorie()
        {

        }
        /// <summary>
        /// Vérification du format du nom de la catégorie
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>La validité du résultat</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string nomCat;
            if(value == null)
            {
                return new ValidationResult(false, "Nom obligatoire");
            }
            else if (((string)value).Length == 0){
                return new ValidationResult(false, "Nom obligatoire");
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
                return new ValidationResult(false, "Nom autre que nombre");
            }
        }
    }
}
