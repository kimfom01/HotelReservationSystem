using MediatR;
using ReservationService.Application.Dtos.Reservations;

namespace ReservationService.Application.Features.Reservations.Commands;

public class CreateReservationCommand : IRequest<GetReservationDetailsResponse>
{
    public CreateReservationRequest? CreateReservationDto { get; init; }
}