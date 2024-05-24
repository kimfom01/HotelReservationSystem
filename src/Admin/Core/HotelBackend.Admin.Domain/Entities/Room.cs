using HotelBackend.Admin.Domain.Entities.Common;

namespace HotelBackend.Admin.Domain.Entities;

public class Room : BaseEntity
{
    public string RoomNumber { get; set; } = string.Empty;
    public bool Availability { get; set; } = true;
    public Guid HotelId { get; set; }
    public Hotel? Hotel { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomType? RoomType { get; set; }
}
