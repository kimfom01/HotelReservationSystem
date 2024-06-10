using System.Net;

namespace HotelBackend.Admin.Application.Dtos.Employees;

public record LoginEmployeeResponse
{
    public string Token { get; init; } = string.Empty;

    public string TokenType { get; init; } = string.Empty;
}