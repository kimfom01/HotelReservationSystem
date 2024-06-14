using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace Hrs.Infrastructure.Database;

public class ReservationDataContextFactory : IDesignTimeDbContextFactory<ReservationDataContext>
{
    private readonly IConfiguration? _configuration;

    public ReservationDataContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ReservationDataContextFactory()
    {
    }

    public ReservationDataContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ReservationDataContext>();

        builder.UseNpgsql(_configuration?.GetConnectionString("DefaultConnection"),
            options => options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "reservations"));

        return new ReservationDataContext(builder.Options);
    }
}