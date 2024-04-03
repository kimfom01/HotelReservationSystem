using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class GuestRepository : Repository<Guest>, IGuestRepository
{
    public GuestRepository(Context context) : base(context)
    {
    }
}
