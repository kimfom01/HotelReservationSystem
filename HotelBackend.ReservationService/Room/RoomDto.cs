using HotelBackend.ReservationService.Room.RoomType;

namespace HotelBackend.ReservationService.Room;

public class RoomDto
{
    public Guid Id { get; set; }
    public string RoomNumber { get; set; }
    public bool Availability { get; set; }
    public Guid HotelId { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomTypeDto? RoomType { get; set; }
}