using Microsoft.Extensions.Configuration;
﻿using AbsoluteCinema.Domain.Interfaces;
using AbsoluteCinema.Infrastructure.DbContexts;
using AbsoluteCinema.Infrastructure.EntitiesIdentity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AbsoluteCinema.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
            
            //Inject repositories

            return services;
        }
    }
}
