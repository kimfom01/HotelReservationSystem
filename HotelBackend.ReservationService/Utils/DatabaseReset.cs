using HotelBackend.ReservationService.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.ReservationService.Utils;

public static class DatabaseReset
{
    public static async Task SetupDatabase(IServiceScope scope, IWebHostEnvironment environment)
    {
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        if (environment.IsDevelopment())
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
        else
        {
            await context.Database.MigrateAsync();
        }
    }
}
