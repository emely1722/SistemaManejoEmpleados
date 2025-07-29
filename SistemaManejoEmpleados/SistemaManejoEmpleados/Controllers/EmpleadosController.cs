using SistemaManejoEmpleados.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleados.Data;
using System.Security.Cryptography.Xml;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SistemaManejoEmpleados.Controllers
{
    public class EmpleadosController  :  Controller
    {
        public readonly ManejoempleadosContext _context;

        public EmpleadosController(ManejoempleadosContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new EmpleadoViewModel()
            {
                Departamentos = _context.Departamentos
                .Select(d => new SelectListItem
                {
                    Value = d.IdDepartamento.ToString(),
                    Text = d.NombreDepartamento
                }).ToList(),

                Cargos = _context.Cargos
                .Select(c => new SelectListItem 
                {
                   Value = c.IdCargo.ToString(),
                   Text = c.NombreCargo
                }).ToList()

            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Registrar(EmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var nuevoempleado = new Empleado();
                {
                    nuevoempleado.NombreEmpleado = model.NOMBRE_EMPLEADO;
                    nuevoempleado.CedulaEmpleado = model.CEDULA_EMPLEADO;
                    nuevoempleado.Genero = model.GENERO;
                    nuevoempleado.TelefonoEmpleado = model.TELEFONO_EMPLEADO;
                    nuevoempleado.FechaInicio = model.FECHA_INICIO;
                    nuevoempleado.SalarioEmpleado = model.SALARIO_EMPLEADO;
                    nuevoempleado.Estado = model.ESTADO;
                    nuevoempleado.IdDepartamento = model.ID_DEPARTAMENTO;
                    nuevoempleado.IdCargo = model.ID_CARGO;
                };
            _context.Empleados.Add(nuevoempleado);
            _context.SaveChanges();
            TempData["Mensaje"] = "Empleado registrado correctamente";
            return RedirectToAction("Lista");
            }
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Lista()
        {
			var lista = _context.Empleados
	        .Include(e => e.IdCargoNavigation)
	        .Include(e => e.IdDepartamentoNavigation)
	        .Select(e => new EmpleadoViewModel
	        {
		        ID_EMPLEADO = e.IdEmpleado,
		        NOMBRE_EMPLEADO = e.NombreEmpleado,
		        CEDULA_EMPLEADO = e.CedulaEmpleado,
		        GENERO = e.Genero,
		        TELEFONO_EMPLEADO = e.TelefonoEmpleado,
		        FECHA_INICIO = e.FechaInicio,
		        SALARIO_EMPLEADO = e.SalarioEmpleado,
		        ESTADO = e.Estado,
		        ID_DEPARTAMENTO = e.IdDepartamento,
		        ID_CARGO = e.IdCargo,
		        NOMBRE_CARGO = e.IdCargoNavigation.NombreCargo,
		        NOMBRE_DEPARTAMENTO = e.IdDepartamentoNavigation.NombreDepartamento
	        }).ToList();

			return View(lista);
        }

        [HttpGet]
        public IActionResult Editar (int id)
        {
            if (ModelState.IsValid)
            {
                var e = _context.Empleados
                .Include(x => x.IdDepartamento)
                .Include(x => x.IdCargo)
                .Include(x => x.IdEmpleado == id);
            }            
            return View();            
        }

        [HttpPost]
        public IActionResult Editar(EmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departamentos = _context.Departamentos.ToList();
                ViewBag.Cargos = _context.Cargos.ToList();
                return View(model);
            }

            var e = _context.Empleados.Find(model.ID_EMPLEADO);
            if (e == null) return NotFound();

            e.NombreEmpleado = model.NOMBRE_EMPLEADO;
            e.CedulaEmpleado = model.CEDULA_EMPLEADO;
            e.Genero = model.GENERO;
            e.TelefonoEmpleado = model.TELEFONO_EMPLEADO;
            e.FechaInicio = model.FECHA_INICIO;
            e.SalarioEmpleado = model.SALARIO_EMPLEADO;
            e.Estado = model.ESTADO;
            e.IdDepartamento = model.ID_DEPARTAMENTO;
            e.IdCargo = model.ID_CARGO;

            _context.SaveChanges();
            TempData["Mensaje"] = "Empleado actualizado correctamente";
            return RedirectToAction("Lista");
        }

        public IActionResult Eliminar(int id)
        {
            var e = _context.Empleados.FirstOrDefault(x => x.IdEmpleado == id);
            if (e != null)
            {
                _context.Empleados.Remove(e);
                _context.SaveChanges();
                TempData["Mensaje"] = "Empleado eliminado.";
            }

            return RedirectToAction("Lista");
        }

        public IActionResult Exportar()
        {
            var empleados = _context.Empleados
                .Include(e => e.IdDepartamento)
                .Include(e => e.IdCargo)
                .Select(e => new
                {
                    e.IdEmpleado,
                    e.NombreEmpleado,
                    e.CedulaEmpleado,
                    e.Genero,
                    e.TelefonoEmpleado,
                    FECHA_INICIO = e.FechaInicio.ToString("yyyy-MM-dd"),
                    e.SalarioEmpleado,
                    ESTADO = e.Estado ? "Activo" : "Inactivo",
                    DEPARTAMENTO = e.IdDepartamento,
                    CARGO = e.IdCargo
                }).ToList();

            var csv = new System.Text.StringBuilder();
            csv.AppendLine("\"ID\",\"Nombre\",\"Cédula\",\"Género\",\"Teléfono\",\"Fecha de Inicio\",\"Salario\",\"Estado\",\"Departamento\",\"Cargo\"");

            foreach (var e in empleados)
            {
                csv.AppendLine($"\"{e.IdEmpleado}\",\"{e.NombreEmpleado}\",\"{e.CedulaEmpleado}\",\"{e.Genero}\",\"{e.TelefonoEmpleado}\",\"{e.FECHA_INICIO}\",\"{e.SalarioEmpleado}\",\"{e.ESTADO}\",\"{e.DEPARTAMENTO}\",\"{e.CARGO}\"");
            }

            var bom = System.Text.Encoding.UTF8.GetPreamble();
            var bytes = System.Text.Encoding.UTF8.GetBytes(csv.ToString());
            var final = bom.Concat(bytes).ToArray();

            return File(final, "text/csv", "Empleados.csv");
        }



    }
}