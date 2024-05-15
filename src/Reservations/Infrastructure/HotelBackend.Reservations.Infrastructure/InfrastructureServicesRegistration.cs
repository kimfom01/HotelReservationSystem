using HotelBackend.Reservations.Application.Contracts.Infrastructure;
using HotelBackend.Reservations.Infrastructure.BackgroundServices;
using HotelBackend.Reservations.Infrastructure.MessageBroker;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.Reservations.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services
    )
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddHostedService<PaymentStatusEventHandler>();
        services.AddScoped<IEmailQueuePublisher, EmailQueuePublisher>();
        services.AddTransient<IEmailQueueSubscriber, EmailQueueSubscriber>();

        return services;
    }
}
