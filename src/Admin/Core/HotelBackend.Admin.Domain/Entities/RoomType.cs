using HotelBackend.Admin.Domain.Entities.Common;

namespace HotelBackend.Admin.Domain.Entities;

public class RoomType : BaseEntity
{
    public string Type { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal RoomPrice { get; set; }
    public Guid HotelId { get; set; }
    public Hotel? Hotel { get; set; }

    public IEnumerable<Room>? Rooms { get; set; }
}