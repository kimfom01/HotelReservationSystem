using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class ReservationRoomRepository : Repository<ReservationRoom>, IReservationRoomRepository
{
    public ReservationRoomRepository(Context context) : base(context)
    {
    }
}