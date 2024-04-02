using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Repositories.Implementations;

namespace HotelBackend.ReservationService.Hotel;

public class HotelRepository : Repository<HotelModel>, IHotelRepository
{
    public HotelRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}
