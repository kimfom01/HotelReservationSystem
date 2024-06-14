using Hrs.Domain.Entities.Reservation;

namespace Hrs.Application.Contracts.Database.Repositories;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<Reservation?> GetReservationDetails(Guid reservationId,
        CancellationToken cancellationToken);
}