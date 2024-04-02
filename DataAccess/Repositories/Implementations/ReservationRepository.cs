using DataAccess.Data;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementations;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    private readonly Context _context;

    public ReservationRepository(Context context) : base(context)
    {
        _context = context;
    }
}