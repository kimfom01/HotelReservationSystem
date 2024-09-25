using Hrs.Common.Dtos;

namespace Hrs.Application.Dtos.Admin.Users;

public record GetUserResponse : BaseDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public Guid? HotelId { get; init; }
}