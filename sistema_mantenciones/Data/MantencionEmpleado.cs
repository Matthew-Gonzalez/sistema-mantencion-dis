namespace sistema_mantenciones.Data
{
    public class MantencionEmpleado
    {
        public int MantencionId { get; set; }
        public Mantencion Mantencion { get; set; }
        public string EmpleadoRut { get; set; }
        public Empleado Empleado { get; set; }
        public int Horas { get; set; }
    }
}