using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;
using HotelBackend.Reservations.Infrastructure.BackgroundServices;
using HotelBackend.Reservations.Infrastructure.Database;
using HotelBackend.Reservations.Infrastructure.MessageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.Reservations.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration
    )
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddHostedService<PaymentStatusEventHandler>();
        services.AddScoped<IEmailQueuePublisher, EmailQueuePublisher>();
        services.AddTransient<IPaymentQueueSubscriber, PaymentQueueSubscriber>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<ReservationDataContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "reservations"));

            // options.UseInMemoryDatabase("DefaultConnection"); // For testing purpose... will revert to postgres when needed
        });

        return services;
    }
}