using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security;
using Admin.Application.Contracts.Authentication;
using Admin.Application.Contracts.Database;
using Admin.Application.Contracts.Email;
using Admin.Infrastructure.Authentication;
using Admin.Infrastructure.Database;
using Admin.Infrastructure.Email;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Admin.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IWebHostEnvironment environment
    )
    {
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
        services.ConfigureOptions<JwtBearerSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        
        services.AddScoped<IEmailSender, EmailSender>();
        services.ConfigureOptions<EmailOptionsSetup>();
        
        var emailOptions = services.BuildServiceProvider()
            .GetRequiredService<IOptions<EmailOptions>>().Value;

        if (environment.IsDevelopment())
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