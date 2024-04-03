using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class PricingRepository : Repository<Pricing>, IPricingRepository
{
    public PricingRepository(Context context) : base(context)
    {
    }
}