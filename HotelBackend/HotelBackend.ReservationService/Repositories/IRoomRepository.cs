using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Repositories;

public interface IRoomRepository : IRepository<Room>
{
    Task<List<Room>> GetAllAvailableRooms(Guid hotelId);
}