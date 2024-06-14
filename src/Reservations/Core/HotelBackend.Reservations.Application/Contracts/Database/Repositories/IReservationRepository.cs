using HotelBackend.Reservations.Domain.Entities.Reservation;

namespace HotelBackend.Reservations.Application.Contracts.Database.Repositories;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<Reservation?> GetReservationDetails(Guid reservationId,
        CancellationToken cancellationToken);
}