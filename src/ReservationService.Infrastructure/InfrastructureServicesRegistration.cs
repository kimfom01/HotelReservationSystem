using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservationService.Application.Contracts.Database;
using ReservationService.Application.Contracts.Services;
using ReservationService.Infrastructure.Database;
using ReservationService.Infrastructure.Services;

namespace ReservationService.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IReservationsUnitOfWork, ReservationsUnitOfWork>();
        services.AddScoped<IRoomService, RoomService>();

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

        services.AddDbContext<ReservationDataContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("hrs-db"), y =>
                y.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "reservations")));

        return services;
    }
}