using Microsoft.AspNetCore.Mvc;

namespace Protov4.DAO
{
    public class AdminDAO:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> Index()
        //{
        //    return _context.Compras != null ?
        //                View(await _context.Compras.ToListAsync()) :
        //                Problem("Entity set 'ScrapDbContext.Compras'  is null.");
        //}

        public IActionResult Create()
        {
            return View();
        }
        /*Metodo GET para dirigirse a la ventana de eliminar un producto*/
        public IActionResult Delete()
        {
            return View();
        }
        /*Metodo GET para dirigirse a la ventana de ver mas a detalle el producto*/
        public IActionResult Details()
        {
            return View();
        }
        /*Metodo GET para dirigirse a la ventana de editar un producto*/
        public IActionResult Edit()
        {
            return View();
        }

    }
}
