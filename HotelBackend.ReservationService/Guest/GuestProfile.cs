using HotelBackend.ReservationService.Reservation;

namespace HotelBackend.ReservationService.Guest;

public class GuestProfile
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactEmail { get; set; }

    public IEnumerable<ReservationModel>? Reservations { get; set; }
}