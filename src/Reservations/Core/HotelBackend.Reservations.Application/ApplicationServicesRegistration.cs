using System.Reflection;
using FluentValidation;
using HotelBackend.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBackend.Reservations.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(
        this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.Configure<Config>(config.GetSection(nameof(Config)));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}