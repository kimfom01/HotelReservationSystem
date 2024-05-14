using HotelBackend.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Application.Features.Reservations.Requests.Commands;

public class UpdateReservationStatusRequest : IRequest<Unit>
{
    public UpdateReservationPaymentStatusDto? UpdateReservationPaymentStatusDto { get; set; }
}