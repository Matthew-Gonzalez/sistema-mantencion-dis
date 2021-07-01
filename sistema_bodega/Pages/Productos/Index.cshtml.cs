using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using sistema_bodega.Data;

namespace sistema_bodega.Pages.Productos
{
    // Modelo de la vista Index de los productos
    public class IndexModel : PageModel
    {
        // Conexion a la base de datos
        private readonly BaseDatos _baseDatos;
        // Lista con los productos
        public List<Producto> Productos { get; set; }

        public IndexModel(BaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public async Task<IActionResult> OnGet()
        {
            // Obtenemos los productos
            Productos = await _baseDatos.Productos.ToListAsync();

            return Page();
        }
    }
}