using Hrs.Application.Dtos.Common;

namespace Hrs.Application.Dtos.Admin.Employees;

public record GetEmployeeResponse : BaseDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public Guid? HotelId { get; init; }
}