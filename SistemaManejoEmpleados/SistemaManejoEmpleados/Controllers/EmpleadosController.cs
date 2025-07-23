//using SistemaManejoEmpleados.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Reflection.Metadata.Ecma335;
//using System;
//using Microsoft.EntityFrameworkCore;
//using SistemaManejoEmpleados.Data;

//namespace SistemaManejoEmpleados.Controllers
//{
//    public class EmpleadosController : Controller
//    {
//        private readonly ManejoempleadosContext _context;

//        public EmpleadosController(ManejoempleadosContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult Registrar()
//        {
//            var departamentos = _context.Departamentos.ToList();
//            var cargos = _context.Cargos.ToList();

//            if (departamentos == null || cargos == null)
//            {
//                return Content("¡Los datos de departamentos o cargos no están cargando!");
//            }

//            ViewBag.Departamentos = departamentos;
//            ViewBag.Cargos = cargos;

//            return View();
//            ;
//        }

//        [HttpPost]
//        public IActionResult Registrar(EmpleadoViewModel empleado)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Empleados.Add(empleado);
//                _context.SaveChanges();
//                TempData["Mensaje"] = "Empleado registrado satisfactoriamente";
//                return RedirectToAction("Lista");
//            }
//            return View("Index", empleado);
//        }

//        public IActionResult Lista (int ID)
//        {
//            var empleado = _context.Empleados
//            .Include(e => e.NOMBRE_DEPARTAMENTO)
//            .Include(e => e.NOMBRE_CARGO)
//            .FirstOrDefault(e => e.ID_EMPLEADO == ID);

//            return View(empleado);

//        }

//        public IActionResult Editar(int ID)
//        {
//            var empleado = _context.Empleados.Find(ID);
//            //ViewBag.Departamentos = _context.Departamentos.ToList();
//            //ViewBag.Cargos = _context.Cargos.ToList();
//            return View(empleado);
//        }

//        [HttpPost]
//        public IActionResult Editar(EmpleadoViewModel empleado)
//        {
//            _context.Empleados.Update(empleado);
//            _context.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        public IActionResult Eliminar(int ID)
//        {
//            var empleado = _context.Empleados.Find(ID);

//            if (empleado != null)
//            {
//                _context.Empleados.Remove(empleado);
//                _context.SaveChanges();
//            }

//            return RedirectToAction("Index");
//        }



//    }
//}
