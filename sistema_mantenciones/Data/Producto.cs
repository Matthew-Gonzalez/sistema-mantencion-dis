using System.Collections.Generic;

namespace sistema_mantenciones.Data
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<MantencionProducto> MantencionesProducto { get; set; }
    }
}