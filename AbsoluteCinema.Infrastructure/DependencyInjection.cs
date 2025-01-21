using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AbsoluteCinema.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //Inject DbContext
            
            //Inject repositories

            return services;
        }
    }
}
