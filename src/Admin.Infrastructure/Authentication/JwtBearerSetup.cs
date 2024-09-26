using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Admin.Infrastructure.Authentication;

public class JwtBearerSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtConfigOptions _configOptions;

    public JwtBearerSetup(IOptions<JwtConfigOptions> configOptions)
    {
        _configOptions = configOptions.Value;
    }

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = _configOptions.Audience,
            ValidIssuer = _configOptions.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configOptions.Key)),
            ClockSkew = TimeSpan.Zero
        };
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}