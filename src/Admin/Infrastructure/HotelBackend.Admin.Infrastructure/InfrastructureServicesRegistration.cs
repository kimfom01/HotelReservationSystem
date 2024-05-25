using HotelBackend.Admin.Application.Contracts.Authentication;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Infrastructure.Authentication;
using HotelBackend.Admin.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.Admin.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration
    )
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<AdminDataContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "admin"));

            // options.UseInMemoryDatabase("DefaultConnection"); // For testing purpose... will revert to postgres when needed
        });

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordManager, PasswordManager>();

        return services;
    }
}