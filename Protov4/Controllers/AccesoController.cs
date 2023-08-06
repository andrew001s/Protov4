using Microsoft.AspNetCore.Mvc;
using Protov4.DAO;
using Protov4.DTO;

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
        public ActionResult Login(UsuariosDTO user)
        {
            int idUsuario = _usuariosDAO.ValidarUsuario(user.correo_elec, user.contrasena);
            if (idUsuario != 0)
            {
                Response.Cookies.Append("user", "Bienvenido" + user.correo_elec);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
            }
            return View();
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
