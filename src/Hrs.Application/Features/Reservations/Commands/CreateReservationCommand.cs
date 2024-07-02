using Hrs.Application.Dtos.Reservations;
using MediatR;

namespace Hrs.Application.Features.Reservations.Commands;

public class CreateReservationCommand : IRequest<GetReservationDetailsResponse>
{
    public CreateReservationRequest? CreateReservationDto { get; init; }
}