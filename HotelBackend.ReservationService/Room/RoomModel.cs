using HotelBackend.ReservationService.Hotel;
using HotelBackend.ReservationService.Reservation;
using HotelBackend.ReservationService.Room.RoomType;

namespace HotelBackend.ReservationService.Room;

public class RoomModel
{
    public Guid Id { get; set; }
    public string RoomNumber { get; set; }
    public bool? Availability { get; set; } = true;
    public Guid HotelId { get; set; }
    public HotelModel? Hotel { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomTypeModel? RoomType { get; set; }

    public IEnumerable<ReservationModel>? Reservations { get; set; }
}
