using HotelBackend.Reservations.Application.Dtos.Admin.Rooms;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.Rooms.Requests.Commands;

public record FreeRoomCommand : IRequest<Unit>
{
    public FreeRoomRequest? RoomRequest { get; init; }
}