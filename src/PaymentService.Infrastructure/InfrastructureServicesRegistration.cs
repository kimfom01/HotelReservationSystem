using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Contracts.Database;
using PaymentService.Infrastructure.Database;

namespace PaymentService.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IPaymentsUnitOfWork, PaymentsUnitOfWork>();

        services.Configure<MassTransitHostOptions>(options => { options.WaitUntilStarted = true; });

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumers(Assembly.GetExecutingAssembly());

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var config = context.GetRequiredService<IConfiguration>();

                configurator.Host(config.GetConnectionString("rabbitmq"));

                configurator.ConfigureEndpoints(context);
            });
        });

        services.AddDbContext<PaymentDataContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("hrs-db"), y =>
                y.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "payments")));

        return services;
    }
}