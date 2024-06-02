using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Admin.Infrastructure.Database.Repositories;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(AdminDataContext adminDataContext) : base(adminDataContext)
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
}