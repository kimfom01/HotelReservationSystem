namespace HotelBackend.Reservations.Domain.Entities;

public class GuestProfile
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;

    public IEnumerable<Reservation>? Reservations { get; set; }
}