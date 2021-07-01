using System.Collections.Generic;

namespace sistema_mantenciones.Data
{
    public class Empleado
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public List<MantencionEmpleado> MantencionesEmpleado { get; set; }
    }
}