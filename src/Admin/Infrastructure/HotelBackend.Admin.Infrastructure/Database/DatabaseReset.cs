using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBackend.Admin.Infrastructure.Database;

public static class DatabaseReset
{
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AdminDataContext>();

        var migrations = context.Database.GetPendingMigrations();

        if (migrations.Any())
        {
            context.Database.Migrate();
        }

        return app;
    }
}