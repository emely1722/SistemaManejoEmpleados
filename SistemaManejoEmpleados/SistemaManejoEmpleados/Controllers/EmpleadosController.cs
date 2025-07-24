using SistemaManejoEmpleados.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleados.Data;
using System.Security.Cryptography.Xml;
using System.Text;

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
            ViewBag.Departamentos = _context.Departamentos.ToList();
            ViewBag.Cargos = _context.Cargos.ToList();  
            return View(new EmpleadoViewModel());
        }

        [HttpPost]
        public IActionResult Registrar(EmpleadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Departamentos = _context.Departamentos.ToList();
                ViewBag.Cargos = _context.Cargos.ToList(); 
                
                
                var nuevo = new EmpleadoViewModel()
            {
                NOMBRE_EMPLEADO = model.NOMBRE_EMPLEADO,
                CEDULA_EMPLEADO = model.CEDULA_EMPLEADO,
                GENERO = model.GENERO,
                TELEFONO_EMPLEADO = model.TELEFONO_EMPLEADO,
                FECHA_INICIO = model.FECHA_INICIO,
                SALARIO_EMPLEADO = model.SALARIO_EMPLEADO,
                ESTADO = model.ESTADO,
                ID_DEPARTAMENTO = model.ID_DEPARTAMENTO,
                ID_CARGO = model.ID_CARGO
            };
                _context.Empleados.Add(nuevo);
            _context.SaveChanges();
            TempData["Mensaje"] = "Empleado registrado correctamente";
            return RedirectToAction("Lista");
            }return View("Index", model);

            

            
        }

        [HttpGet]
        public IActionResult Lista()
        {
            var empleados = _context.Empleados.ToList();
            return View(empleados);
        }

        [HttpGet]
        public IActionResult Editar (int id)
        {
            if (ModelState.IsValid)
            {
                var e = _context.Empleados
                .Include(x => x.ID_DEPARTAMENTO)
                .Include(x => x.ID_CARGO)
                .Include(x => x.ID_EMPLEADO == id);
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

            e.NOMBRE_EMPLEADO = model.NOMBRE_EMPLEADO;
            e.CEDULA_EMPLEADO = model.CEDULA_EMPLEADO;
            e.GENERO = model.GENERO;
            e.TELEFONO_EMPLEADO = model.TELEFONO_EMPLEADO;
            e.FECHA_INICIO = model.FECHA_INICIO;
            e.SALARIO_EMPLEADO = model.SALARIO_EMPLEADO;
            e.ESTADO = model.ESTADO;
            e.ID_DEPARTAMENTO = model.ID_DEPARTAMENTO;
            e.ID_CARGO = model.ID_CARGO;

            _context.SaveChanges();
            TempData["Mensaje"] = "Empleado actualizado correctamente";
            return RedirectToAction("Lista");
        }

        public IActionResult Eliminar(int id)
        {
            var e = _context.Empleados.FirstOrDefault(x => x.ID_EMPLEADO == id);
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
                .Include(e => e.ID_DEPARTAMENTO)
                .Include(e => e.ID_CARGO)
                .Select(e => new
                {
                    e.ID_EMPLEADO,
                    e.NOMBRE_EMPLEADO,
                    e.CEDULA_EMPLEADO,
                    e.GENERO,
                    e.TELEFONO_EMPLEADO,
                    FECHA_INICIO = e.FECHA_INICIO.ToString("yyyy-MM-dd"),
                    e.SALARIO_EMPLEADO,
                    ESTADO = e.ESTADO ? "Activo" : "Inactivo",
                    DEPARTAMENTO = e.ID_DEPARTAMENTO,
                    CARGO = e.ID_CARGO
                }).ToList();

            var csv = new System.Text.StringBuilder();
            csv.AppendLine("\"ID\",\"Nombre\",\"Cédula\",\"Género\",\"Teléfono\",\"Fecha de Inicio\",\"Salario\",\"Estado\",\"Departamento\",\"Cargo\"");

            foreach (var e in empleados)
            {
                csv.AppendLine($"\"{e.ID_EMPLEADO}\",\"{e.NOMBRE_EMPLEADO}\",\"{e.CEDULA_EMPLEADO}\",\"{e.GENERO}\",\"{e.TELEFONO_EMPLEADO}\",\"{e.FECHA_INICIO}\",\"{e.SALARIO_EMPLEADO}\",\"{e.ESTADO}\",\"{e.DEPARTAMENTO}\",\"{e.CARGO}\"");
            }

            var bom = System.Text.Encoding.UTF8.GetPreamble();
            var bytes = System.Text.Encoding.UTF8.GetBytes(csv.ToString());
            var final = bom.Concat(bytes).ToArray();

            return File(final, "text/csv", "Empleados.csv");
        }



    }
}