using Hrs.Application.Dtos.Reservations;
using MediatR;

namespace Hrs.Application.Features.Reservations.Requests.Commands;

public class UpdateReservationStatusCommand : IRequest<Unit>
{
    public UpdateReservationPaymentStatusRequest? UpdateReservationPaymentStatusDto { get; set; }
}