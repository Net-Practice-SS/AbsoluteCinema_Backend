using AbsoluteCinema.Application.Mappings.AuthMapping;
using AbsoluteCinema.Domain.Entities.Interfaces;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.DbContexts;
using AbsoluteCinema.Infrastructure.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AbsoluteCinema.Infrastructure.UnitOfWorks;


namespace AbsoluteCinema.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services,  IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IUser, ApplicationUser>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // Подключаем мапперы
            services.AddAutoMapper(typeof(LoginMappingProfile).Assembly);
            
            return services;
        }
    }
}
