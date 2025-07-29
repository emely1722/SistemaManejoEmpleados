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
    public class CargoController : Controller
    {
        private readonly ManejoempleadosContext _context;

        public CargoController(ManejoempleadosContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new CargoViewModel());
        }

        [HttpPost]
        public IActionResult Registrar (CargoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var nuevoCargo = new Cargo()
                {
                    NombreCargo = model.NOMBRE_CARGO
                };
                _context.Cargos.Add(nuevoCargo);
                _context.SaveChanges();
                TempData["Mensaje"] = "Cargo Registrado";
                return RedirectToAction("Lista");
            }

            return View("Index", model);
        }

        public IActionResult Lista()
        {
            var lista = _context.Cargos
               .Select(d => new CargoViewModel
               {
                   ID_CARGO = d.IdCargo,
                   NOMBRE_CARGO = d.NombreCargo
               }).ToList();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Editar(int id) 
        {
            var cargos = _context.Cargos.Find(id);
            if (cargos == null)
                return NotFound();

            var model = new CargoViewModel
            {
                ID_CARGO = cargos.IdCargo,
                NOMBRE_CARGO = cargos.NombreCargo
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Editar(CargoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var cargos = _context.Cargos.Find(model.ID_CARGO);
            if (cargos == null)
                return NotFound();

            cargos.NombreCargo = model.NOMBRE_CARGO;
            _context.SaveChanges();

            TempData["Mensaje"] = "Cargo Actualizado.";
            return RedirectToAction("Lista");
        }

        public IActionResult Eliminar(int id)
        {
            var cargos = _context.Cargos.FirstOrDefault(d => d.IdCargo == id);
            if (cargos != null)
            {
                _context.Cargos.Remove(cargos);
                _context.SaveChanges();
            }

            TempData["Mensaje"] = "Cargo eliminado.";
            return RedirectToAction("Lista");
        }

        public ActionResult Exportar()
        {
            var cargos = _context.Cargos.ToList();

            var csv = new StringBuilder();
            csv.AppendLine("\"ID\",\"Nombre del Cargo\"");

            foreach (var c in cargos)
            {
                csv.AppendLine($"\"{c.IdCargo}\",\"{c.NombreCargo}\"");
            }

            var bom = Encoding.UTF8.GetPreamble();
            var csvBytes = Encoding.UTF8.GetBytes(csv.ToString());
            var finalBytes = bom.Concat(csvBytes).ToArray();

            return File(finalBytes, "text/csv", "Cargos.csv");
        }

    }
}
