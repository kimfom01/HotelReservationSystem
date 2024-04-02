using HotelBackend.ReservationService.Repositories;

namespace HotelBackend.ReservationService.Room;

public interface IRoomRepository : IRepository<RoomModel>
{
    Task<List<RoomModel>> GetAllAvailableRooms(Guid hotelId);
}