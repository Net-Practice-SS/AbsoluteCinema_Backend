using AbsoluteCinema.Application.Mappings.AuthMapping;
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
           
            // Подключаем маппер
            services.AddAutoMapper(typeof(LoginMappingProfile).Assembly);
            
            return services;
        }
    }
}
