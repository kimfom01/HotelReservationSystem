using HotelBackend.Reservations.Domain.Entities.Common;

namespace HotelBackend.Reservations.Domain.Entities.Reservation;

public class GuestProfile : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public int Age { get; set; }

    public IEnumerable<Reservation>? Reservations { get; set; }
}