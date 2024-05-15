using HotelBackend.Reservations.Domain.Entities;

namespace HotelBackend.Reservations.Application.Contracts.Persistence;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<Reservation?> GetReservationDetails(Guid reservationId,
        CancellationToken cancellationToken);
}