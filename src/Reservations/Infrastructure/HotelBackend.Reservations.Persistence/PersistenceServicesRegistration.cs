using HotelBackend.Reservations.Application.Contracts.Persistence;
using HotelBackend.Reservations.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBackend.Reservations.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            // options.UseInMemoryDatabase("DefaultConnection"); // For testing purpose... will revert to postgres when needed
        });

        return services;
    }
}