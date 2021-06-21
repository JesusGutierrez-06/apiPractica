using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiPractica.Models
{
    public class Persona
    {
        [Key]
        public int idpersona { get; set; }
        public string nombre { get; set; }
        public int tipo_documento { get; set; }
        public string numero_documento { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public bool estado { get; set; }
    }
}
