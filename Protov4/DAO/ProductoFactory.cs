using MongoDB.Driver;
using Protov4.DTO;

namespace Protov4.DAO
{
    public class ProductoFactory : IProductoCollection
    {
        private readonly IMongoCollection<ProductoDTO> prod;
        public ProductoFactory(IConfiguration configuration)
        {
            var mongo = new DBMongo(configuration);
            prod = mongo.GetDatabase().GetCollection<ProductoDTO>("Productos");
        }
        public override List<ProductoDTO> GetAllProductos(string tipo)
        {
            throw new NotImplementedException();
        }

        public override List<ProductoDTO> GetSeleccion(string id)
        {
            throw new NotImplementedException();
        }
    }
}
