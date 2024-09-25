using Hrs.Common.Entities;

namespace Hrs.Domain.Entities.Admin;

public class Hotel : BaseEntity
{
    internal Hotel(string name, string location, Guid adminId)
    {
        Name = name;
        Location = location;
        AdminId = adminId;
        Rooms = new List<Room>();
        RoomTypes = new List<RoomType>();
        Users = new List<User>();
    }

    public string Name { get; private set; }
    public string Location { get; private set; }
    public Guid AdminId { get; private set; }

    public IReadOnlyCollection<Room>? Rooms { get; private set; }
    public IReadOnlyCollection<RoomType>? RoomTypes { get; private set; }
    public IReadOnlyCollection<User>? Users { get; private set; }

    public static Hotel CreateHotel(string name, string location, Guid adminId)
    {
        return new Hotel(name, location, adminId);
    }
}