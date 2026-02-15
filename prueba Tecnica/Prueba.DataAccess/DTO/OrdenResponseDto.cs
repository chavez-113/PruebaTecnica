using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/OrdenResponseDto.cs
namespace Prueba.Models
{
    public class OrdenResponseDto
    {
        public long OrdenId { get; set; }
        public long ClienteId { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<DetalleOrdenResponseDto> Detalles { get; set; }
    }

    public class DetalleOrdenResponseDto
    {
        public long DetalleOrdenId { get; set; }
        public long OrdenId { get; set; }
        public long ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}
