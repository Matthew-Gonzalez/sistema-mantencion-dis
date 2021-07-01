using Microsoft.EntityFrameworkCore;

namespace sistema_rrhh.Data
{
    /// <summary>
    /// Establece conexion con la base de datos
    /// </summary>
    public class BaseDatos : DbContext
    {
        /// <summary>
        /// Define la conexion a la base de datos
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SistemaRRHHDIS;Integrated Security=true");
        }

        /// <summary>
        /// Configura las relaciones n:n y n:n:n
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Para la tabla Empleados definimos Rut como clave primaria
            modelBuilder.Entity<Empleado>().HasKey(em => new { em.Rut });

            // Para la tabla EmpleadosHoras definimos las claves primarias
            modelBuilder.Entity<EmpleadoHora>().HasKey(eh => new { eh.EmpleadoRut, eh.Fecha });
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<EmpleadoHora> EmpleadosHoras { get; set; }
    }
}