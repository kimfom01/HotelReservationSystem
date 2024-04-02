using HotelBackend.ReservationService.Room.RoomType;

namespace HotelBackend.ReservationService.Room.Price;

public class PriceModel
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomTypeModel? RoomType { get; set; }
}
