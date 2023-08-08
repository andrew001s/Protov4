using System.ComponentModel.DataAnnotations;

namespace Protov4.DTO
{
    public class PedidoDTO
    {
        [Key]
        public int id_pedido { get; set; }
        public int id_cliente { get; set; }
        public decimal pago_total { get; set; }
        public string? Ciudad_envio { get; set; }
        public string? Calle_principal { get; set; }
        public string? Calle_secundaria { get; set; }
        public int id_tipo_pago { get; set; }
        public int id_tipo_estado { get; set; }
        public DateTime fecha_pedido { get; set; }
    }
}
