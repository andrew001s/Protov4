using Protov4.DTO;

namespace Protov4.DAO
{
    public class MikutechDAO : MikuTechFactory
    {   ProductoDAO productoDAO;
        CarritoDAO carritoDAO;
        public static bool pedido = false;
        public MikutechDAO(IConfiguration configuration)
        {
            // Initialize the productoDAO object here
            productoDAO = new ProductoDAO(configuration);
            carritoDAO = new CarritoDAO(configuration);// You might need to adjust this based on your actual implementation
        }

        public override void ActualizarPedido(int id_pedido,decimal pago_total, int tipoestado , string Ciudad_envio, string Calle_principal, string Calle_secundaria, int id_tipo_pago, DateTime fecha_pedido)
        {
            carritoDAO.ActualizarPedido(id_pedido, pago_total,tipoestado, Ciudad_envio, Calle_principal, Calle_secundaria, id_tipo_pago, fecha_pedido);
        }

        public override void ActualizarPedidoDetalle(string id_producto, int id_pedido, int cantidad, decimal subtotal_producto)
        {
            carritoDAO.ActualizarPedidoDetalle(id_producto,id_pedido, cantidad, subtotal_producto);
        }

        public override void EliminarProductoCarrito(int id, string idproducto)
        {
            carritoDAO.EliminarProductoCarrito(id, idproducto);
        }

        public override List<ProductoDTO> GetAllProductos(string tipo)
        {
            return productoDAO.ObtenerProductos(tipo);
        }

        public override List<ProductoDTO> GetSeleccion(string id)
        {
            return productoDAO.ObtenerSeleccion(id);
        }

        public override void insertarCarrito(int id_pedido, string id_producto, decimal precio, int cantidad, decimal subtotal)
        {
            carritoDAO.InsertarPedidoDetalle(id_pedido, id_producto, precio, cantidad, subtotal);
        }

        public override void insertarPedido_Detalle(int id_pedido, string id_producto, decimal precio, int cantidad, decimal subtotal)
        {
            carritoDAO.InsertarPedidoDetalle(id_pedido, id_producto, precio, cantidad, subtotal);
        }

        public override List<CarritoDTO> ObtenerCarrito()
        {
            return carritoDAO.ObtenerCarrito();
        }

        public override List<CarritoFullDTO> ObtenerCarritoFull(int id)
        {
            return carritoDAO.ObtenerCarritoFull(id);
        }

        public override int ObtenerIdPedido()
        {
            return carritoDAO.ObtenerIdPedido();
        }

        public override void RegistrarPedido(int id_cliente)
        {
             carritoDAO.RegistrarPedido(id_cliente);
        }

        public override List<PedidoDTO> ObtenerPedidoPorId(int idPedido)
        {
            return carritoDAO.ObtenerPedidoPorId(idPedido);
        }
    }
}
    