using Hrs.Application.Dtos.Common;

namespace Hrs.Application.Dtos.GuestProfiles;

public record GetGuestProfileResponse : BaseDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string ContactEmail { get; init; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Sex { get; init; } = string.Empty;
    public int Age { get; init; }
}