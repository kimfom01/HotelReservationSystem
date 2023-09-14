using DataAccess.Models;

namespace Api.Services;

public interface IRoomService
{
    Task<IEnumerable<Room>?> GetAvailableRoomsPerRoomCapacity(int hotelId, int capacity);
    Task UpdateRoom(Room room);
}
