using HotelBackend.Reservations.Application.Dtos.Common;

namespace HotelBackend.Reservations.Application.Dtos.GuestProfiles;

public record GetGuestProfileResponse : BaseDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string ContactEmail { get; init; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Sex { get; init; } = string.Empty;
    public int Age { get; init; }
}