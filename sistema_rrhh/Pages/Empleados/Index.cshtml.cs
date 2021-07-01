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
    // Modelo de la vista Index de los productos
    public class IndexModel : PageModel
    {
        // Conexion a la base de datos
        private readonly BaseDatos _baseDatos;
        // Lista con los productos
        public List<TempEmpleado> TempEmpleados { get; set; }

        public IndexModel(BaseDatos baseDatos)
        {
            _baseDatos = baseDatos;
        }

        public async Task<IActionResult> OnGet()
        {
            // Obtenemos los empleados
            List<Empleado> empleados = await _baseDatos.Empleados.ToListAsync();
            // Obtenemos la relacion EmpleadoHora para las horas de trabajo
            List<EmpleadoHora> empleadosHoras = await _baseDatos.EmpleadosHoras
                .OrderBy(eh => eh.EmpleadoRut).ToListAsync();

            // Creamos y poblamos una lista de TempEmpleado para tener la cantidad total de horas trabajadas por cada empleado
            TempEmpleados = new List<TempEmpleado>();

            foreach (Empleado empleado in empleados)
            {
                TempEmpleado temp = new TempEmpleado();
                temp.Empleado = empleado;

                foreach (EmpleadoHora empleadoHora in empleadosHoras)
                {
                    if (empleadoHora.EmpleadoRut == empleado.Rut)
                    {
                        temp.HorasTrabajo += empleadoHora.Horas;
                    }
                }

                TempEmpleados.Add(temp);
            }

            return Page();
        }
    }

    /// <summary>
    /// Clase temporal para unir al empleado y sus horas de trabajo
    /// </summary>
    public class TempEmpleado
    {
        public Empleado Empleado { get; set; }
        public int HorasTrabajo { get; set; }
    }
}