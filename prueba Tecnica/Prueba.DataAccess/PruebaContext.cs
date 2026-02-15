using Prueba.DataAccess.Contexrt;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.DataAccess
{
    // Contexto específico de la aplicación que extiende el contexto generado por EF.
    public class PruebaContext: OrderManagementDBContext
    {
        // Cadena de conexión global usada como fallback en OnConfiguring.
        public static string ConnectionString { get; set; }
        // pasa las opciones al contexto base y desactiva Lazy Loading.
        public PruebaContext(DbContextOptions<OrderManagementDBContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        // Configuración de provider: usa ConnectionString si no se ha configurado optionsBuilder.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
        // Normaliza y almacena una cadena de conexión (para uso por OnConfiguring).
        public static void BuildConnectionString(string connection)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder { ConnectionString = connection };
            ConnectionString = connectionStringBuilder.ConnectionString;
        }
    }
}
