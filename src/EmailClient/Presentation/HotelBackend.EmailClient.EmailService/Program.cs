using HotelBackend.EmailClient.Application;
using HotelBackend.EmailClient.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        
        services.AddLogging(opt =>
        {
            opt.AddSimpleConsole(options =>
            {
                options.TimestampFormat = "[HH:mm:ss] ";
            });
        });
    }
);


var app = builder.Build();

Console.WriteLine("Listening for email event...");

app.Run();