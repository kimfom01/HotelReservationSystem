using Microsoft.AspNetCore.Identity;
using Web.Models;

namespace Web.Data;

public static class ContextSeed
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        try
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.SystemAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.HotelAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Guest.ToString()));
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occured seeding the database");
        }
    }

    public static async Task SeedSuperAdminAsync(IServiceProvider serviceProvider)
    {
        var superUser = new ApplicationUser
        {
            UserName = "systemadmin@azurehotels.com",
            Email = "systemadmin@azurehotels.com",
            FirstName = "System",
            MiddleName = "User",
            LastName = "Admin",
            PhoneNumber = "+71234567890",
            EmailConfirmed = true,
            DateOfBirth = DateTime.Now,
        };

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (userManager.Users.All(u => u.Id != superUser.Id))
        {
            var user = await userManager.FindByEmailAsync(superUser.Email);

            if (user is null)
            {
                await userManager.CreateAsync(superUser, "123Pa$$word.");
                await userManager.AddToRoleAsync(superUser, Roles.SystemAdmin.ToString());
                await userManager.AddToRoleAsync(superUser, Roles.HotelAdmin.ToString());
                await userManager.AddToRoleAsync(superUser, Roles.Guest.ToString());
            }
        }
    }
}