using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hrs.Application.Contracts.Authentication;
using Hrs.Domain.Entities.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Hrs.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;

    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(Employee employee)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, employee.Email),
            new Claim("Id", employee.Id.ToString()),
            new Claim("FirstName", employee.FirstName),
            new Claim("LastName", employee.LastName),
            new Claim("Role", employee.Role)
        ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Key")!));
        var issuer = _configuration.GetValue<string>("Issuer");
        var audience = _configuration.GetValue<string>("Audience");

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(issuer, audience, claims,
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(_configuration.GetValue<double>("ExpiresIn"))),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}