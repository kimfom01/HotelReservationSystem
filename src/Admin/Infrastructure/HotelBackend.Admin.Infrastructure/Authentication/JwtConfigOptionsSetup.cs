using HotelBackend.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HotelBackend.Admin.Infrastructure.Authentication;

public class JwtConfigOptionsSetup : IConfigureOptions<JwtConfigOptions>
{
    private readonly IConfiguration _configuration;

    public JwtConfigOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtConfigOptions optionses)
    {
        _configuration.GetSection(nameof(JwtConfigOptions)).Bind(optionses);
    }
}