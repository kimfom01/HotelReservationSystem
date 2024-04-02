using HotelBackend.ReservationService.Reservation;
using HotelBackend.ReservationService.Room;

namespace HotelBackend.ReservationService.Hotel;

public class HotelModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public IEnumerable<ReservationModel>? Reservations { get; set; }
    public IEnumerable<RoomModel> Rooms { get; set; }
    
}
