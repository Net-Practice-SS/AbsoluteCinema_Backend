using AbsoluteCinema.Domain.Constants;
using AbsoluteCinema.Infrastructure.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace AbsoluteCinema.Infrastructure.Seeders;

public class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
    {
        string[] roleNames = [Role.User, Role.Admin];

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new ApplicationRole
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                };

                var result = await roleManager.CreateAsync(role);
                
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating role {roleName}: {error.Description}");
                    }
                }
            }
        }
    }
}