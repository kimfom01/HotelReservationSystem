using System.Reflection;
using HotelBackend.Application.Contracts.Features;
using HotelBackend.Application.Features;
using HotelBackend.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBackend.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(
        this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IPricingService, PricingService>();
        services.Configure<RabbitMqOption>(config.GetSection(nameof(RabbitMqOption)));

        return services;
    }
}