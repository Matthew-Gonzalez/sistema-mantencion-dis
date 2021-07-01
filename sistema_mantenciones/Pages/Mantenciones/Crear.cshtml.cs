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
using sistema_mantenciones.RabbitMQ;

namespace sistema_mantenciones.Pages.Mantenciones
{
    /// <summary>
    /// Modelo de la vista Crear de mantenciones
    /// </summary>
    public class CrearModel : PageModel
    {
        // Conexion a la base de datos
        private readonly BaseDatos _baseDatos;
        // Lista con los empleados a escoger
        public SelectList Empleados { get; set; }
        // Lista con los productos a escoger
        public SelectList Productos { get; set; }
        // Mantencion que acabamos de crear
        [BindProperty]
        public Mantencion Mantencion { get; set; }
        // Los empleados actuales 
        public List<MantencionEmpleado> MantencionEmpleados { get; set; }
        // Los productos actuales
        public List<MantencionProducto> MantencionProductos { get; set; }

        public CrearModel(BaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        /// <summary>
        /// Cuando se carga por primera vez la pagina
        /// </summary>
        /// <param name="id">ID de la mantencion en caso de agregar mas empleados y/o productos</param>
        /// <returns></returns>
        public async Task<IActionResult> OnGet(int? id)
        {
            // Se verifica si vamos a trabajar sobre una mantencion existente o una nueva
            if (id != null)
            {
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

                // Se cargan las listas para escoger omitiendo los que ya existen
                List<Empleado> tempE = await _baseDatos.Empleados
                    .Include(e => e.MantencionesEmpleado)
                    .Where(me => !me.MantencionesEmpleado.Any(e => e.MantencionId == id)).ToListAsync();

                Empleados = new SelectList(tempE
                    .Select(e => new
                    {
                        Rut = e.Rut,
                        RutNombre = e.Rut + " - " + e.Nombre
                    })
                    , "Rut", "RutNombre");

                List<Producto> tempP = await _baseDatos.Productos
                    .Include(p => p.MantencionesProducto)
                    .Where(me => !me.MantencionesProducto.Any(e => e.MantencionId == id)).ToListAsync();

                Productos = new SelectList(tempP, nameof(Producto.Id), nameof(Producto.Nombre));
            }
            else
            {
                // Se cargan las listas de donde se escogeran a los empleados y productos
                List<Empleado> tempE = await _baseDatos.Empleados
                    .OrderBy(e => e.Rut).ToListAsync();

                Empleados = new SelectList(tempE
                    .Select(e => new
                    {
                        Rut = e.Rut,
                        RutNombre = e.Rut + " - " + e.Nombre
                    })
                    , "Rut", "RutNombre");

                List<Producto> tempP = await _baseDatos.Productos.ToListAsync();

                Productos = new SelectList(tempP, nameof(Producto.Id), nameof(Producto.Nombre));
            }

            return Page();
        }

        /// <summary>
        /// Crea una nueva mantencion
        /// </summary>
        /// <param name="fecha">La fecha de la mantencion</param>
        /// <param name="rut_empleado">El RUT del primer empleado</param>
        /// <param name="horas">Cuantas horas trabajo el empleado</param>
        /// <param name="id_producto">El ID del primer producto</param>
        /// <param name="cantidad">La cantidad que se uso de producto</param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostNueva(DateTime fecha, string observaciones, string rut_empleado, int horas, int id_producto, int cantidad)
        {
            // Se crean y configura una nueva mantencion
            Mantencion mantencion = new Mantencion();
            mantencion.Fecha = fecha;
            mantencion.Observaciones = observaciones;

            // Se guarda la mantencion para obtener su ID
            _baseDatos.Mantenciones.Add(mantencion);
            await _baseDatos.SaveChangesAsync();

            // Se crea una nueva relacion MantencionEmpleado
            MantencionEmpleado mantencionEmpleado = new MantencionEmpleado();
            mantencionEmpleado.MantencionId = mantencion.Id;
            mantencionEmpleado.EmpleadoRut = rut_empleado;
            mantencionEmpleado.Horas = horas;

            // Se guarda la relacion
            _baseDatos.MantencionesEmpleados.Add(mantencionEmpleado);
            await _baseDatos.SaveChangesAsync();

            // Se manda un JSON a la cola de empleados
            QueueProducer.PublicarMantencion(fecha, mantencionEmpleado);

            // Se crea una nueva relacion MantencionProducto
            MantencionProducto mantencionProducto = new MantencionProducto();
            mantencionProducto.MantencionId = mantencion.Id;
            mantencionProducto.ProductoId = id_producto;
            mantencionProducto.Cantidad = cantidad;

            // Se guarda la relacion
            _baseDatos.MantencionesProductos.Add(mantencionProducto);
            await _baseDatos.SaveChangesAsync();

            // Se manda un JSON a la cola de productos
            QueueProducer.PublicarMantencion(fecha, mantencionProducto);

            // Se retorna este mismo index con el ID de la mantencion que se acaba de crear
            return RedirectToPage("./Crear", new { id = mantencion.Id });
        }

        public async Task<IActionResult> OnPostActualizar(string rut_empleado, int horas, int id_producto, int cantidad, int id_mantencion, DateTime fecha)
        {
            // Verificamos si debemos registrar un nuevo empleado
            if (!string.IsNullOrEmpty(rut_empleado))
            {
                // Se crea una nueva relacion MantencionEmpleado
                MantencionEmpleado mantencionEmpleado = new MantencionEmpleado();
                mantencionEmpleado.MantencionId = id_mantencion;
                mantencionEmpleado.EmpleadoRut = rut_empleado;
                mantencionEmpleado.Horas = horas;

                // Se guarda la relacion
                _baseDatos.MantencionesEmpleados.Add(mantencionEmpleado);
                await _baseDatos.SaveChangesAsync();

                // Se manda un JSON a la cola de empleados
                QueueProducer.PublicarMantencion(fecha, mantencionEmpleado);
            }

            // Verificamos si debemos registrar un nuevo producto
            if (id_producto > 0)
            {
                // Se crea una nueva relacion MantencionProducto
                MantencionProducto mantencionProducto = new MantencionProducto();
                mantencionProducto.MantencionId = id_mantencion;
                mantencionProducto.ProductoId = id_producto;
                mantencionProducto.Cantidad = cantidad;

                // Se guarda la relacion
                _baseDatos.MantencionesProductos.Add(mantencionProducto);
                await _baseDatos.SaveChangesAsync();

                // Se manda un JSON a la cola de productos
                QueueProducer.PublicarMantencion(fecha, mantencionProducto);
            }

            // Se retorna este mismo index con el ID de la mantencion que estamos modificando
            return RedirectToPage("./Crear", new { id = id_mantencion });
        }
    }
}