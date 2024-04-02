using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Repositories.Implementations;

public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}
