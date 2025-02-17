using AbsoluteCinema.Domain.Entities.Interfaces;
using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.DbContexts;
using AbsoluteCinema.Infrastructure.Identity.Data;
using AbsoluteCinema.Infrastructure.Mappings.AuthMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AbsoluteCinema.Infrastructure.UnitOfWorks;
using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Infrastructure.Services;
using AbsoluteCinema.Application.Services;


namespace AbsoluteCinema.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddRoles<ApplicationRole>()
               .AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IUser, ApplicationUser>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JWTService>();
            services.AddScoped<ITmdbService, TmdbService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IUserService, UserService>();


            // Подключаем мапперы
            services.AddAutoMapper(typeof(LoginMappingProfile).Assembly);

            // Надаєм http client для the movie database сервису
            services.AddHttpClient<TmdbService>();

            return services;
        }
    }
}
