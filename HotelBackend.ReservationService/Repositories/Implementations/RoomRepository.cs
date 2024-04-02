using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.ReservationService.Repositories.Implementations;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public async Task<List<Room>> GetAllAvailableRooms(Guid hotelId)
    {
        return await DbSet
            .Where(room => room.HotelId == hotelId
                          && room.Availability == true)
            .AsNoTracking().ToListAsync();
    }
}