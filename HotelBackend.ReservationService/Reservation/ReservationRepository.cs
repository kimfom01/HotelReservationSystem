using HotelBackend.ReservationService.Data;
using HotelBackend.ReservationService.Repositories.Implementations;

namespace HotelBackend.ReservationService.Reservation;

public class ReservationRepository : Repository<ReservationModel>, IReservationRepository
{
    public ReservationRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}