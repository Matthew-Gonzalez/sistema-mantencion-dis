using System.Collections.Generic;

namespace sistema_rrhh.Data
{
    public class Empleado
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public List<EmpleadoHora> EmpleadoHoras { get; set; }
    }
}