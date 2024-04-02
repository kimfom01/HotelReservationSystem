using HotelBackend.ReservationService.Room;

namespace HotelBackend.ReservationService.Models;

public class RoomType
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public Price? Price { get; set; }

    public IEnumerable<RoomModel>? Rooms { get; set; } // ????????????
}