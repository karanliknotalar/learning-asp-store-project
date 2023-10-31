using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructure.Extensions;

public static class ApplicationExtension
{
    public static void AutoMigration(this IApplicationBuilder builder)
    {
        var context = builder
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<RepositoryContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }

    public static void ConfigureLocalization(this WebApplication application)
    {
        application.UseRequestLocalization(options =>
        {
            options.AddSupportedCultures("tr-TR")
                .AddSupportedUICultures("tr-TR")
                .SetDefaultCulture("tr-TR");
        });
    }

    public static async void ConfigureDefaultAdminUser(this IApplicationBuilder builder)
    {
        const string adminUser = "Admin";
        const string adminPassword = "admin";

        UserManager<IdentityUser> userManager = builder.ApplicationServices
            .CreateAsyncScope()
            .ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();

        RoleManager<IdentityRole> roleManager = builder.ApplicationServices
            .CreateAsyncScope()
            .ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        IdentityUser user = await userManager.FindByNameAsync(adminUser);

        if (user is null)
        {
            user = new IdentityUser()
            {
                UserName = adminUser,
                Email = "admin@admin.com",
                PhoneNumber = "5551112233",
            };
            var result = await userManager.CreateAsync(user, adminPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Admin user could not created.");
            }

            var roleResult = await userManager.AddToRolesAsync(user,
                roleManager.Roles
                    .Select(r => r.Name)
                    .ToList()
            );

            if (!roleResult.Succeeded)
            {
                throw new Exception("System have problems with role definition for admin");
            }
        }
    }
}