using Protov4.DTO;

namespace Protov4.DAO
{
    public class MikutechDAO : MikuTechFactory
    {   ProductoDAO productoDAO;
        CarritoDAO carritoDAO;
        public MikutechDAO(IConfiguration configuration)
        {
            // Initialize the productoDAO object here
            productoDAO = new ProductoDAO(configuration);
            carritoDAO = new CarritoDAO(configuration);// You might need to adjust this based on your actual implementation
        }

        public override void ActualizarPedidoDetalle(int id_pedido, int cantidad, decimal subtotal_producto)
        {
            carritoDAO.ActualizarPedidoDetalle(id_pedido, cantidad, subtotal_producto);
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

        public override List<PedidoDTO> RegistrarPedido(int id_cliente)
        {
            return carritoDAO.RegistrarPedido(id_cliente);
        }
    }
}
    