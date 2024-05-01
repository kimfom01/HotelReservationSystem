using HotelBackend.Application;
using HotelBackend.EmailService.Listeners;
using HotelBackend.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((builderContext, services) =>
{
    IConfiguration consoleConfig = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddUserSecrets<Program>()
        .Build();

    services.ConfigureApplicationServices(consoleConfig);
    services.ConfigureInfrastructureServices(consoleConfig, true);
    services.AddScoped<EmailerListener>();
    services.AddTransient<IConnectionFactory, ConnectionFactory>();
});

var app = builder.Build();

var emailerListener = app.Services.GetRequiredService<EmailerListener>();

Console.WriteLine("Listening for email event...");
await emailerListener.ExecuteAsync();