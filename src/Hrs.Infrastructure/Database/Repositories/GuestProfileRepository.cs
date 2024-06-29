using Hrs.Application.Contracts.Database.Repositories;
using Hrs.Domain.Entities.Reservation;

namespace Hrs.Infrastructure.Database.Repositories;

public class GuestProfileRepository : ReservationsBaseRepository<GuestProfile>, IGuestProfileRepository
{
    public GuestProfileRepository(ReservationDataContext reservationDataContext) : base(reservationDataContext)
    {
    }
}