using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.DataAccess.Repositories
{
    public class RequestStatus
    {
        public int code_Status { get; set; }
        public string message_Status { get; set; }
    }
    public class RequestStatusWithData<T>
    {
        public int code_Status { get; set; }
        public string message_Status { get; set; }
        public T Data { get; set; }
    }
}
