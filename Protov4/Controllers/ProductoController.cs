using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Protov4.DAO;
using Protov4.DTO;
using Protov4.Models;

namespace Protov4.Controllers
{
    public class ProductoController : Controller
    {
        private readonly MikuTechFactory db;

        public ProductoController(IConfiguration configuration)
        {
            db = new MikutechDAO(configuration);
        }
   
        // GET: ProductoController

        public ActionResult Producto(string tipo)
        {
            var productos = ListarProductos(tipo);
            return View(productos);
        }
        public ActionResult ProductoSeleccion(string _id)
        {
            var productos = ObjetoSeleccion(_id);
            return View(productos);
        }
        [HttpGet]
        public List<ProductoDTO> ObjetoSeleccion(string _id)
        {
            List<ProductoDTO> productos;
            productos = db.GetSeleccion(_id);
            var ProductoDTO = productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre_Producto = p.Nombre_Producto,
                Precio = p.Precio,
                Imagen = p.Imagen,
                Marca = p.Marca,
                Existencia = p.Existencia,
                Tipo = p.Tipo,
                Especificaciones = new EspecificacionesDTO
                {
                    Fabricante = p.Especificaciones.Fabricante,
                    Modelo = p.Especificaciones.Modelo,
                    Velocidad = p.Especificaciones.Velocidad,
                    Zócalo = p.Especificaciones.Zócalo,
                    TamañoVRAM = p.Especificaciones.TamañoVRAM,
                    Interfaz = p.Especificaciones.Interfaz,
                    Tamañomemoria = p.Especificaciones.Tamañomemoria,
                    TecnologíaRAM = p.Especificaciones.TecnologíaRAM,
                    Almacenamiento = p.Especificaciones.Almacenamiento,
                    Descripción = p.Especificaciones.Descripción
                }
               
            }).ToList();

            return ProductoDTO;
        }
        [HttpGet]
        public List<ProductoDTO> ListarProductos(string tipo)
        {
            List<ProductoDTO> productos;
            productos = db.GetAllProductos(tipo);
            var ProductoDTO = productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre_Producto = p.Nombre_Producto,
                Precio = p.Precio,
                Imagen = p.Imagen,
                Marca = p.Marca,
                Existencia = p.Existencia,
                Tipo = p.Tipo
            }).ToList();

            return ProductoDTO;
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: ProductoController/Create
        [HttpPost]
        public ActionResult CrearCarrito(int id_cliente, string id_producto, decimal precio, int cantidad, decimal subtotal)
        {
            try
            {
                db.RegistrarPedido(id_cliente);
                int id_pedido= db.ObtenerIdPedido();
                db.insertarCarrito(id_pedido, id_producto, precio, cantidad, subtotal);
                
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Manejar el error si es necesario y devolver un resultado JSON con éxito falso en caso de error
                return Json(new { success = false, errorMessage = ex.Message });
            }

        }
        // POST: ProductoController/Create
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

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
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

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
