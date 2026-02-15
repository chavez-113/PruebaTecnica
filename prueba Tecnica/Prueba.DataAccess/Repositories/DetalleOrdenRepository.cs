// DataAccess/Repositories/DetalleOrdenRepository.cs
using Dapper;
using Microsoft.Data.SqlClient;
using Prueba.Entities.Entities;
using System.Data;

namespace Prueba.DataAccess.Repositories
{
    public class DetalleOrdenRepository
    {
        public SpDetalleOrdenResult CrearDetalle(long ordenId, long productoId, int cantidad,
            decimal impuesto, decimal subtotal, decimal total)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@OrdenId", ordenId, DbType.Int64, ParameterDirection.Input);
            parameter.Add("@ProductoId", productoId, DbType.Int64, ParameterDirection.Input);
            parameter.Add("@Cantidad", cantidad, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Impuesto", impuesto, DbType.Decimal, ParameterDirection.Input);
            parameter.Add("@Subtotal", subtotal, DbType.Decimal, ParameterDirection.Input);
            parameter.Add("@Total", total, DbType.Decimal, ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.QueryFirstOrDefault<SpDetalleOrdenResult>(
                    ScriptDataBase.DetalleOrden_Crear,
                    parameter,
                    commandType: CommandType.StoredProcedure);

                return result ?? new SpDetalleOrdenResult
                {
                    code_Status = 0,
                    message_Status = "Error desconocido"
                };
            }
            catch (Exception ex)
            {
                return new SpDetalleOrdenResult
                {
                    code_Status = 0,
                    message_Status = $"Error inesperado: {ex.Message}"
                };
            }
        }

        public RequestStatus ActualizarExistenciaProducto(long productoId, int cantidad)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@ProductoId", productoId, DbType.Int64, ParameterDirection.Input);
            parameter.Add("@Cantidad", cantidad, DbType.Int32, ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.QueryFirstOrDefault<RequestStatus>(
                    ScriptDataBase.Producto_ActualizarExistencia,
                    parameter,
                    commandType: CommandType.StoredProcedure);

                return result ?? new RequestStatus
                {
                    code_Status = 0,
                    message_Status = "Error desconocido"
                };
            }
            catch (Exception ex)
            {
                return new RequestStatus
                {
                    code_Status = 0,
                    message_Status = $"Error inesperado: {ex.Message}"
                };
            }
        }
    }
}