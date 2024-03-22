using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Repositories.Implementations;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    private readonly DatabaseContext _databaseContext;

    public ReservationRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
        _databaseContext = databaseContext;
    }
}