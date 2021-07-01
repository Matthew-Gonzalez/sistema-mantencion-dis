using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using sistema_mantenciones.Data;

namespace sistema_mantenciones.Pages.Mantenciones
{
    /// <summary>
    /// Modelo de la vista Detalle de mantenciones 
    /// </summary>
    public class DetalleModelo : PageModel
    {
        // Conexion a la base de datos
        private readonly BaseDatos _baseDatos;

        // Mantencion
        [BindProperty]
        public Mantencion Mantencion { get; set; }
        // Lista con los empleados que participaron
        public List<MantencionEmpleado> MantencionEmpleados { get; set; }
        // Lista con los productos que se utilizaron
        public List<MantencionProducto> MantencionProductos { get; set; }

        public DetalleModelo(BaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            // Revisamos si se mando una ID
            if (id == null)
            {
                return NotFound();
            }

            // Revisamos que la mantencion exista
            Mantencion = await _baseDatos.Mantenciones
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Mantencion == null)
            {
                return NotFound();
            }

            // Se carga la lista con los empleados de la mantencion
            MantencionEmpleados = await _baseDatos.MantencionesEmpleados
                .Where(me => me.MantencionId == id)
                .Include(me => me.Empleado).ToListAsync();

            // Se carga la lista con los productos
            MantencionProductos = await _baseDatos.MantencionesProductos
                .Where(mp => mp.MantencionId == id)
                .Include(mp => mp.Producto).ToListAsync();

            return Page();
        }
    }
}