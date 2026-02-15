using Microsoft.Extensions.DependencyInjection;
using Prueba.BusinessLogic.Services;
using Prueba.DataAccess.Repositories;

namespace Prueba.BusinessLogic.Extensions
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection businessLogic(this IServiceCollection services)
        {
            // Registrar repositorios

            services.AddScoped<GeneralServices>();

            return services;
        }
    }
}
