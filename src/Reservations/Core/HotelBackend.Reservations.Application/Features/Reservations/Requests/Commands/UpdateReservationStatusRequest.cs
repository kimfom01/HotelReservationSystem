using HotelBackend.Reservations.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;

public class UpdateReservationStatusRequest : IRequest<Unit>
{
    public UpdateReservationPaymentStatusDto? UpdateReservationPaymentStatusDto { get; set; }
}