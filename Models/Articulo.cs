using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiPractica.Models
{
    public class Articulo
    {
        [Key]
        public int id { get; set; }
        public int categoriaid { get; set; }
        public Categoria categoria { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal precio_venta { get; set; }
        public int stock { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
