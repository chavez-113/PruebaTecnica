// BusinessLogic/ServiceConfiguration.cs
using Microsoft.Extensions.DependencyInjection;
using Prueba.DataAccess;
using Prueba.DataAccess.Repositories;
using Prueba.BusinessLogic.Extensions;
using Prueba.BusinessLogic.Services;

namespace Prueba.BusinessLogic
{
    public static class ServiceConfiguration
    {
        public static void DataAccess(this IServiceCollection services, string connectionString)
        {
            // Repositorios
            services.AddScoped<ClienteRepository>();
            services.AddScoped<ProductoRepository>();
            services.AddScoped<OrdenRepository>();
            services.AddScoped<DetalleOrdenRepository>();

            PruebaContext.BuildConnectionString(connectionString);
        }

        public static void businessLogic(this IServiceCollection services)
        {
            // Servicios de negocio
            services.AddScoped<GeneralServices>();
        }
    }
}