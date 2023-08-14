using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Protov4.DAO;
using Protov4.DTO;

namespace Protov4.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdministradorController : Controller
    {
        private readonly MikuTechFactory db;
        public AdministradorController(IConfiguration configuration)
        {
            db = new MikutechDAO(configuration);

        }
        public IActionResult Administrador()
        {
            var productos = ListarProductos("");
            return View("Productos",productos);
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
        public ActionResult Auditoria()
        {
            var audotira = obtenerAuditoria();
            return View(audotira);
        }
        [HttpGet]
        public List<AuditoriaDTO> obtenerAuditoria()
        {
            List<AuditoriaDTO> auditoria;
            auditoria = db.ObtenerAuditoria();
            var AuditoriaDTO = auditoria.Select(p => new AuditoriaDTO
            {
                id_auditoria = p.id_auditoria,
                id_usuario=p.id_usuario,
                fecha_inicio_sesion=p.fecha_inicio_sesion,
                fecha_cierre_session=p.fecha_cierre_session
            }).ToList();

            return AuditoriaDTO;
        }
        public ActionResult CrearNuevoProducto()
        {

            return View();
        }

    }
}
