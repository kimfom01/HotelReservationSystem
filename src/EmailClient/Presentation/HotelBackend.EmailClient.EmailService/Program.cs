using HotelBackend.EmailClient.Application;
using HotelBackend.EmailClient.Infrastructure;
using HotelBackend.EmailClient.Infrastructure.Listeners;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(
    (builderContext, services) =>
    {
        IConfiguration consoleConfig = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables()
            .Build();
        services.ConfigureApplicationServices(consoleConfig);
        services.ConfigureInfrastructureServices(
            consoleConfig,
            builderContext.HostingEnvironment.IsDevelopment()
        );
    }
);

var app = builder.Build();

var scope = app.Services.CreateScope();

var emailerListener = scope.ServiceProvider.GetRequiredService<EmailerListener>();

Console.WriteLine("Listening for email event...");
await emailerListener.ExecuteAsync();
