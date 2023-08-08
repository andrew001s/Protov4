using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Protov4.DTO
{
    public class CarritoFullDTO
    { 
        
        public string id_producto { get; set; }
        public byte[]? Imagen { get; set; }
        public string? Nombre_Producto { get; set; }
        public int cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal subtotal_producto { get; set; }

    }
}
