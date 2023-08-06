using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Protov4.DTO
{
    public class ProductoDTO
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string? Nombre_Producto { get; set; }
        public double Precio { get; set; }
        public string? Marca { get; set; }
        public int Existencia { get; set; }
        public string? Tipo { get; set; }
        public byte[]? Imagen { get; set; }
        public EspecificacionesDTO? Especificaciones { get; set; }

    }
    public class EspecificacionesDTO
    {
        public string? Fabricante { get; set; }
        public string? Modelo { get; set; }
        public string? Velocidad { get; set; }
        public string? Zócalo { get; set; }
        public string? TamañoVRAM { get; set; }
        public string? Interfaz { get; set; }
        public string? Tamañomemoria { get; set; }
        public string? TecnologíaRAM { get; set; }
        public string? Almacenamiento { get; set; }
        public List<string>? Descripción { get; set; }

    }
}
