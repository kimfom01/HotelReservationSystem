using HotelBackend.ReservationService.Reservation;

namespace HotelBackend.ReservationService.Models;

public class Hotel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public IEnumerable<ReservationModel>? Reservations { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
    
}
