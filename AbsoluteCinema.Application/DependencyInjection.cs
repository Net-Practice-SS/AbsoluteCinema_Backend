using System.Text;
using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.Services;
using AbsoluteCinema.Application.Validators.AuthValidators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using AbsoluteCinema.Domain.Constants;

namespace AbsoluteCinema.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services, IConfiguration configuration)
        {
            // Подключаем флюент-валидаторы, ищет все валидаторы там где лежит LoginDtoValidator
            services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>(ServiceLifetime.Transient);

            // Підлючення сервісів
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IActorService, ActorService>();

            //Token authentication with lifetime and issuer validation rules
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {

                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:TokenIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]!))
                };
            });

            services.AddAuthorization(options => {
                options.AddPolicy(Policy.UserPolicy, policy => {
                    policy.RequireRole(Role.User, Role.Admin);
                });

                options.AddPolicy(Policy.AdminPolicy, policy => {
                    policy.RequireRole(Role.Admin);
                });
            });


            return services;
        }
    }
}
