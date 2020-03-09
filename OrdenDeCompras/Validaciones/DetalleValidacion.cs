using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Globalization;
using OrdenDeCompras.Entidades;
using System.Windows.Data;

namespace OrdenDeCompras.Validaciones
{
    public class DetalleValidacion : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value!= null)
            {
                List<OrdenesDetalle> detalle = (List<OrdenesDetalle>)value;

                if (detalle.Count>0)
                {
                    return ValidationResult.ValidResult;
                }
                return new ValidationResult(false, "Debes poner algun producto");
            }
            return new ValidationResult(false, "Debes poner algun producto");
        }
    }
}
