namespace HotelBackend.Application.Dtos.GuestProfiles;

public class CreateGuestProfileDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
}