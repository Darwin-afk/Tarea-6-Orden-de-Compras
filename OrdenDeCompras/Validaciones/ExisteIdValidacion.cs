using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace OrdenDeCompras.Validaciones
{
    public class ExisteIdValidacion : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                int id = 0;
                try
                {
                    id = Convert.ToInt32(value);
                }
                catch
                {
                    return new ValidationResult(false, "El ID debe ser un numero");
                }

                if (id >= 1)
                    return ValidationResult.ValidResult;
                else
                    return new ValidationResult(false, "El ID debe mayor o igual a uno");

            }
            return new ValidationResult(false, "Debes poner un ID");
        }
    }
}
