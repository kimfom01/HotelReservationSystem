using HotelBackend.Reservations.Application.Dtos.Admin.RoomTypes;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.RoomTypes.Requests.Command;

public record CreateRoomTypeCommand : IRequest<GetRoomTypeResponse>
{
    public CreateRoomTypeRequest? RoomTypeRequest { get; init; }
}