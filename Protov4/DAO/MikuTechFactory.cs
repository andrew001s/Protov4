using Protov4.DTO;

namespace Protov4.DAO
{
    public abstract class MikuTechFactory
    {
        public abstract List<ProductoDTO> GetAllProductos(string tipo);

        public abstract List<ProductoDTO> GetSeleccion(string id);
        public abstract void insertarCarrito(int id_pedido, string id_producto,decimal precio,int cantidad,decimal subtotal);
        public abstract void insertarPedido_Detalle(int id_pedido, string id_producto, decimal precio, int cantidad, decimal subtotal);
        public abstract List<CarritoDTO> ObtenerCarrito();
        public abstract List<CarritoFullDTO> ObtenerCarritoFull(int id);
        public abstract void EliminarProductoCarrito(int id, string idproducto);
        public abstract List<PedidoDTO> RegistrarPedido(int id_cliente);
        public abstract void ActualizarPedidoDetalle(int id_pedido, int cantidad, decimal subtotal_producto);
    }
}
