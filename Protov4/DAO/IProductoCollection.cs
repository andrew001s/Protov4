using Protov4.DTO;

namespace Protov4.DAO
{
    public abstract class IProductoCollection
    {
        public abstract List<ProductoDTO> GetAllProductos(string tipo);

        public abstract List<ProductoDTO> GetSeleccion(string id);

    }
}
