using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security;
using HotelBackend.Admin.Infrastructure.Authentication;
using Hrs.Application.Contracts.ApiServices;
using Hrs.Application.Contracts.Authentication;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Contracts.Email;
using Hrs.Common.Options;
using Hrs.Infrastructure.ApiServices;
using Hrs.Infrastructure.Authentication;
using Hrs.Infrastructure.Database;
using Hrs.Infrastructure.Email;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Hrs.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, bool isDevelopment, IConfiguration configuration
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

        services.AddScoped<IEmailSender, EmailSender>();
        services.ConfigureOptions<EmailOptionsSetup>();

        var emailOptions = services.BuildServiceProvider()
            .GetRequiredService<IOptions<EmailOptions>>().Value;

        if (isDevelopment)
        {
            services
                .AddFluentEmail(emailOptions.SenderEmail)
                .AddRazorRenderer()
                .AddSmtpSender("localhost", 1025);
        }
        else
        {
            services
                .AddFluentEmail(emailOptions.SenderEmail)
                .AddRazorRenderer()
                .AddSmtpSender(
                    new SmtpClient(emailOptions.Host, emailOptions.Port)
                    {
                        Credentials = new NetworkCredential(
                            emailOptions.SenderEmail,
                            GetSecurePassword(emailOptions.Password)
                        ),
                        EnableSsl = !isDevelopment,
                        DeliveryMethod = SmtpDeliveryMethod.Network
                    }
                );
        }

        return services;
    }

    private static SecureString GetSecurePassword(string password)
    {
        var secureString = new SecureString();

        foreach (var character in password)
        {
            secureString.AppendChar(character);
        }

        return secureString;
    }
}