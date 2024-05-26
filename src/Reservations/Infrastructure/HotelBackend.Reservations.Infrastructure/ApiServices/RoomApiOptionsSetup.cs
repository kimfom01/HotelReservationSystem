using HotelBackend.Common.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HotelBackend.Reservations.Infrastructure.ApiServices;

public class RoomApiOptionsSetup : IConfigureOptions<RoomApiOptions>
{
    private readonly IConfiguration _configuration;

    public RoomApiOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(RoomApiOptions options)
    {
        _configuration.GetSection(nameof(RoomApiOptions)).Bind(options);
    }
}