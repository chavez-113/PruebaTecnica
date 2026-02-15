using Dapper;
using Microsoft.Data.SqlClient;
using Prueba.DataAccess.DTO;
using Prueba.Entities.Entities;
using Prueba.Models;

namespace Prueba.DataAccess.Repositories
{
    public class ProductoRepository
    {
        public IEnumerable<ProductoResponseDto> List()
        {
            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.Query<ProductoResponseDto>(
                    ScriptDataBase.Producto_Listar,
                    commandType: System.Data.CommandType.StoredProcedure);

                return result ?? new List<ProductoResponseDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar productos: {ex.Message}");
                return new List<ProductoResponseDto>();
            }
        }

        public SpProductoResult Find(long? id)
        {
            if (!id.HasValue || id <= 0)
            {
                return new SpProductoResult
                {
                    code_Status = 0,
                    message_Status = "ID inválido"
                };
            }

            var parameter = new DynamicParameters();
            parameter.Add("@ProductoId", id.Value, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.QueryFirstOrDefault<SpProductoResult>(
                    ScriptDataBase.Producto_Buscar,
                    parameter,
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result == null)
                {
                    return new SpProductoResult
                    {
                        code_Status = 0,
                        message_Status = "Producto no encontrado"
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new SpProductoResult
                {
                    code_Status = 0,
                    message_Status = $"Error inesperado: {ex.Message}"
                };
            }
        }

        public SpProductoResult Insert(Producto item)
        {
            if (item == null)
            {
                return new SpProductoResult
                {
                    code_Status = 0,
                    message_Status = "Los datos llegaron vacíos o son erróneos."
                };
            }

            var parameter = new DynamicParameters();
            parameter.Add("@Nombre", item.Nombre, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameter.Add("@Descripcion", item.Descripcion, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameter.Add("@Precio", item.Precio, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
            parameter.Add("@Existencia", item.Existencia, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.QueryFirstOrDefault<SpProductoResult>(
                    ScriptDataBase.Producto_Insertar,
                    parameter,
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result == null)
                {
                    return new SpProductoResult
                    {
                        code_Status = 0,
                        message_Status = "Ocurrió un error desconocido"
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new SpProductoResult
                {
                    code_Status = 0,
                    message_Status = $"Error inesperado: {ex.Message}"
                };
            }
        }

        public SpProductoResult Update(Producto item)
        {
            if (item == null)
            {
                return new SpProductoResult
                {
                    code_Status = 0,
                    message_Status = "Los datos llegaron vacíos o son erróneos."
                };
            }

            var parameter = new DynamicParameters();
            parameter.Add("@ProductoId", item.ProductoId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
            parameter.Add("@Nombre", item.Nombre, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameter.Add("@Descripcion", item.Descripcion, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameter.Add("@Precio", item.Precio, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
            parameter.Add("@Existencia", item.Existencia, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.QueryFirstOrDefault<SpProductoResult>(
                    ScriptDataBase.Producto_Actualizar,
                    parameter,
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result == null)
                {
                    return new SpProductoResult
                    {
                        code_Status = 0,
                        message_Status = "Ocurrió un error desconocido"
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new SpProductoResult
                {
                    code_Status = 0,
                    message_Status = $"Error inesperado: {ex.Message}"
                };
            }
        }
    }
}