using HotelBackend.Reservations.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;

public class UpdateReservationStatusCommand : IRequest<Unit>
{
    public UpdateReservationPaymentStatusRequest? UpdateReservationPaymentStatusDto { get; set; }
}