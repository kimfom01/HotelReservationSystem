namespace HotelBackend.Reservations.Application.Dtos.GuestProfiles;

public class CreateGuestProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
}