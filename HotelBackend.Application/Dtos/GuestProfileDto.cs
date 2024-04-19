namespace HotelBackend.Application.Dtos;

public class GuestProfileDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
}