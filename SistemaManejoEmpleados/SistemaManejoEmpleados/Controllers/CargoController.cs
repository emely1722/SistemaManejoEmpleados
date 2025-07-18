using Microsoft.AspNetCore.Mvc;

namespace SistemaManejoEmpleados.Controllers
{
    public class CargoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
