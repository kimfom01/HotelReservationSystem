using HotelBackend.Payments.Application.Contracts.Infrastructure.Database;
using HotelBackend.Payments.Application.Contracts.Infrastructure.MessageBroker;
using HotelBackend.Payments.Infrastructure.Database;
using HotelBackend.Payments.Infrastructure.MessageBroker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.Payments.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IPaymentStatusPublisher, PaymentStatusPublisher>();
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<PaymentDataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}