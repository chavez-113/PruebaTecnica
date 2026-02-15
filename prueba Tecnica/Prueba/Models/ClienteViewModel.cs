using System.ComponentModel.DataAnnotations;

namespace Prueba.Models
{
    public class ClienteViewModel
    {
        public long ClienteId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La identidad es requerida")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "La identidad debe tener exactamente 15 caracteres")]
        [RegularExpression(@"^\d{4}-\d{4}-\d{5}$", ErrorMessage = "La identidad debe tener el formato 0000-0000-00000")]
        public string Identidad { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
