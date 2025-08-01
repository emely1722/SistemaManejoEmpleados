using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaManejoEmpleados.Models;

namespace SistemaManejoEmpleados.Controllers
{
    public class NominaController : Controller
    {
        private readonly ManejoempleadosContext _context;

        public NominaController(ManejoempleadosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Empleados = _context.Empleados
                .Select(e => new SelectListItem
                {
                    Value = e.IdEmpleado.ToString(),
                    Text = e.NombreEmpleado
                }).ToList();
            return View(new NominaViewModel());
        }

        [HttpPost]
        public IActionResult Index(int ID_EMPLEADO)
        {
            var empleado = _context.Empleados
                .FirstOrDefault(e => e.IdEmpleado == ID_EMPLEADO);

            ViewBag.Empleados = _context.Empleados
                .Select(e => new SelectListItem
                {
                    Value = e.IdEmpleado.ToString(),
                    Text = e.NombreEmpleado
                }).ToList();

            if (empleado == null)
            {
                TempData["Advertencia"] = "DEBE SELECCIONAR UN EMPLEADO";
                return View();
            }



            var model = new NominaViewModel
            {
                ID_EMPLEADO = empleado.IdEmpleado,
                NOMBRE_EMPLEADO = empleado.NombreEmpleado,
                SALARIO_EMPLEADO = empleado.SalarioEmpleado,
                FECHA_INGRESO = empleado.FechaInicio
            };

            TempData["Exito"] = "Cálculo ya fue realizado";
            return View("Index", model);
        }
    }

}
