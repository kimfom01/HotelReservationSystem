using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Repositories.Implementations;

namespace HotelBackend.ReservationService.Room.Price;

public class PriceRepository : Repository<PriceModel>, IPriceRepository
{
    public PriceRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}