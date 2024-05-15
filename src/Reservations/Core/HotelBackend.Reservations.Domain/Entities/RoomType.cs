namespace HotelBackend.Reservations.Domain.Entities;

public class RoomType
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Description { get; set; } = string.Empty;
    public PriceModel? Price { get; set; }

    public IEnumerable<Room>? Rooms { get; set; } 
}