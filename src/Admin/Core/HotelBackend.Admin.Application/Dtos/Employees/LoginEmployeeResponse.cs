using System.Net;

namespace HotelBackend.Admin.Application.Dtos.Employees;

public record LoginEmployeeResponse
{
    public string Token { get; init; } = string.Empty;

    public int ExpiresIn { get; init; }

    public string RefreshToken { get; init; } = string.Empty;

    public int RefreshExpiresIn { get; init; }

    public string TokenType { get; init; } = string.Empty;

    public HttpStatusCode StatusCode { get; init; }
}