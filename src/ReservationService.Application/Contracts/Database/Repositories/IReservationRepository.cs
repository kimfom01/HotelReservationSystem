using Hrs.Common.Repositories;
using ReservationService.Domain;

namespace ReservationService.Application.Contracts.Database.Repositories;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<Reservation?> GetReservationDetails(Guid reservationId,
        CancellationToken cancellationToken);
}