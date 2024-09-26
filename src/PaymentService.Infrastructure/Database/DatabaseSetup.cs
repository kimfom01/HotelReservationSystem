using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentService.Infrastructure.Database;

public static class DatabaseSetup
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var paymentDataContext = scope.ServiceProvider.GetRequiredService<PaymentDataContext>();

        var paymentsMigrations = paymentDataContext.Database.GetPendingMigrations();

        if (paymentsMigrations.Any())
        {
            paymentDataContext.Database.Migrate();
        }

        return app;
    }
}