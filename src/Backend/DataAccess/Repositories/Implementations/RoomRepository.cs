using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(Context context) : base(context)
    {
    }
}
