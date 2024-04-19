namespace HotelBackend.Domain.Entities;

public class GuestProfile
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactEmail { get; set; }

    public IEnumerable<Reservation>? Reservations { get; set; }
}