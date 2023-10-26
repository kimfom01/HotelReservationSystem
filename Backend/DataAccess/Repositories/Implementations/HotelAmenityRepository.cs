using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class HotelAmenityRepository : Repository<HotelAmenity>, IHotelAmenityRepository
{
    public HotelAmenityRepository(Context context) : base(context)
    {
    }
}
