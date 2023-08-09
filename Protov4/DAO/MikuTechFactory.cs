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
        public abstract void RegistrarPedido(int id_cliente);
        public abstract void ActualizarPedidoDetalle(int id_pedido, int cantidad, decimal subtotal_producto);
        public abstract void ActualizarPedido(int id_pedido, decimal pago_total, string Ciudad_envio, string Calle_principal, string Calle_secundaria, int id_tipo_pago, DateTime fecha_pedido);
        public abstract int ObtenerIdPedido();
    }
}
