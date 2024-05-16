using HotelBackend.Reservations.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBackend.Reservations.Persistence.Utils;

public static class DatabaseReset
{
    public static async Task SetupDatabase(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        var migrations = await context.Database.GetPendingMigrationsAsync();

        if (migrations.Any())
        {
            await context.Database.MigrateAsync();
        }
    }
}