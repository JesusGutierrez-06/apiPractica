using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiPractica.Models
{
    public class Tipo_Usuario
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
