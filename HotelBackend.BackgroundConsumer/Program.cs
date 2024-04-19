using HotelBackend.Application;
using HotelBackend.BackgroundConsumer.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.ConfigureApplicationServices(context.Configuration);
    services.AddHostedService<RabbitMqConsumer>();
    services.AddTransient<IConnectionFactory, ConnectionFactory>();
});

var app = builder.Build();

await app.RunAsync();