using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.Contracts.Features;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAvailableRoomsPerRoomCapacity(Guid hotelId);
    Task<Room?> GetRoom(Guid id);
}
