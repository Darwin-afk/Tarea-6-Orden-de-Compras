using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OrdenDeCompras.Entidades
{
    public class OrdenesDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }

        public OrdenesDetalle()
        {
            DetalleId = 0;
            OrdenId = 0;
            ProductoId = 0;
            Cantidad = 0;
            Precio = 0;
        }

        public OrdenesDetalle(int productoId, int cantidad, int precio)
        {
            DetalleId = 0;
            OrdenId = 0;
            ProductoId = productoId;
            Cantidad = cantidad;
            Precio = precio;
        }
    }
}
