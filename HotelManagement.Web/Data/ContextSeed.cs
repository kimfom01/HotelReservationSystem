using HotelManagement.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Web.Data;

public static class ContextSeed
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        try
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
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
            UserName = "superadmin@gmail.com",
            Email = "superadmin@gmail.com",
            FirstName = "Super",
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
                await userManager.AddToRoleAsync(superUser, Roles.Basic.ToString());
                await userManager.AddToRoleAsync(superUser, Roles.Moderator.ToString());
                await userManager.AddToRoleAsync(superUser, Roles.Admin.ToString());
                await userManager.AddToRoleAsync(superUser, Roles.SuperAdmin.ToString());
                await userManager.AddToRoleAsync(superUser, Roles.Guest.ToString());
            }
        }
    }
}