using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AbsoluteCinema.Tests.TestsInfrastructure;

public class TestMappings : DependencyInjectionTests
{
    [Fact] 
    public void AutoMapper_ShouldHaveValidConfiguration() 
    {
        // Получаем IMapper
        var mapper = ServiceProvider.GetService<IMapper>();

        // Проверяем, что AutoMapper зарегистрирован и конфигурация корректна
        mapper.Should().NotBeNull();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}