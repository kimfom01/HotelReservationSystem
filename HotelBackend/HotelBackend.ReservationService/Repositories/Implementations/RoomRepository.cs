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
            .Where(hot => hot.Id == hotelId
                          && hot.Availability == true)
            .AsNoTracking()
            .ToListAsync();
    }
}