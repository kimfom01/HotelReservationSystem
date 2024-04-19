using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Infrastructure.RabbitMqService;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IQueueService, QueueService>();
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();

        return services;
    }
}