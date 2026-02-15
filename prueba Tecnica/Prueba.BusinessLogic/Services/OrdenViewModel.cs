using System.ComponentModel.DataAnnotations;

namespace Prueba.Models
{
    public class OrdenViewModel
    {
        public long OrdenId { get; set; } = 0;

        [Required(ErrorMessage = "El ClienteId es requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "El ClienteId debe ser mayor a 0")]
        public long ClienteId { get; set; }

        [Required(ErrorMessage = "Debe incluir al menos un detalle")]
        [MinLength(1, ErrorMessage = "Debe incluir al menos un detalle")]
        public List<DetalleOrdenViewModel> Detalle { get; set; }
    }

    public class DetalleOrdenViewModel
    {
        [Required(ErrorMessage = "El ProductoId es requerido")]
        [Range(1, long.MaxValue, ErrorMessage = "El ProductoId debe ser mayor a 0")]
        public long ProductoId { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }
    }
}