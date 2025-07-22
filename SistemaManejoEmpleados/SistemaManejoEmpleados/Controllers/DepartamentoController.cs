using SistemaManejoEmpleados.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System;
using Microsoft.EntityFrameworkCore;
using SistemaManejoEmpleados.Data;

namespace SistemaManejoEmpleados.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly ManejoempleadosContext _context;

        public DepartamentoController(ManejoempleadosContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var DEPARTAMENTOS = _context.Departamentos.ToList();
            return View(DEPARTAMENTOS);
        }
    }
}
