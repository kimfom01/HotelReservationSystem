using HotelBackend.ReservationService.Room.Price;

namespace HotelBackend.ReservationService.Room.RoomType;

public class RoomTypeModel
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public PriceModel? Price { get; set; }

    public IEnumerable<RoomModel>? Rooms { get; set; } // ????????????
}