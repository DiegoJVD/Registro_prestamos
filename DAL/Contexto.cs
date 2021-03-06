
using Microsoft.EntityFrameworkCore;
using Registro_prestamos.Entidades;

namespace Registro_prestamos.DAL
{
    public class Contexto : DbContext{
        public DbSet<Personas> Personas { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
        public DbSet<Moras> Moras { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source=Data/Prestamos.db");
        }
    }
}