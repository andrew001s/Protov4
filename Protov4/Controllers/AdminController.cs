using Microsoft.AspNetCore.Mvc;

namespace Protov4.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult admin()
        {
            return View();
        }
    }
}
