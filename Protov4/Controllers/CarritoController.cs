using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Protov4.DAO;
using Protov4.DTO;

namespace Protov4.Controllers
{
    [Authorize]

    public class CarritoController : Controller
    {
        MikutechDAO carr;
        public CarritoController(IConfiguration configuration)
        {
            carr = new MikutechDAO(configuration);
           

        }
        // GET: HomeController1
        public ActionResult Carrito()
        {
            if (HttpContext.Session.GetInt32("IdPedidoActual") is int id_pedido)
            {
                
                var carrfull = obtenerCarritoFull(id_pedido);
                return View(carrfull);
            }
            else
            {
                var mensaje = "No hay productos en el carrito.";
                var carrfull = obtenerCarritoFull(0);
                return View(carrfull);
            }
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
        public ActionResult FinalizarCompra(string[] id_producto,int[] cantidad, decimal[] precio, decimal pagototal)
        {
            int id_pedido = carr.ObtenerIdPedido();
            for (int i = 0; i < id_producto.Length; i++)
            {
                carr.ActualizarPedidoDetalle(id_producto[i].ToString(), id_pedido, cantidad[i], (cantidad[i] * precio[i]));
            }
            carr.ActualizarPedido(id_pedido, pagototal,1, "", "", "", 1, DateTime.Now);
            List<PedidoDTO> pedido=  carr.ObtenerPedidoPorId(id_pedido);
                return View(pedido);
            
           
            //int id_pedido = carr.ObtenerIdPedido();
            // carr.ActualizarPedidoDetalle(id_producto, id_pedido,cantidad,subtotal);

        }
        public ActionResult CompraRealizada(string ciudad, string callePrincipal, string calleSecundaria, int pagometodo, decimal pagototal)
        {
            try
            {
                int id_pedido = carr.ObtenerIdPedido();


                carr.ActualizarPedido(id_pedido, pagototal, pagometodo, ciudad, callePrincipal, calleSecundaria, 1, DateTime.Now);
                List<PedidoDTO> pedido = carr.ObtenerPedidoPorId(id_pedido);
                return View(pedido);
            }
            catch (Exception ex)
            {
                // Manejar la excepción de manera adecuada, como mostrar un mensaje de error o registrar el error en un log
                ViewBag.ErrorMessage = "Error al procesar la compra: " + ex.Message;
                return View("Error"); // Puedes crear una vista específica para mostrar errores
            }
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


        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePedido( string id_producto)
        {
            try
            {
                int id_pedido = carr.ObtenerIdPedido();
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
