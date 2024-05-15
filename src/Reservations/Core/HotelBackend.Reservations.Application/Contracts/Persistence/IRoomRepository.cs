using HotelBackend.Reservations.Domain.Entities;

namespace HotelBackend.Reservations.Application.Contracts.Persistence;

public interface IRoomRepository : IRepository<Room>
{
    Task<List<Room>> GetAllAvailableRooms(Guid hotelId, CancellationToken cancellationToken);
}