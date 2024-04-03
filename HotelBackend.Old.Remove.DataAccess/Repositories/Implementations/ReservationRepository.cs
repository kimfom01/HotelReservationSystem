using HotelBackend.Old.Remove.DataAccess.Data;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.DataAccess.Repositories.Implementations;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    private readonly Context _context;

    public ReservationRepository(Context context) : base(context)
    {
        _context = context;
    }
}