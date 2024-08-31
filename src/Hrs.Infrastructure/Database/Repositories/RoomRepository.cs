using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;

namespace Hrs.Infrastructure.Database.Repositories;

public class RoomRepository : AdminBaseRepository<Room>, IRoomRepository
{
    public RoomRepository(AdminDataContext context) : base(context)
    {
    }

    public async Task<List<Room>> GetAllAvailableRooms(Guid hotelId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(room => room.HotelId == hotelId
                           && room.Availability)
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Room?> GetRoomOfType(Guid hotelId, Guid roomTypeId)
    {
        return await DbSet
            .FirstOrDefaultAsync(room =>
                room.HotelId == hotelId
                && room.RoomTypeId == roomTypeId
                && room.Availability);
    }

    public async Task<Room?> GetRoom(Guid roomId, Guid hotelId, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(rom
                => rom.Id == roomId
                   && rom.HotelId == hotelId,
            cancellationToken);
    }
}