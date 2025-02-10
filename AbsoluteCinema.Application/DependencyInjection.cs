using AbsoluteCinema.Application.Contracts;
using AbsoluteCinema.Application.Services;
using AbsoluteCinema.Application.Validators.AuthValidators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AbsoluteCinema.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            // Подключаем флюент-валидаторы, ищет все валидаторы там где лежит LoginDtoValidator
            services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>(ServiceLifetime.Transient);

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IActorService, ActorService>();

            return services;
        }
    }
}
