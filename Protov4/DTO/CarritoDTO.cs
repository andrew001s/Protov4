using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes; 

namespace Protov4.DTO
{
    public class CarritoDTO{

        [Key]
        public int id_pedido_detalle { get; set; }
        public int id_pedido { get; set; }
        public string? id_producto { get; set; }
        public decimal precio { get; set; } 
        public int cantidad { get; set; }
        public decimal subtotal_producto { get; set; }
    }

    
}
