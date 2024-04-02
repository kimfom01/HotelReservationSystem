namespace HotelBackend.ReservationService.Guest;

public class GuestProfileDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactEmail { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}