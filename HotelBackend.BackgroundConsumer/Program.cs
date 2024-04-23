using HotelBackend.Application;
using HotelBackend.BackgroundConsumer.Consumer;
using HotelBackend.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((_, services) =>
{
    IConfiguration consoleConfig = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddUserSecrets<Program>()
        .Build();

    services.ConfigureApplicationServices(consoleConfig);
    services.ConfigurePersistenceServices(consoleConfig);
    services.AddScoped<RabbitMqConsumer>();
    services.AddTransient<IConnectionFactory, ConnectionFactory>();
});

var app = builder.Build();

var consumer = app.Services.GetRequiredService<RabbitMqConsumer>();

Console.WriteLine("Listening for event...");
await consumer.ExecuteAsync();