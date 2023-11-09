using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class PricingRepository : Repository<Pricing>, IPricingRepository
{
    public PricingRepository(Context context) : base(context)
    {
    }
}