using Protov4.DTO;

namespace Protov4.DAO
{
    public abstract class IProductoCollection
    {
        public abstract List<ProductoDTO> GetAllProductos(string tipo);
        public abstract List<ProductoDTO> GetAllProcesadores();
        public abstract List<ProductoDTO> GetAllGraficas();
        public abstract List<ProductoDTO> GetSeleccion(string id);

        public abstract List<ProductoDTO> GetAllRAM();
    }
}
