using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiPractica.Models
{
    public class Detalle_Compra
    {
        [Key]
        public int id { get; set; }
        public int compraid { get; set; }
        public Compra compra { get; set; }
        public int articuloid { get; set; }
        public Articulo articulo { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}
