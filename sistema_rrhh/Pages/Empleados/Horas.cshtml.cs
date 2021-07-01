using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using sistema_rrhh.Data;

namespace sistema_rrhh.Pages.Empleados
{
    public class HorasModel : PageModel
    {
        // Conexion con la base de datos
        private readonly BaseDatos _baseDatos;
        // Empleado
        [BindProperty]
        public Empleado Empleado { get; set; }
        // Lista con los retiros
        public List<EmpleadoHora> EmpleadoHoras { get; set; }

        public HorasModel(BaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            // Verificamos si se recibio un id
            if (id == null)
            {
                return NotFound();
            }

            string rut = id.ToString();

            // Verificamos que un producto con el id exista
            Empleado = await _baseDatos.Empleados
                .FirstOrDefaultAsync(e => e.Rut == rut);

            if (Empleado == null)
            {
                return NotFound();
            }

            // Obtenemos los retiros del producto
            EmpleadoHoras = await _baseDatos.EmpleadosHoras
                .Where(eh => eh.EmpleadoRut == rut)
                .OrderByDescending(eh => eh.Fecha).ToListAsync();

            return Page();
        }
    }
}