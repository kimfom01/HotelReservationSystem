namespace HotelBackend.Reservations.Application.Dtos.Admin.Employees;

public record LoginEmployeeRequest
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}