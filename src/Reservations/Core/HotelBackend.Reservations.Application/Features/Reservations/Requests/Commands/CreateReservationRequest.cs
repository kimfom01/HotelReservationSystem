using HotelBackend.Reservations.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;

public class CreateReservationRequest : IRequest<GetReservationDetailsDto>
{
    public CreateReservationDto? CreateReservationDto { get; set; }
}