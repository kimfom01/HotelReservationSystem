using HotelBackend.Admin.Application.Dtos.Rooms;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;

public record FreeRoomCommand : IRequest<Unit>
{
    public FreeRoomRequest? RoomRequest { get; init; }
}