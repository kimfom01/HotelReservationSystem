using System.Linq.Expressions;
using DataAccess.Models;

namespace Api.Services;

public interface IRoomService
{
    Task<IEnumerable<Room>?> GetAvailableRoomsPerRoomCapacity(int hotelId, int capacity);
    Task UpdateRoom(Room room);
    Task<Room?> GetRoom(int id);
    Task<Room?> GetRoom(Expression<Func<Room, bool>> expression);
    Task<IEnumerable<Room>?> GetRooms();
    Task<IEnumerable<Room>?> GetRooms(Expression<Func<Room, bool>> expression);
    Task<int> DeleteRoom(int id);
    Task<Room> PostRoom(Room room);
}
