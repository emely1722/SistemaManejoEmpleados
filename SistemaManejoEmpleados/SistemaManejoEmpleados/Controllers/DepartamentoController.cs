using SistemaManejoEmpleados.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleados.Data;
using System.Security.Cryptography.Xml;

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
            return View(new DepartamentoViewModel());
        }

        [HttpPost]
        public IActionResult Registrar (DepartamentoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var nuevoDepartamento = new DepartamentoViewModel
                {

                    NOMBRE_DEPARTAMENTO = model.NOMBRE_DEPARTAMENTO
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
                    ID_DEPARTAMENTO = d.ID_DEPARTAMENTO,
                    NOMBRE_DEPARTAMENTO = d.NOMBRE_DEPARTAMENTO
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
                ID_DEPARTAMENTO = departamento.ID_DEPARTAMENTO,
                NOMBRE_DEPARTAMENTO = departamento.NOMBRE_DEPARTAMENTO
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Editar (DepartamentoViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            var departamento = _context.Departamentos.Find(model.ID_DEPARTAMENTO);
            if (departamento == null)
                return NotFound();

            departamento.NOMBRE_DEPARTAMENTO = model.NOMBRE_DEPARTAMENTO;
            _context.SaveChanges();

            TempData["Mensaje"] = "Departamento Actualizado.";
            return RedirectToAction("Lista");
        }

        public IActionResult Eliminar(int id)
        {
            var departamento = _context.Departamentos.FirstOrDefault(d => d.ID_DEPARTAMENTO == id);
            if (departamento != null)
            {
                _context.Departamentos.Remove(departamento);
                _context.SaveChanges();
            }

            TempData["Mensaje"] = "Departamento eliminado.";
            return RedirectToAction("Lista");
        }

    }
}
