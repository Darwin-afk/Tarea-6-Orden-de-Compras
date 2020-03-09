using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdenDeCompras.Entidades
{
    public class Ordenes
    {
        [Key]
        public int OrdenId { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public int Monto { get; set; }
        [ForeignKey("OrdenId")]
        public virtual List<OrdenesDetalle> Detalle { get; set; }

        public Ordenes()
        {
            OrdenId = 0;
            Fecha = DateTime.Now;
            ClienteId = 0;
            Monto = 0;
            Detalle = new List<OrdenesDetalle>();
        }
    }
}
