using Microsoft.AspNetCore.Mvc;
using SistemaEscuela.Models;

namespace SistemaEscuela.Controllers
{
    public class AdminController : Controller
    {
        private SistemaEscuelaContext _context;

        public AdminController( SistemaEscuelaContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var roles = ObtenerRoles();
            return View(roles);
        }

        public List<Rol> ObtenerRoles()
        {
            List<Rol> roles = new List<Rol>();

            var rolesBD = _context.Rols.Where(x => x.Bhabilitado == true).ToList();

            foreach (var role in rolesBD)
            {
                roles.Add(new Rol { Id = role.Id, Nombre = role.Nombre });
            }

            return roles;
        }

        public JsonResult RegistrarUsuario(string Email,string Pass, int IdRol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var UsuarioExiste = _context.Usuarios
                    .Where(x => x.Email == Email)
                    .FirstOrDefault();

                    if (UsuarioExiste != null) return Json($"El usuario con el Email: {Email}, ya existe.");

                    Usuario usuarioNuevo = new Usuario()
                    {
                        Email = Email,
                        IdRol = IdRol,
                        Pass = Pass,
                        Bhabilitado = true
                    };

                    _context.Usuarios.Add(usuarioNuevo);
                    _context.SaveChanges();

                    return Json("Se ha agregado el usuario, de manera correcta.");
                }
                return Json("Los campos no son correctos.");
            }
            catch (Exception ex)
            {
                return Json($"Ocurrió un error: {ex}");
            }
        }
    }
}
