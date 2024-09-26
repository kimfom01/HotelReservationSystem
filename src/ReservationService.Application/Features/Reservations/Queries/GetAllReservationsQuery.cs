using MediatR;
using ReservationService.Application.Dtos.Reservations;

namespace ReservationService.Application.Features.Reservations.Queries;

public class GetAllReservationsQuery : IRequest<List<GetReservationDetailsResponse>>
{
}