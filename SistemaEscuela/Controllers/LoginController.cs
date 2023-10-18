using Microsoft.AspNetCore.Mvc;
using SistemaEscuela.Models;

namespace SistemaEscuela.Controllers
{
    public class LoginController : Controller
    {
        private SistemaEscuelaContext _context;

        public LoginController( SistemaEscuelaContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {  
            return View();
        }
        
        public ActionResult IniciarSesion(string Email, string Pass) 
        {
            try
            {
                var usuarioExiste = _context.Usuarios
                    .Where(x=>x.Email == Email && x.Pass == Pass)
                    .FirstOrDefault();

                if(usuarioExiste != null)
                {
                    Rol rol = _context.Rols.FirstOrDefault(x => x.Id == usuarioExiste.IdRol);

                    string Rol = rol.Nombre;
                    int RolId = rol.Id;

                    HttpContext.Session.SetString("RolActual", Rol);                    

                    return Json("Iniciando Sesion...");
                }
                return Json("No se ha podido iniciar sesión.");
            }
            catch (Exception ex)
            {
                return View("Ocurrió un error: "+ ex);
            }
        }
    }
}
