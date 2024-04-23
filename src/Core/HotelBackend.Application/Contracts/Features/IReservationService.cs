using HotelBackend.Domain.Entities;
using HotelBackend.Domain.Enums;

namespace HotelBackend.Application.Contracts.Features;

public interface IReservationService
{
    Task<Reservation?> GetReservation(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Reservation>?> GetReservations(CancellationToken cancellationToken);
    Task SetPaymentStatus(Guid reservationId, PaymentStatus status, CancellationToken cancellationToken);
}