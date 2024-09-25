using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ReservationService.Infrastructure.Database;

public static class DatabaseSetup
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var reservationDataContext = scope.ServiceProvider.GetRequiredService<ReservationDataContext>();

        var reservationsMigrations = reservationDataContext.Database.GetPendingMigrations();

        if (reservationsMigrations.Any())
        {
            reservationDataContext.Database.Migrate();
        }

        return app;
    }
}