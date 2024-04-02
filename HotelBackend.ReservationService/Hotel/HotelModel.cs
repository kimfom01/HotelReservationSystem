using HotelBackend.ReservationService.Models;
using HotelBackend.ReservationService.Reservation;

namespace HotelBackend.ReservationService.Hotel;

public class HotelModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public IEnumerable<ReservationModel>? Reservations { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
    
}
