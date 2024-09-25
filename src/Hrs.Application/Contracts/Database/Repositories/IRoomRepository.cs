using Hrs.Common.Repositories;
using Hrs.Domain.Entities.Admin;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IRoomRepository : IRepository<Room>
{
    Task<List<Room>> GetAllAvailableRooms(Guid hotelId, CancellationToken cancellationToken);
    Task<Room?> GetRoomOfType(Guid hotelId, Guid roomTypeId);
    public Task<Room?> GetRoom(Guid roomId, Guid hotelId, CancellationToken cancellationToken);
}