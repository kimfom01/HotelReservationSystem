using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.ReservationService.Room;

public class RoomRepository : Repository<RoomModel>, IRoomRepository
{
    public RoomRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public async Task<List<RoomModel>> GetAllAvailableRooms(Guid hotelId)
    {
        return await DbSet
            .Where(room => room.HotelId == hotelId
                          && room.Availability == true)
            .AsNoTracking().ToListAsync();
    }
}