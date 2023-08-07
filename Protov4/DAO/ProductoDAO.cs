﻿using MongoDB.Bson;
using MongoDB.Driver;
using Protov4.DTO;

namespace Protov4.DAO
{
    public class ProductoDAO : IProductoCollection
    {
        private readonly IMongoCollection<ProductoDTO> prod;
        private readonly IMongoCollection<CarritoFullDTO> carfull;

        public ProductoDAO(IConfiguration configuration)
        {

            var mongo = new DBMongo(configuration);
            prod = mongo.GetDatabase().GetCollection<ProductoDTO>("Productos");
            carfull = mongo.GetDatabase().GetCollection<CarritoFullDTO>("Productos");
        }
        public override List<ProductoDTO> GetAllProductos(string tipo)
        {
            var filtro = Builders<ProductoDTO>.Filter.Eq("Tipo", tipo);

            if (tipo == "Procesador")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else if (tipo == "Gráfica")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else if (tipo == "Ram")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else if (tipo == "Placa")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else if (tipo == "Fuente")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else if (tipo == "Almacenamiento")
            {
                var query = prod.Find(filtro).ToListAsync();
                return query.Result;
            }
            else
            {
                var query = prod.Find(new BsonDocument()).ToListAsync();
                return query.Result;
            }


        }

        public override List<ProductoDTO> GetAllProcesadores()
        {
            var filtro = Builders<ProductoDTO>.Filter.Eq("Tipo", "Procesador");
            var query = prod.Find(filtro).ToListAsync();
            return query.Result;
        }
        public override List<ProductoDTO> GetAllGraficas()
        {
            var filtro = Builders<ProductoDTO>.Filter.Eq("Tipo", "Gráfica");
            var query = prod.Find(filtro).ToListAsync();
            return query.Result;
        }

        public override List<ProductoDTO> GetAllRAM()
        {
            var filtro = Builders<ProductoDTO>.Filter.Eq("Tipo", "RAM");
            var query = prod.Find(filtro).ToListAsync();
            return query.Result;
        }

        public override List<ProductoDTO> GetSeleccion(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<ProductoDTO>.Filter.Eq(x => x.Id, objectId);
            return prod.Find(filter).ToList();
        }
      

    }
}