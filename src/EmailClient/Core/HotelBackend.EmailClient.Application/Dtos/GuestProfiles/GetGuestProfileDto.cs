using HotelBackend.EmailClient.Application.Dtos.Common;

namespace HotelBackend.EmailClient.Application.Dtos.GuestProfiles;

public class GetGuestProfileDto : BaseDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
}