using Protov4.DTO;

namespace Protov4.DAO
{
    public class MikutechDAO : MikuTechFactory
    { ProductoDAO productoDAO;
        public MikutechDAO(IConfiguration configuration)
        {
            // Initialize the productoDAO object here
            productoDAO = new ProductoDAO(configuration); // You might need to adjust this based on your actual implementation
        }
        public override List<ProductoDTO> GetAllProductos(string tipo)
        {
            return productoDAO.ObtenerProductos(tipo);
        }

        public override List<ProductoDTO> GetSeleccion(string id)
        {
            return productoDAO.ObtenerSeleccion(id);
        }
    }
}
