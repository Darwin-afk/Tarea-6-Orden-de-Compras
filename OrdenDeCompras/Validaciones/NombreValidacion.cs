using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace OrdenDeCompras.Validaciones
{
    public class NombreValidacion : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string cadena = value as string;
            if (cadena != null)
            {
                if (cadena.Length <= 0)
                    return new ValidationResult(false, "Debes poner un Nombre");

                foreach (var caracter in cadena)
                {
                    if (!char.IsLetter(caracter))
                        return new ValidationResult(false, "El nombre solo puede tener letras");
                }

                return ValidationResult.ValidResult;

            }
            return new ValidationResult(false, "Debes poner un Nombre");
        }
    }
}
