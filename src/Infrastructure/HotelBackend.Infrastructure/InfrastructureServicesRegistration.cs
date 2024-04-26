using System.Net;
using System.Net.Mail;
using System.Security;
using HotelBackend.Application.Contracts.Infrastructure;
using HotelBackend.Application.Models;
using HotelBackend.Infrastructure.EmailProvider;
using HotelBackend.Infrastructure.RabbitMqService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace HotelBackend.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IReservationQueueService, ReservationQueueService>();
        services.AddScoped<IEmailQueueService, EmailQueueService>();
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IEmailSender, EmailSender>();


        var emailOption = new EmailOption();

        configuration.GetSection(nameof(emailOption)).Bind(emailOption);

        services
            .AddFluentEmail(emailOption.SenderEmail)
            .AddRazorRenderer()
            .AddSmtpSender("localhost", 1025);


        // services
        //     .AddFluentEmail(emailOption.SenderEmail)
        //     .AddRazorRenderer()
        //     .AddSmtpSender(new SmtpClient(emailOption.Host, emailOption.Port)
        //     {
        //         Credentials = new NetworkCredential(emailOption.SenderEmail,
        //             GetSecurePassword(emailOption.Password)),
        //         EnableSsl = true,
        //         DeliveryMethod = SmtpDeliveryMethod.Network
        //     });
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