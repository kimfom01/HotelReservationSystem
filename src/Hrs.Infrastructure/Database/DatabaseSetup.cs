using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hrs.Infrastructure.Database;

public static class DatabaseSetup
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var adminDataContext = scope.ServiceProvider.GetRequiredService<AdminDataContext>();

        var adminMigrations = adminDataContext.Database.GetPendingMigrations();

        if (adminMigrations.Any())
        {
            adminDataContext.Database.Migrate();
        }

        var paymentDataContext = scope.ServiceProvider.GetRequiredService<PaymentDataContext>();

        var paymentsMigrations = paymentDataContext.Database.GetPendingMigrations();

        if (paymentsMigrations.Any())
        {
            paymentDataContext.Database.Migrate();
        }

        return app;
    }
}