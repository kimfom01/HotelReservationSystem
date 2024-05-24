using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace HotelBackend.Admin.Infrastructure.Database;

public class AdminDataContextFactory : IDesignTimeDbContextFactory<AdminDataContext>
{
    private readonly IConfiguration? _configuration;

    public AdminDataContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AdminDataContextFactory()
    {
    }

    public AdminDataContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AdminDataContext>();

        builder.UseNpgsql(_configuration?.GetConnectionString("DefaultConnection"),
            options => options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "admin"));

        return new AdminDataContext(builder.Options);
    }
}