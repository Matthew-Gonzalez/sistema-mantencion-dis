using Microsoft.EntityFrameworkCore;

namespace sistema_mantenciones.Data
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
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SistemaMantencionDIS;Integrated Security=true");
        }

        /// <summary>
        /// Configura las relaciones n:n y n:n:n
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Para la tabla Empleados definimos Rut como clave primaria
            modelBuilder.Entity<Empleado>().HasKey(em => new { em.Rut });

            // Para la tabla MantencionProducto
            modelBuilder.Entity<MantencionProducto>().HasKey(mp => new { mp.MantencionId, mp.ProductoId });

            // Para la tabla MantencionEmpleado
            modelBuilder.Entity<MantencionEmpleado>().HasKey(me => new { me.MantencionId, me.EmpleadoRut });
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Mantencion> Mantenciones { get; set; }
        public DbSet<MantencionProducto> MantencionesProductos { get; set; }
        public DbSet<MantencionEmpleado> MantencionesEmpleados { get; set; }
    }
}