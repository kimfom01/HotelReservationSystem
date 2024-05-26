using HotelBackend.Reservations.Application.Contracts.ApiServices;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.MessageBroker;
using HotelBackend.Reservations.Infrastructure.ApiServices;
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
        services.AddHttpClient<IRoomApiService, RoomApiService>();
        services.ConfigureOptions<RoomApiOptionsSetup>();
        services.ConfigureOptions<EmailQueueOptionsSetup>();
        services.ConfigureOptions<PaymentQueueOptionsSetup>();

        services.AddDbContext<ReservationDataContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "reservations"));
        });

        return services;
    }
}