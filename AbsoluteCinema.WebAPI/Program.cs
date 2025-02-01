using System.Diagnostics;
using AbsoluteCinema.Infrastructure;
using AbsoluteCinema.Application;
using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Application.Validators.AuthValidators;
using AbsoluteCinema.Domain;
using AutoMapper;
using FluentValidation;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    // Додаємо конвертер для серіалізації enum як рядків
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
builder.Services.AddDomainDI();
builder.Services.AddApplicationDI();
builder.Services.AddInfrastructureDI(builder.Configuration);

var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var validator = scope.ServiceProvider.GetService<IValidator<RegisterDto>>();
//     if (validator == null)
//     {
//         throw new Exception("Validator could not be found.");
//     }
//     Console.WriteLine("Validator OK");
// }
//
// using (var scope = app.Services.CreateScope())
// {
//     var mapper = scope.ServiceProvider.GetService<IMapper>();
//     if (mapper == null)
//     {
//         throw new Exception("AutoMapper не зарегистрирован!");
//     }
//     Console.WriteLine("Mapping OK");
//     
//     var configuration = mapper.ConfigurationProvider;
//     configuration.AssertConfigurationIsValid(); // Выбросит исключение, если есть ошибки в маппингах
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
