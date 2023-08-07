using System.ComponentModel.DataAnnotations;

namespace Protov4.DTO
{
    public class TipoPagoDTO
    {
        [Key]
        public int id_tipo_pago { get; set; }
        public string? nombre_pago { get; set; }
    }
}
