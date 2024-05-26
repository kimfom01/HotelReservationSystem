using HotelBackend.Admin.Domain.Entities.Common;

namespace HotelBackend.Admin.Domain.Entities;

public class Hotel : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public Guid AdminId { get; set; }

    public IEnumerable<Room>? Rooms { get; set; }
    public IEnumerable<RoomType>? RoomTypes { get; set; }
    public IEnumerable<Employee>? Employees { get; set; }
}