using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security;
using Hrs.Application.Contracts.Authentication;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Contracts.Email;
using Hrs.Application.Contracts.Services;
using Hrs.Infrastructure.Authentication;
using Hrs.Infrastructure.Database;
using Hrs.Infrastructure.Email;
using Hrs.Infrastructure.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hrs.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration
    )
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

        services.AddScoped<IAdminUnitOfWork, AdminUnitOfWork>();

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordManager, PasswordManager>();
        services.ConfigureOptions<JwtConfigOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();


        services.AddScoped<IPaymentsUnitOfWork, PaymentsUnitOfWork>();

        services.AddScoped<IEmailSender, EmailSender>();
        services.ConfigureOptions<EmailOptionsSetup>();

        if (environment.IsDevelopment())
        {
            services
                .AddFluentEmail(configuration.GetValue<string>("SenderEmail"))
                .AddRazorRenderer()
                .AddSmtpSender("localhost", 1025);
        }
        else
        {
            services
                .AddFluentEmail(configuration.GetValue<string>("SenderEmail"))
                .AddRazorRenderer()
                .AddSmtpSender(
                    new SmtpClient(configuration.GetValue<string>("Host"), configuration.GetValue<int>("Port"))
                    {
                        Credentials = new NetworkCredential(
                            configuration.GetValue<string>("SenderEmail"),
                            GetSecurePassword(configuration.GetValue<string>("Password")!)
                        ),
                        EnableSsl = !environment.IsDevelopment(),
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