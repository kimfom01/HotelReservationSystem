using HotelBackend.Payments.Application.Contracts.Infrastructure;
using HotelBackend.Payments.Infrastructure.MessageBroker;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.Payments.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentStatusPublisher, PaymentStatusPublisher>();
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();

        return services;
    }
}