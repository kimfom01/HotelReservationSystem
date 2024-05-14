using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.Contracts.Persistence;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<Reservation?> GetReservationDetails(Guid reservationId,
        CancellationToken cancellationToken);
}