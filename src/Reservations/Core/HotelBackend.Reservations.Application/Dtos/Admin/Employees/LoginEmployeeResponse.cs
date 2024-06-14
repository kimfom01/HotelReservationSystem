namespace HotelBackend.Reservations.Application.Dtos.Admin.Employees;

public record LoginEmployeeResponse
{
    public string Token { get; init; } = string.Empty;

    public string TokenType { get; init; } = string.Empty;
}