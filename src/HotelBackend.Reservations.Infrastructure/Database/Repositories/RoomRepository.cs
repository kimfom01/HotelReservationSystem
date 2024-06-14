using HotelBackend.Reservations.Application.Contracts.Database.Repositories;
using HotelBackend.Reservations.Domain.Entities.Admin;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Reservations.Infrastructure.Database.Repositories;

public class RoomRepository : AdminBaseRepository<Room>, IRoomRepository
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