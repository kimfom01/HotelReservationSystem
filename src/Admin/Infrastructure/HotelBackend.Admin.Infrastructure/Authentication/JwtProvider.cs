using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelBackend.Admin.Application.Contracts.Authentication;
using HotelBackend.Admin.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace HotelBackend.Admin.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("security key here make sure you change this latter and it should be pretty long"));
        var issuer = "issuer here";
        var audience = "audience here";

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.Add(TimeSpan.FromMinutes(5)),
            audience: audience, issuer: issuer, signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}