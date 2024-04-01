using Microsoft.Extensions.DependencyInjection;
using TesteBMP.Domain.Service;

namespace TesteBMP
{
    public static class DI
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IServiceBMP, ServiceBMP>();

            return services;
        }
        
    }
}
