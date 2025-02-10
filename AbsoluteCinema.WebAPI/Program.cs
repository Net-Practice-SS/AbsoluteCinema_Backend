using System.Diagnostics;
using AbsoluteCinema.Infrastructure;
using AbsoluteCinema.Application;
using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Application.Validators.AuthValidators;
using AbsoluteCinema.Domain;
using AutoMapper;
using FluentValidation;
using System.Text.Json.Serialization;
using AbsoluteCinema.Infrastructure.DbContexts;
using AbsoluteCinema.Infrastructure.Seeders;
using AbsoluteCinema.WebAPI.Filters;
using Microsoft.EntityFrameworkCore;

string reactClientCORSPolicy = "reactClientCORSPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(reactClientCORSPolicy, policy =>
    {
        policy.WithOrigins(builder.Configuration["ClientAddress"]) 
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); 
    });
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilterAttribute>();
}).AddJsonOptions(options =>
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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Запускаем сидер для статусов тикетов
    await TicketStatusSeeder.SeedTicketStatusesAsync(context);
}

app.UseCors(reactClientCORSPolicy);

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
