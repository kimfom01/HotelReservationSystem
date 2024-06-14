using Hrs.Application.Dtos.Reservations;
using MediatR;

namespace Hrs.Application.Features.Reservations.Requests.Commands;

public class CreateReservationCommand : IRequest<GetReservationDetailsResponse>
{
    public CreateReservationRequest? CreateReservationDto { get; set; }
}