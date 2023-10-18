using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscuela.Models;
using System.Diagnostics;

namespace SistemaEscuela.Controllers
{
    public class HomeController : Controller
    {
        private Models.SistemaEscuelaContext _context;

        public HomeController(SistemaEscuelaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string miValor = HttpContext.Session.GetString("RolActual");

            ViewBag.Rol = miValor;

            if(miValor == null)
            {
                RedirectToAction("Index","Login");
            }

            return View();
        }

        public JsonResult ObtenerAlumnos()
        {
            var Alumnos = _context.Alumnos.FirstOrDefault();

            if (Alumnos == null) return Json("No se encontraron alumnos");

            return Json(Alumnos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}