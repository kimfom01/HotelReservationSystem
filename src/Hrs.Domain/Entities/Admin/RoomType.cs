using Hrs.Domain.Entities.Common;

namespace Hrs.Domain.Entities.Admin;

public class RoomType : BaseEntity
{
    internal RoomType(string type, int capacity, string description, decimal roomPrice, Guid hotelId)
    {
        Id = Guid.NewGuid();
        Type = type;
        Capacity = capacity;
        Description = description;
        RoomPrice = roomPrice;
        HotelId = hotelId;
    }

    public string Type { get; private set; }
    public int Capacity { get; private set; }
    public string Description { get; private set; }
    public decimal RoomPrice { get; private set; }
    public Guid HotelId { get; private set; }
    public Hotel? Hotel { get; private set; }

    public IReadOnlyCollection<Room> Rooms => _rooms;

    public static RoomType CreateRoomType(string type, int capacity, string description, decimal roomPrice,
        Guid hotelId)
    {
        return new RoomType(type, capacity, description, roomPrice, hotelId);
    }

    private List<Room> _rooms = new();

    public void CreateRooms(int count, int start, Guid roomTypeId, string roomTypeName, Guid hotelId)
    {
        _rooms = new List<Room>(count);

        for (var i = start; i < start + count; i++)
        {
            _rooms.Add(Room.CreateRoom($"{roomTypeName[0]}{i}", hotelId, roomTypeId));
        }
    }
}