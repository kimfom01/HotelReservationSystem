using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;

namespace Hrs.Infrastructure.Database.Repositories;

public class RoomTypeRepository : AdminBaseRepository<RoomType>, IRoomTypeRepository
{
    public RoomTypeRepository(AdminDataContext adminDataContext) : base(adminDataContext)
    {
    }

    public async Task<RoomType?> GetRoomType(Guid roomTypeId, Guid hotelId, CancellationToken cancellationToken)
    {
        var entity = await DbSet.FirstOrDefaultAsync(rom
                => rom.Id == roomTypeId
                   && rom.HotelId == hotelId,
            cancellationToken);

        return entity;
    }

    public async Task<IReadOnlyList<RoomType>> GetRoomTypes(Guid hotelId, CancellationToken cancellationToken)
    {
        return await DbSet.Where(rt => rt.HotelId == hotelId).ToListAsync(cancellationToken);
    }
}