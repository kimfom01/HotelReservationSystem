using System.Reflection;
using HotelBackend.Payments.Application.Contracts.Infrastructure.Database;
using HotelBackend.Payments.Infrastructure.Database;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.Payments.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<PaymentDataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "payments")));

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            
            busConfigurator.AddConsumers(Assembly.GetExecutingAssembly());
            
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(configuration["MessageBroker:Host"]!), h =>
                {
                    h.Username(configuration["MessageBroker:User"]!);
                    h.Password(configuration["MessageBroker:Password"]!);
                });
                
                configurator.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}