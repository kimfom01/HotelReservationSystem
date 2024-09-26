using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Admin.Application.Contracts.Authentication;
using Admin.Domain.Entities.Admin;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Admin.Infrastructure.Authentication;

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
            new(ClaimTypes.NameIdentifier, user.Email),
            new("Id", user.Id.ToString()),
            new("FirstName", user.FirstName),
            new("LastName", user.LastName)
        ];
        claims.AddRange(
            user.UserRoles.Select(rol 
                => new Claim(ClaimTypes.Role, rol.Role!.Name)));

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