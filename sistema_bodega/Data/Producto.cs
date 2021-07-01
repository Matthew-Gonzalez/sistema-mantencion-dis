using System.Collections.Generic;

namespace sistema_bodega.Data
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public List<ProductoRetiro> ProductoRetiros { get; set; }
    }
}