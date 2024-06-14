using Hrs.Application.Dtos.Reservations;
using MediatR;

namespace Hrs.Application.Features.Reservations.Requests.Queries;

public class GetReservationByIdQuery : IRequest<GetReservationDetailsResponse>
{
    public Guid ReservationId { get; set; }
}