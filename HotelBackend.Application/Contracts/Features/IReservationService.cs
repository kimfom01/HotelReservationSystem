using HotelBackend.Domain.Entities;
using HotelBackend.Domain.Enums;

namespace HotelBackend.Application.Contracts.Features;

public interface IReservationService
{
    Task<Reservation> MakeReservation(Reservation reservationDto, CancellationToken cancellationToken);
    Task<Reservation?> GetReservation(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Reservation>?> GetReservations(CancellationToken cancellationToken);
    Task SetPaymentStatus(Guid reservationId, PaymentStatusEnum statusEnum, CancellationToken cancellationToken);
}