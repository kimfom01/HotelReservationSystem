using Hrs.Common.Repositories;
using Admin.Domain.Entities.Admin;

namespace Admin.Application.Contracts.Database.Repositories;

public interface IRoomTypeRepository : IRepository<RoomType>
{
    public Task<RoomType?> GetRoomType(Guid roomTypeId, Guid hotelId, CancellationToken cancellationToken);
    public Task<IReadOnlyList<RoomType>> GetRoomTypes(Guid hotelId, CancellationToken cancellationToken);
}