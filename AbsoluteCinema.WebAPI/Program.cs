using System.Diagnostics;
using AbsoluteCinema.Infrastructure;
using AbsoluteCinema.Application;
using AbsoluteCinema.Application.DTO.AuthDTO;
using AbsoluteCinema.Application.Validators.AuthValidators;
using AbsoluteCinema.Domain;
using AutoMapper;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
builder.Services.AddDomainDI();
builder.Services.AddApplicationDI();
builder.Services.AddInfrastructureDI(builder.Configuration);

var app = builder.Build();

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
