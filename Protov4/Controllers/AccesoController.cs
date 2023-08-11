using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Protov4.DAO;
using Protov4.DTO;
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

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsuariosDTO user)
        {
            try
            {
                ((int id_usuario, int id_rol_user), int id_cliente) = _usuariosDAO.ValidarUsuario(user);
                if (id_usuario != 0)
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, user.correo_elec),
                    new Claim("id_usuario", id_usuario.ToString()),
                    new Claim("id_cliente", id_cliente.ToString()),
                    new Claim("id_rol_user", id_rol_user.ToString())

                    };

                    if (id_rol_user == 1)
                    {
                        claims.Add(new Claim("id_rol_user", "1")); // Administrador
                    }
                    else
                    {
                        claims.Add(new Claim("id_rol_user", "2")); // Usuario normal
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    if (id_rol_user == 1)
                    {
                        return RedirectToAction("Admin", "Administrador");
                    }
                    else
                    {

                       // _usuariosDAO.RegistrarAuditoria(id_usuario, DateTime.Now, true);
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

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult AdminPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(UsuariosDTO nuser, ClientesDTO nclient)
        {
            bool registrado = _usuariosDAO.Registrar(nuser, nclient);

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                ViewData["Mensaje"] = "Error al registrar";
            }

            return View();
        }

        public ActionResult LogOut()
        {
            Response.Cookies.Delete("user");
            return RedirectToAction("Login", "Acceso");
        }
    }
}
