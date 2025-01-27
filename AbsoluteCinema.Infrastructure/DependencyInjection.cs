using AbsoluteCinema.Domain.Entities.Interfaces;
using AbsoluteCinema.Infrastructure.DbContexts;
using AbsoluteCinema.Infrastructure.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AbsoluteCinema.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,  IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IUser, ApplicationUser>();

            return services;
        }
    }
}
