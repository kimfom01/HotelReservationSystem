using HotelBackend.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Application.Features.Reservations.Requests.Queries;

public class GetReservationByIdRequest : IRequest<GetReservationDetailsDto>
{
    public Guid ReservationId { get; set; }
}