using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.DataAccess.DTO
{
    public class SpProductoResult
    {
        public int code_Status { get; set; }
        public string message_Status { get; set; }
        public long? ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Existencia { get; set; }
    }
}
