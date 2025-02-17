using AbsoluteCinema.Infrastructure;
using AbsoluteCinema.Application;
using AbsoluteCinema.Domain;
using System.Text.Json.Serialization;
using AbsoluteCinema.Infrastructure.DbContexts;
using AbsoluteCinema.Infrastructure.Identity.Data;
using AbsoluteCinema.Infrastructure.Seeders;
using AbsoluteCinema.WebAPI.Filters;
using Microsoft.AspNetCore.Identity;
using AbsoluteCinema.WebAPI.Swagger;
using Microsoft.OpenApi.Models;

string reactClientCORSPolicy = "reactClientCORSPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(reactClientCORSPolicy, policy =>
    {
        policy.WithOrigins(builder.Configuration["ClientAddress"]!) 
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

    // Знімаємо правило на точні назви при запитах
    //options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = @"Bearer (paste here your token (remove all brackets) )",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
        });

    o.OperationFilter<AuthorizeCheckOperationFilter>();

    o.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "AbsoluteCinema API - v1",
        Version = "v1"
    });
});

//Dependency Injection
builder.Services.AddDomainDI();
builder.Services.AddApplicationDI(builder.Configuration);
builder.Services.AddInfrastructureDI(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    
    // Запускаем сидер для статусов тикетов
    await TicketStatusSeeder.SeedTicketStatusesAsync(context);
    
    // Запускаем сидер для ролей
    await RoleSeeder.SeedRolesAsync(roleManager);

    // Запускаем сидер для Hall
    await HallSeeder.SeedHallsAsync(context);

    // Запускаем сидер для Session
    await SessionSeeder.SeedSessionsAsync(context);
}

// Запускаем сидер для заповнення даних Movie, Genres, Actors, MovieGenres, MovieActors
await TmdbSeeder.SeedTmdbDataAsync(app.Services);

app.UseCors(reactClientCORSPolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
