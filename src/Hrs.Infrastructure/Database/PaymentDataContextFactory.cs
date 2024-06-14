using Hrs.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace HotelBackend.Payments.Infrastructure.Database;

public class PaymentDataContextFactory : IDesignTimeDbContextFactory<PaymentDataContext>
{
    private readonly IConfiguration? _configuration;

    public PaymentDataContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public PaymentDataContextFactory()
    {
    }

    public PaymentDataContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<PaymentDataContext>();

        builder.UseNpgsql(_configuration?.GetConnectionString("DefaultConnection"),
            options => options.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "payments"));

        return new PaymentDataContext(builder.Options);
    }
}