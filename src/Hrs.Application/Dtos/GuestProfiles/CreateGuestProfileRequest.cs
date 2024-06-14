namespace Hrs.Application.Dtos.GuestProfiles;

public record CreateGuestProfileRequest
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string ContactEmail { get; init; } = string.Empty;
    public string Sex { get; init; } = string.Empty;
    public int Age { get; init; }
}