using ReservationService.Application.Contracts.Database.Repositories;
using ReservationService.Domain;

namespace ReservationService.Infrastructure.Database.Repositories;

public class GuestProfileRepository : ReservationsBaseRepository<GuestProfile>, IGuestProfileRepository
{
    public GuestProfileRepository(ReservationDataContext reservationDataContext) : base(reservationDataContext)
    {
    }
}