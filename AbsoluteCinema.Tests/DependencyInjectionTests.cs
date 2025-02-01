using AbsoluteCinema.Application;
using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Application.Validators.AuthValidators;
using AbsoluteCinema.Domain;
using AbsoluteCinema.Infrastructure;
using AutoMapper;
using FluentValidation;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AbsoluteCinema.Tests
{
    public class DependencyInjectionTests
    {
        private readonly ServiceProvider _serviceProvider;

        public DependencyInjectionTests()
        {
            var services = new ServiceCollection();

            // Загружаем тестовую конфигурацию из appsettings.json
            IConfiguration configuration = GetTestConfiguration();
            
            // Подключаем зависимости 
            services.AddDomainDI();
            services.AddApplicationDI();
            services.AddInfrastructureDI(configuration);

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void Validators_ShouldBeRegistered()
        {
            // Получаем валидатор для RegisterDto
            var validator = _serviceProvider.GetService<IValidator<RegisterDto>>();

            // Проверяем, что валидатор зарегистрирован и имеет корректный тип
            validator.Should().NotBeNull();
            validator.Should().BeOfType<RegisterDtoValidator>();
        }

        [Fact]
        public void AutoMapper_ShouldHaveValidConfiguration()
        {
            // Получаем IMapper
            var mapper = _serviceProvider.GetService<IMapper>();

            // Проверяем, что AutoMapper зарегистрирован и конфигурация корректна
            mapper.Should().NotBeNull();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
        
        private IConfiguration GetTestConfiguration()
        {
            var webApiPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "AbsoluteCinema.WebAPI"));
            
            return new ConfigurationBuilder()
                .SetBasePath(webApiPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}
