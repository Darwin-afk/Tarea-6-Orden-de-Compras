using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdenDeCompras.Entidades
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }

        public Productos()
        {
            ProductoId = 0;
            Descripcion = string.Empty;
            Precio = 1;
        }
    }
}
