using HotelBackend.Reservations.Application.Dtos.Reservations;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;

public class CreateReservationCommand : IRequest<GetReservationDetailsResponse>
{
    public CreateReservationRequest? CreateReservationDto { get; set; }
}