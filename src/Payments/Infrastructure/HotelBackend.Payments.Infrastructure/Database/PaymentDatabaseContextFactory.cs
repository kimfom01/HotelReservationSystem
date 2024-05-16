using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelBackend.Payments.Infrastructure.Database;

public class PaymentDatabaseContextFactory : IDesignTimeDbContextFactory<PaymentDataContext>
{
    private readonly IConfiguration? _configuration;

    public PaymentDatabaseContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public PaymentDatabaseContextFactory()
    {
    }

    public PaymentDataContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<PaymentDataContext>();

        builder.UseNpgsql(_configuration?.GetConnectionString("DefaultConnection"));

        return new PaymentDataContext(builder.Options);
    }
}