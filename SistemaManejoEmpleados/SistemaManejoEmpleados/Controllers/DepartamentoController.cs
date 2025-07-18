using Microsoft.AspNetCore.Mvc;

namespace SistemaManejoEmpleados.Controllers
{
    public class DepartamentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
