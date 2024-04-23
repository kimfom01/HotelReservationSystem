using HotelBackend.Application.Contracts.Features;
using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Application.Exceptions;
using HotelBackend.Domain.Entities;
using HotelBackend.Domain.Enums;

namespace HotelBackend.Application.Features;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservationService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Reservation?> GetReservation(Guid id, CancellationToken cancellationToken)
    {
        return _unitOfWork.Reservations.GetEntity(id, cancellationToken);
    }

    public async Task<IEnumerable<Reservation>?> GetReservations(CancellationToken cancellationToken)
    {
        return await _unitOfWork.Reservations.GetEntities(res => true, cancellationToken);
    }

    public async Task SetPaymentStatus(Guid reservationId, PaymentStatus status,
        CancellationToken cancellationToken)
    {
        var reservation = await _unitOfWork.Reservations.GetEntity(reservationId, cancellationToken);

        if (reservation is null)
        {
            throw new NotFoundException($"Reservation with id={reservationId} does not exist");
        }

        reservation.PaymentStatus = status;
        await _unitOfWork.SaveChanges(cancellationToken);
    }
}