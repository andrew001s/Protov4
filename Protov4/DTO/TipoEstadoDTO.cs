using System.ComponentModel.DataAnnotations;

namespace Protov4.DTO
{
    public class TipoEstadoDTO
    {
        [Key]
        public int id_tipo_estado { get; set; }
        public string? nombre_estado { get; set; }
    }
}
