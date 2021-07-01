namespace sistema_mantenciones.Data
{
    public class MantencionProducto
    {
        public int MantencionId { get; set; }
        public Mantencion Mantencion { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}