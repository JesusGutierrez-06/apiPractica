using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiPractica.Models
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }
        public int tipo_usuarioid { get; set; }
        public Tipo_Usuario tipo_usuario { get; set; }
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool estado { get; set; }
    }
}
