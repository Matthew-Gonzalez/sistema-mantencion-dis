using Microsoft.EntityFrameworkCore;

namespace sistema_bodega.Data
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
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SistemaBodegaDIS;Integrated Security=true");
        }

        /// <summary>
        /// Configura las relaciones n:n y n:n:n
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductoRetiro>().HasKey(pr => new { pr.ProductoId, pr.Fecha });
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoRetiro> ProductosRetiros { get; set; }
    }
}