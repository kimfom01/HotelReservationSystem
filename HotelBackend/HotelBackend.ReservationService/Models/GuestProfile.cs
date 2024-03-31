namespace HotelBackend.ReservationService.Models;

public class GuestProfile
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactEmail { get; set; }
    // public string FullName => $"{FirstName} {LastName}"; move to dto

    public IEnumerable<Reservation>? Reservations { get; set; }
}