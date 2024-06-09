using HotelBackend.EmailClient.Application;
using HotelBackend.EmailClient.Infrastructure;
using Microsoft.Extensions.Configuration;
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
        services.ConfigureApplicationServices();
        services.ConfigureInfrastructureServices(builderContext.HostingEnvironment.IsDevelopment(), consoleConfig);
    }
);

var app = builder.Build();

Console.WriteLine("Listening for email event...");

app.Run();