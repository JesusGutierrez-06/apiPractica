using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiPractica.Models
{
    public class Detalle_Venta
    {
        [Key]
        public int id { get; set; }
        public int ventaid { get; set; }
        public Venta venta { get; set; }
        public int articuloid { get; set; }
        public Articulo articulo { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }
    }
}
