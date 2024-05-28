namespace HotelBackend.Reservations.Application.Dtos.GuestProfiles;

public class CreateGuestProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public int Age { get; set; }
    public bool Adult { get; set; }
}