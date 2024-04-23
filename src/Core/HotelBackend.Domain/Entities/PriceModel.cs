namespace HotelBackend.Domain.Entities;

public class PriceModel
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomType? RoomType { get; set; }
}
