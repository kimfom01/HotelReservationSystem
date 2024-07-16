using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hrs.Application.Contracts.Authentication;
using Hrs.Common.Options;
using Hrs.Domain.Entities.Admin;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hrs.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtConfigOptions _jwtConfig;

    public JwtProvider(IOptions<JwtConfigOptions> configOptions)
    {
        _jwtConfig = configOptions.Value;
    }

    public string Generate(User user)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Email),
            new Claim("Id", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName)
        ];
        claims.AddRange(
            user.Roles.Select(rol 
                => new Claim("Roles", rol.Name)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var issuer = _jwtConfig.Issuer;
        var audience = _jwtConfig.Audience;

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(issuer, audience, claims,
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(_jwtConfig.ExpiresIn)), signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}