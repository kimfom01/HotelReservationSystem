using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Repositories.Implementations;

public class PricingRepository : Repository<Price>, IPricingRepository
{
    public PricingRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}