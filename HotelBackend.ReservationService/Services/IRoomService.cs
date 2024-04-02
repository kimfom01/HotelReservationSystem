using HotelBackend.ReservationService.Dtos;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Services;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAvailableRoomsPerRoomCapacity(Guid hotelId);
    Task<Room?> GetRoom(Guid id);
}
