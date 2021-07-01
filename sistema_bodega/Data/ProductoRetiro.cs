using System;

namespace sistema_bodega.Data
{
    public class ProductoRetiro
    {
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
    }
}