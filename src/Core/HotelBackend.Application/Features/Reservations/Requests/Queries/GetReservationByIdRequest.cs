using HotelBackend.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Application.Features.Reservations.Requests.Queries;

public class GetReservationByIdRequest : IRequest<GetReservationDto>
{
    public Guid ReservationId { get; set; }
}