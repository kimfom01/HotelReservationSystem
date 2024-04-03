using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class HotelAmenityRepository : Repository<HotelAmenity>, IHotelAmenityRepository
{
    public HotelAmenityRepository(Context context) : base(context)
    {
    }
}
