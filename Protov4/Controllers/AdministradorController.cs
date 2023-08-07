using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Protov4.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        public IActionResult Admin()
        {

            return View();
        }
    }
}
