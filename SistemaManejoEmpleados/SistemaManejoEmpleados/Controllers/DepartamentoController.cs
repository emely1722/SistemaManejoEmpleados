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
    public class DepartamentoController : Controller
    {
        private readonly ManejoempleadosContext _context;

        public DepartamentoController(ManejoempleadosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar (DepartamentoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var nuevoDepartamento = new Departamento()
                {
                    NombreDepartamento = model.NOMBRE_DEPARTAMENTO
                };
            _context.Departamentos.Add(nuevoDepartamento);
            _context.SaveChanges();
            TempData["Mensaje"] = "Departamento Registrado.";
            return RedirectToAction("Lista");
            }
            return View("Index", model);
        }

        public IActionResult Lista()
        {
            var lista = _context.Departamentos
                .Select (d => new DepartamentoViewModel
                {
                    ID_DEPARTAMENTO = d.IdDepartamento,
                    NOMBRE_DEPARTAMENTO = d.NombreDepartamento
                }).ToList();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Editar (int id)
        {
            var departamento = _context.Departamentos.Find(id);
            if (departamento == null)
                return NotFound();

            var model = new DepartamentoViewModel
            {
                ID_DEPARTAMENTO = departamento.IdDepartamento,
                NOMBRE_DEPARTAMENTO = departamento.NombreDepartamento
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Editar (DepartamentoViewModel model)
        {
            if (ModelState.IsValid) 
                return View(model);

            var departamento = _context.Departamentos.Find(model.ID_DEPARTAMENTO);
            if (departamento == null)
                return NotFound();

            departamento.NombreDepartamento = model.NOMBRE_DEPARTAMENTO;
            _context.Departamentos.Update(departamento);
            _context.SaveChanges();

            TempData["Mensaje"] = "Departamento Actualizado.";
            return RedirectToAction("Lista");
        }

        public IActionResult Eliminar(int id)
        {
            var departamento = _context.Departamentos.FirstOrDefault(d => d.IdDepartamento == id);
            if (departamento != null)
            {
                _context.Departamentos.Remove(departamento);
                _context.SaveChanges();
            }

            TempData["Mensaje"] = "Departamento eliminado.";
            return RedirectToAction("Lista");
        }

        public ActionResult Exportar()
        {
            var departamentos = _context.Departamentos.ToList();

            var csv = new StringBuilder();
            csv.AppendLine("\"Id\",\"Nombre del departamento\"");

            foreach (var d in departamentos)
            {
                csv.AppendLine($"\"{d.IdDepartamento}\",\"{d.NombreDepartamento}\"");
            }

            var bom = Encoding.UTF8.GetPreamble();
            var csvBytes = Encoding.UTF8.GetBytes(csv.ToString());
            var finalBytes = bom.Concat(csvBytes).ToArray();

            return File(finalBytes, "text/csv", "Departamentos.csv");
        }
    }
}
