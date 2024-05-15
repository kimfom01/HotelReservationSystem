using HotelBackend.Reservations.Application.Contracts.Persistence;
using HotelBackend.Reservations.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Reservations.Persistence.Data.Repositories.Implementations;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public async Task<List<Room>> GetAllAvailableRooms(Guid hotelId, CancellationToken cancellationToken)
    {
        return await DbSet
            .Where(room => room.HotelId == hotelId
                           && room.Availability)
            .AsNoTracking().ToListAsync(cancellationToken);
    }
}