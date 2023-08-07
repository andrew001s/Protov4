using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Protov4.DTO;
using Protov4.DAO;
using System.Security.Claims;

namespace Protov4.Controllers
{
    public class AccesoController : Controller
    {
        private readonly UsuariosDAO _usuariosDAO;

        public AccesoController(UsuariosDAO usuariosDAO)
        {
            _usuariosDAO = usuariosDAO;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuariosDTO user)
        {
            try
            {
                (int id_usuario, int id_rol_user) = _usuariosDAO.ValidarUsuario(user);
                if (id_usuario != 0)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.correo_elec),
                        new Claim("id_usuario", id_usuario.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    //HttpContext.Session.SetInt32("id_usuario", id_usuario);

                    if (id_rol_user == 1) // Administrador
                    {
                        return RedirectToAction("Admin", "Administrador");
                    }
                    else // Usuario normal
                    {
                        //_usuariosDAO.RegistrarAuditoria(id_usuario, DateTime.Now, null);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Error = "Credenciales incorrectas";
                }
                return View();
            }
            catch (System.Exception)
            {
                ViewBag.Error = "Credenciales incorrectas";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            // Obtener el id_usuario almacenado en la variable de sesión
            //int? id_usuario = HttpContext.Session.GetInt32("id_usuario");
            //var idUsuarioClaim = User.FindFirst("id_usuario");
            //if (idUsuarioClaim != null && int.TryParse(idUsuarioClaim.Value, out int id_usuario))
            //{
            //    // Llamar al método RegistrarAuditoria para almacenar la fecha de cierre de sesión
            //    var fechaCierre = DateTime.Now;
            //    _usuariosDAO.RegistrarAuditoria(id_usuario, fechaCierre, fechaCierre);
            //}
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(ClientesDTO nclient)
        {
            bool registrado = _usuariosDAO.Registrar(nclient);

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas";
            }

            return View();
        }
    }
}
