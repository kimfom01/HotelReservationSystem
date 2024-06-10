using HotelBackend.Reservations.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Reservations.Requests.Queries;

public class GetReservationByIdQuery : IRequest<GetReservationDetailsResponse>
{
    public Guid ReservationId { get; set; }
}