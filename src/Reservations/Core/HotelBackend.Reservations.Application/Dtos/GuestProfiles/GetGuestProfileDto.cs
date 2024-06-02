using HotelBackend.Reservations.Application.Dtos.Common;

namespace HotelBackend.Reservations.Application.Dtos.GuestProfiles;

public class GetGuestProfileDto : BaseDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Sex { get; set; } = string.Empty;
    public int Age { get; set; }
}