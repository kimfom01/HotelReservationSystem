using HotelBackend.Reservations.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Reservations.Requests.Queries;

public class GetAllReservationsRequest : IRequest<List<GetReservationDetailsDto>>
{
}