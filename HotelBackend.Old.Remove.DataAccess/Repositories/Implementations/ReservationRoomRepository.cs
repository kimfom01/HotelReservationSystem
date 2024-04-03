using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class ReservationRoomRepository : Repository<ReservationRoom>, IReservationRoomRepository
{
    public ReservationRoomRepository(Context context) : base(context)
    {
    }
}