using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public RoomRepository(Context context) : base(context)
    {
    }
}
