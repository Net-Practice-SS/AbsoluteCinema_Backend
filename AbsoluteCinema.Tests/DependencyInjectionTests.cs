using AbsoluteCinema.Application;
using AbsoluteCinema.Domain;
using AbsoluteCinema.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AbsoluteCinema.Tests
{
    public class DependencyInjectionTests
    {
        protected readonly ServiceProvider ServiceProvider;

        protected DependencyInjectionTests()
        {
            var services = new ServiceCollection();

            // Загружаем тестовую конфигурацию из appsettings.json
            var configuration = GetTestConfiguration();
            
            // Подключаем зависимости 
            services.AddDomainDI();
            services.AddApplicationDI(configuration);
            services.AddInfrastructureDI(configuration);

            ServiceProvider = services.BuildServiceProvider();
        }

        private static IConfiguration GetTestConfiguration()
        {
            var webApiPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "AbsoluteCinema.WebAPI"));
            
            return new ConfigurationBuilder()
                .SetBasePath(webApiPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}
