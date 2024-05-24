using HotelBackend.Admin.Domain.Entities.Common;

namespace HotelBackend.Admin.Domain.Entities;

public class Hotel : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;

    public IEnumerable<Room>? Rooms { get; set; }
    public IEnumerable<RoomType>? RoomTypes { get; set; }
}