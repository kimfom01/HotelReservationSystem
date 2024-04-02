using System.Linq.Expressions;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Services;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAvailableRoomsPerRoomCapacity(Guid hotelId, int capacity);
    Task UpdateRoom(Room room);
    Task<Room?> GetRoom(Guid id);
    Task<Room?> GetRoom(Expression<Func<Room, bool>> expression);
    Task<IEnumerable<Room>?> GetRooms();
    Task<int> DeleteRoom(Guid id);
    Task<Room?> PostRoom(Room room);
}
