// DataAccess/Repositories/OrdenRepository.cs
using Dapper;
using Microsoft.Data.SqlClient;
using Prueba.Entities.Entities;
using Prueba.Models;
using System.Data;

namespace Prueba.DataAccess.Repositories
{
    public class OrdenRepository
    {
        public SpOrdenResult CrearOrden(long clienteId, decimal impuesto, decimal subtotal, decimal total)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@ClienteId", clienteId, DbType.Int64, ParameterDirection.Input);
            parameter.Add("@Impuesto", impuesto, DbType.Decimal, ParameterDirection.Input);
            parameter.Add("@Subtotal", subtotal, DbType.Decimal, ParameterDirection.Input);
            parameter.Add("@Total", total, DbType.Decimal, ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.QueryFirstOrDefault<SpOrdenResult>(
                    ScriptDataBase.Orden_Crear,
                    parameter,
                    commandType: CommandType.StoredProcedure);

                return result ?? new SpOrdenResult
                {
                    code_Status = 0,
                    message_Status = "Error desconocido al crear orden"
                };
            }
            catch (Exception ex)
            {
                return new SpOrdenResult
                {
                    code_Status = 0,
                    message_Status = $"Error inesperado: {ex.Message}"
                };
            }
        }

        public RequestStatus ActualizarTotales(long ordenId, decimal impuesto, decimal subtotal, decimal total)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@OrdenId", ordenId, DbType.Int64, ParameterDirection.Input);
            parameter.Add("@Impuesto", impuesto, DbType.Decimal, ParameterDirection.Input);
            parameter.Add("@Subtotal", subtotal, DbType.Decimal, ParameterDirection.Input);
            parameter.Add("@Total", total, DbType.Decimal, ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.QueryFirstOrDefault<RequestStatus>(
                    ScriptDataBase.Orden_ActualizarTotales,
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

        public OrdenResponseDto ObtenerOrdenConDetalles(long ordenId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@OrdenId", ordenId, DbType.Int64, ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                using var multi = db.QueryMultiple(
                    ScriptDataBase.Orden_ObtenerConDetalles,
                    parameter,
                    commandType: CommandType.StoredProcedure);

                var orden = multi.Read<OrdenResponseDto>().FirstOrDefault();
                if (orden != null)
                {
                    orden.Detalles = multi.Read<DetalleOrdenResponseDto>().ToList();
                }

                return orden;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener orden: {ex.Message}");
                return null;
            }
        }
    }
}