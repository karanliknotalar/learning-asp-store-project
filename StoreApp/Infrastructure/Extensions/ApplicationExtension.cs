using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Contracts;

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
}