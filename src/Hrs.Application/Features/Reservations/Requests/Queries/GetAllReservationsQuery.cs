using Hrs.Application.Dtos.Reservations;
using MediatR;

namespace Hrs.Application.Features.Reservations.Requests.Queries;

public class GetAllReservationsQuery : IRequest<List<GetReservationDetailsResponse>>
{
}