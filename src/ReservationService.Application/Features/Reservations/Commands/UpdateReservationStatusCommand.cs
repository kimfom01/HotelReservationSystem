using MediatR;
using ReservationService.Application.Dtos.Reservations;

namespace ReservationService.Application.Features.Reservations.Commands;

public class UpdateReservationStatusCommand : IRequest<Unit>
{
    public UpdateReservationPaymentStatusRequest? UpdateReservationPaymentStatusDto { get; init; }
}