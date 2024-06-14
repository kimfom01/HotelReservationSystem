using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBackend.Reservations.Infrastructure.Database;

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

        var adminDataContext = scope.ServiceProvider.GetRequiredService<AdminDataContext>();

        var adminMigrations = adminDataContext.Database.GetPendingMigrations();

        if (adminMigrations.Any())
        {
            adminDataContext.Database.Migrate();
        }

        return app;
    }
}