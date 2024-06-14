using System.Reflection;
using HotelBackend.Admin.Infrastructure.Authentication;
using HotelBackend.Payments.Infrastructure.Database;
using HotelBackend.Reservations.Application.Contracts.ApiServices;
using HotelBackend.Reservations.Application.Contracts.Authentication;
using HotelBackend.Reservations.Application.Contracts.Database;
using HotelBackend.Reservations.Infrastructure.ApiServices;
using HotelBackend.Reservations.Infrastructure.Authentication;
using HotelBackend.Reservations.Infrastructure.Database;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        services.AddScoped<IReservationsUnitOfWork, ReservationsUnitOfWork>();
        services.AddHttpClient<IRoomApiService, RoomApiService>();
        services.ConfigureOptions<RoomApiOptionsSetup>();

        services.AddDbContext<ReservationDataContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "reservations"));
        });

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
        
        services.AddScoped<IAdminUnitOfWork, AdminUnitOfWork>();

        services.AddDbContext<AdminDataContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "admin"));
        });

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordManager, PasswordManager>();
        services.ConfigureOptions<JwtConfigOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        
        
        services.AddScoped<IPaymentsUnitOfWork, PaymentsUnitOfWork>();

        services.AddDbContext<PaymentDataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "payments")));

        return services;
    }
}