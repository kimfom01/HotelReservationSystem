using MediatR;
using ReservationService.Application.Dtos.Reservations;

namespace ReservationService.Application.Features.Reservations.Queries;

public class GetReservationByIdQuery : IRequest<GetReservationDetailsResponse>
{
    public Guid ReservationId { get; init; }
}