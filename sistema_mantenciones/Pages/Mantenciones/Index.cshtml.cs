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
    /// Modelo de la vista Index de mantenciones 
    /// </summary>
    public class IndexModel : PageModel
    {
        // Conexion con la base de datos
        private readonly BaseDatos _baseDatos;

        // Lista de mantenciones a desplegar
        public List<Mantencion> Mantenciones { get; set; }

        public IndexModel(BaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public async Task<IActionResult> OnGet()
        {
            // Se obtiene la lista de mantenciones
            Mantenciones = await _baseDatos.Mantenciones
                .OrderByDescending(m => m.Fecha).ToListAsync();

            return Page();
        }
    }
}