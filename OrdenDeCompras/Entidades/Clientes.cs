using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OrdenDeCompras.Entidades
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombre { get; set; }

        public Clientes()
        {
            ClienteId = 0;
            Nombre = string.Empty;
        }
    }
}
