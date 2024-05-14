using HotelBackend.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Application.Features.Reservations.Requests.Commands;

public class CreateReservationRequest : IRequest<GetReservationDetailsDto>
{
    public CreateReservationDto? CreateReservationDto { get; set; }
}