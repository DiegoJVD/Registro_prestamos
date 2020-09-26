using Registro_prestamos;
using Microsoft.EntityFrameworkCore;
using Registro_prestamos.Entidades;

namespace Registro_prestamos.DAL
{
    
}
public class Contexto: DbContext{
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Prestamo> Prestamo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source= Data/Prestamos.db");
        }
}