using System.ComponentModel.DataAnnotations;

namespace Prueba.Models
{
    public class ProductoViewModel
    {
        public long ProductoId { get; set; } = 0;

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La existencia es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "La existencia debe ser mayor o igual a 0")]
        public int Existencia { get; set; }
    }
}