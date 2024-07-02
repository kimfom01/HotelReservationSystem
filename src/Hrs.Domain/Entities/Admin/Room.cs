using Hrs.Domain.Entities.Common;

namespace Hrs.Domain.Entities.Admin;

public class Room : BaseEntity
{
    internal Room(string roomNumber, Guid hotelId, Guid roomTypeId)
    {
        Id = Guid.NewGuid();
        RoomNumber = $"{roomNumber}";
        HotelId = hotelId;
        RoomTypeId = roomTypeId;
    }

    public string RoomNumber { get; private set; }
    public bool Availability { get; private set; } = true;
    public Guid HotelId { get; private set; }
    public Hotel? Hotel { get; private set; }
    public Guid RoomTypeId { get; private set; }
    public RoomType? RoomType { get; private set; }

    public static Room CreateRoom(string roomNumber, Guid hotelId, Guid roomTypeId)
    {
        var room = new Room(roomNumber, hotelId, roomTypeId);

        return room;
    }

    public void SetFree()
    {
        Availability = true;
    }
    
    public void SetReserved()
    {
        Availability = false;
    }
}