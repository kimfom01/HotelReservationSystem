using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class HotelRepository : Repository<Hotel>, IHotelRepository
{
    public HotelRepository(Context context) : base(context)
    {
    }
}
