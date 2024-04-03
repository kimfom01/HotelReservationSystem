using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class RoomStatusRepository: Repository<RoomStatus>, IRoomStatusRepository
{
    public RoomStatusRepository(Context context) : base(context)
    {
    }
}