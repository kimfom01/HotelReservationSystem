using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelBackend.Reservations.Persistence.Data;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    private readonly IConfiguration? _configuration;

    public DatabaseContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DatabaseContextFactory()
    {
    }

    public DatabaseContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DatabaseContext>();

        builder.UseNpgsql(_configuration?.GetConnectionString("DefaultConnection"));

        return new DatabaseContext(builder.Options);
    }
}