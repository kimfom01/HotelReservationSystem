using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Infrastructure.BackgroundServices;
using HotelBackend.Infrastructure.MessageBroker;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.Infrastructure;

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
