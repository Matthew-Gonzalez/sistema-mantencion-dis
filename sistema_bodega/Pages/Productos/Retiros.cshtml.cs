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
    public class RetirosModel : PageModel
    {
        // Conexion con la base de datos
        private readonly BaseDatos _baseDatos;
        // El producto
        public Producto Producto { get; set; }
        // Lista con los retiros
        public List<ProductoRetiro> ProductoRetiros { get; set; }

        public RetirosModel(BaseDatos baseDatos)
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

            // Verificamos que un producto con el id exista
            Producto = await _baseDatos.Productos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Producto == null)
            {
                return NotFound();
            }

            // Obtenemos los retiros del producto
            ProductoRetiros = await _baseDatos.ProductosRetiros
                .Where(pr => pr.ProductoId == id)
                .OrderByDescending(pr => pr.Fecha).ToListAsync();

            return Page();
        }
    }
}