using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Application.Validators.AuthValidators;
using FluentValidation;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AbsoluteCinema.Tests.TestsApplication;

public class TestValidators : DependencyInjectionTests
{
    [Fact]
    public void Validators_ShouldBeRegistered()
    {
        // Получаем валидатор для RegisterDto
        var validator = ServiceProvider.GetService<IValidator<RegisterDto>>();

        // Проверяем, что валидатор зарегистрирован и имеет корректный тип
        validator.Should().NotBeNull();
        validator.Should().BeOfType<RegisterDtoValidator>();
    }
}