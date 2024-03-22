using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Repositories.Implementations;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}
