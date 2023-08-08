using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Protov4.DAO;
using Protov4.DTO;

namespace Protov4.Controllers
{
    public class CarritoController : Controller
    {
        MikutechDAO carr;
        public CarritoController(IConfiguration configuration)
        {
            carr = new MikutechDAO(configuration);
           

        }
        // GET: HomeController1
        public ActionResult Carrito(int id)
        {



            var carrfull = obtenerCarritoFull(id);

            return View(carrfull);

        }

        [HttpGet]
        private List<CarritoFullDTO> obtenerCarritoFull(int idped)
        {
            List<CarritoFullDTO> carr1 = carr.ObtenerCarritoFull(idped);

            return carr1;
        }


        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult FinalizarCompra(int id_cliente)
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id_pedido, string id_producto)
        {
            try
            {
                carr.EliminarProductoCarrito(id_pedido, id_producto);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Manejar el error si es necesario y devolver un resultado JSON con éxito falso en caso de error
                return Json(new { success = false, errorMessage = ex.Message });
            }
        }

    }
}
