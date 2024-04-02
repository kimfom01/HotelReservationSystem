using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Repositories.Implementations;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}