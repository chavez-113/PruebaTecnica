// DataAccess/Repositories/ClienteRepository.cs
using Dapper;
using Microsoft.Data.SqlClient;
using Prueba.BusinessLogic.DTO;
using Prueba.Entities.Entities;
using System;


namespace Prueba.DataAccess.Repositories
{
    public class ClienteRepository 
    {
        public IEnumerable<Cliente> List()
        {
            var parameter = new DynamicParameters();

            using var db = new SqlConnection(PruebaContext.ConnectionString);
            var result = db.Query<Cliente>(ScriptDataBase.Cliente_Listar, parameter, commandType: System.Data.CommandType.StoredProcedure);

            return result;
        }

        public SpClienteResult Update(Cliente item)
        {
            if (item == null)
            {
                return new SpClienteResult
                {
                    code_Status = 0,
                    message_Status = "Los datos llegaron vacíos o son erróneos."
                };
            }

            var parameter = new DynamicParameters();
            parameter.Add("@ClienteId", item.ClienteId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            parameter.Add("@Nombre", item.Nombre, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameter.Add("@Identidad", item.Identidad, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.QueryFirstOrDefault<SpClienteResult>(
                    ScriptDataBase.Cliente_Actualizar,
                    parameter,
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result == null)
                {
                    return new SpClienteResult
                    {
                        code_Status = 0,
                        message_Status = "Ocurrió un error desconocido"
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new SpClienteResult
                {
                    code_Status = 0,
                    message_Status = $"Error inesperado: {ex.Message}"
                };
            }
        }

        public SpClienteResult Find(int? id)
        {
            if (!id.HasValue || id <= 0)
            {
                return new SpClienteResult
                {
                    code_Status = 0,
                    message_Status = "ID inválido"
                };
            }

            var parameter = new DynamicParameters();
            parameter.Add("@ClienteId", id.Value, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.QueryFirstOrDefault<SpClienteResult>(
                    ScriptDataBase.Cliente_Buscar,
                    parameter,
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result == null)
                {
                    return new SpClienteResult
                    {
                        code_Status = 0,
                        message_Status = "Cliente no encontrado"
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new SpClienteResult
                {
                    code_Status = 0,
                    message_Status = $"Error inesperado: {ex.Message}"
                };
            }
        }

        public SpClienteResult Insert(Cliente item)
        {
            if (item == null)
            {
                return new SpClienteResult
                {
                    code_Status = 0,
                    message_Status = "Los datos llegaron vacíos o son erróneos."
                };
            }

            var parameter = new DynamicParameters();
            parameter.Add("@Nombre", item.Nombre, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameter.Add("@Identidad", item.Identidad, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            try
            {
                using var db = new SqlConnection(PruebaContext.ConnectionString);
                var result = db.QueryFirstOrDefault<SpClienteResult>(
                    ScriptDataBase.Cliente_Insertar,
                    parameter,
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result == null)
                {
                    return new SpClienteResult
                    {
                        code_Status = 0,
                        message_Status = "Ocurrió un error desconocido"
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new SpClienteResult
                {
                    code_Status = 0,
                    message_Status = $"Error inesperado: {ex.Message}"
                };
            }
        }
    }
}