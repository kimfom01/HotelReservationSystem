using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class RoomStatusRepository: Repository<RoomStatus>, IRoomStatusRepository
{
    public RoomStatusRepository(Context context) : base(context)
    {
    }
}