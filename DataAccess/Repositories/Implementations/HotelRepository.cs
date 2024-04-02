using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(Context context) : base(context)
    {
    }
}
