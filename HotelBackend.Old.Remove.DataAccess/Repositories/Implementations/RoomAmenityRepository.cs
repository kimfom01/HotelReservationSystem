using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class RoomAmenityRepository:Repository<RoomAmenity>, IRoomAmenityRepository
{
    public RoomAmenityRepository(Context context) : base(context)
    {
    }
}