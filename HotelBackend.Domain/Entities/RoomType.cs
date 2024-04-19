namespace HotelBackend.Domain.Entities;

public class RoomType
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public PriceModel? Price { get; set; }

    public IEnumerable<Room>? Rooms { get; set; } // ????????????
}