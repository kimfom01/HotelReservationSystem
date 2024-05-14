using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Infrastructure.MessageBroker;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentStatusPublisher, PaymentStatusPublisher>();
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();

        return services;
    }
}