using System;
using System.Collections.Generic;

namespace sistema_mantenciones.Data
{
    public class Mantencion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public List<MantencionProducto> MantencionProductos { get; set; }
        public List<MantencionEmpleado> MantencionEmpleados { get; set; }
    }
}