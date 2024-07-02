using Hrs.Application.Dtos.Reservations;
using MediatR;

namespace Hrs.Application.Features.Reservations.Queries;

public class GetReservationByIdQuery : IRequest<GetReservationDetailsResponse>
{
    public Guid ReservationId { get; init; }
}