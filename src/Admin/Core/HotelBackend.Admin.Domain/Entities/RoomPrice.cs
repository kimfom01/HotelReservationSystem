using HotelBackend.Admin.Domain.Entities.Common;

namespace HotelBackend.Admin.Domain.Entities;

public class RoomPrice : BaseEntity
{
    public decimal Value { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomType? RoomType { get; set; }
    public Guid HotelId { get; set; }
    public Hotel? Hotel { get; set; }
}