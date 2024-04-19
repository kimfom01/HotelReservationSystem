using HotelBackend.Domain.Entities;
using HotelBackend.Domain.Enums;

namespace HotelBackend.Application.Contracts.Features;

public interface IReservationService
{
    Task<Reservation> MakeReservation(Reservation reservationDto);
    Task<Reservation?> GetReservation(Guid id);
    Task<IEnumerable<Reservation>?> GetReservations();
    Task SetPaymentStatus(Guid reservationId, PaymentStatusEnum statusEnum);
}