using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class RoomAmenityRepository:Repository<RoomAmenity>, IRoomAmenityRepository
{
    public RoomAmenityRepository(Context context) : base(context)
    {
    }
}