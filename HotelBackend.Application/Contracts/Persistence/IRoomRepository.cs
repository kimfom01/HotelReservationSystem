using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.Contracts.Persistence;

public interface IRoomRepository : IRepository<Room>
{
    Task<List<Room>> GetAllAvailableRooms(Guid hotelId, CancellationToken cancellationToken);
}