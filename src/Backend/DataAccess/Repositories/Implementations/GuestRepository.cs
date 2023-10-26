using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class GuestRepository : Repository<Guest>, IGuestRepository
{
    public GuestRepository(Context context) : base(context)
    {
    }
}
