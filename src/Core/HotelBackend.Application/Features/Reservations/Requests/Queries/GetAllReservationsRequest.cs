using HotelBackend.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Application.Features.Reservations.Requests.Queries;

public class GetAllReservationsRequest : IRequest<List<GetReservationDetailsDto>>
{
}