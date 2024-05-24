using HotelBackend.Admin.Domain.Entities;

namespace HotelBackend.Admin.Application.Contracts.Infrastructure.Database;

public interface IRoomRepository : IRepository<Room>
{
    Task<List<Room>> GetAllAvailableRooms(Guid hotelId, CancellationToken cancellationToken);
}