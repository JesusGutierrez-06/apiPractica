using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace apiPractica.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Persona> persona { get; set; }
        public DbSet<Tipo_Usuario> tipo_usuario { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Categoria> categoria { get; set; }
        public DbSet<Articulo> articulo { get; set; }
        public DbSet<Detalle_Venta> detalle_venta { get; set; }
        public DbSet<Venta> venta { get; set; }
        public DbSet<Detalle_Compra> detalle_compra { get; set; }
        public DbSet<Compra> compra { get; set; }
    }
}
