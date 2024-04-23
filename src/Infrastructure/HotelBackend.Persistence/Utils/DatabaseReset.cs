using HotelBackend.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBackend.Persistence.Utils;

public static class DatabaseReset
{
    public static async Task SetupDatabase(IServiceScope scope, bool environmentIsDevelopment)
    {
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        if (environmentIsDevelopment)
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
