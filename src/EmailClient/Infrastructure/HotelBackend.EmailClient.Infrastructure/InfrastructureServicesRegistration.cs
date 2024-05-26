using System.Net;
using System.Net.Mail;
using System.Security;
using HotelBackend.EmailClient.Application.Contracts.Infrastructure;
using HotelBackend.Common.Models.Options;
using HotelBackend.EmailClient.Infrastructure.EmailProvider;
using HotelBackend.EmailClient.Infrastructure.MessageBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.EmailClient.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment
    )
    {
        services.AddScoped<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<EmailQueueSubscriber>();
        services.ConfigureOptions<EmailQueueOptionsSetup>();
        services.ConfigureOptions<EmailOptionsSetup>();

        var emailOption = new EmailOptions();

        // TODO: figure out how to configure smtp using IConfigureNamedOptions pattern
        configuration.GetSection(nameof(EmailOptions)).Bind(emailOption);

        if (isDevelopment)
        {
            services
                .AddFluentEmail(emailOption.SenderEmail)
                .AddRazorRenderer()
                .AddSmtpSender("localhost", 1025);
        }
        else
        {
            services
                .AddFluentEmail(emailOption.SenderEmail)
                .AddRazorRenderer()
                .AddSmtpSender(
                    new SmtpClient(emailOption.Host, emailOption.Port)
                    {
                        Credentials = new NetworkCredential(
                            emailOption.SenderEmail,
                            GetSecurePassword(emailOption.Password)
                        ),
                        EnableSsl = false,
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