using System;

namespace sistema_rrhh.Data
{
    public class EmpleadoHora
    {
        public string EmpleadoRut { get; set; }
        public Empleado Empleado { get; set; }
        public DateTime Fecha { get; set; }
        public int Horas { get; set; }
    }
}