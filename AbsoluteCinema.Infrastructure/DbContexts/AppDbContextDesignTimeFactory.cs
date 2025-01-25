using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AbsoluteCinema.Infrastructure.DbContexts
{
    // Фабрика для EF Core, которая создаёт AppDbContext во время команд миграций крч.
    
    public class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "AbsoluteCinema.WebAPI"));
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
            
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(connectionString);
            
            return new AppDbContext(builder.Options);
        }
    }
}