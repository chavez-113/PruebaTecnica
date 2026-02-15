using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.BusinessLogic.DTO
{
    public class SpClienteResult
    {
        public int code_Status { get; set; }
        public string message_Status { get; set; }
        public int? ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Identidad { get; set; }
    }
}
