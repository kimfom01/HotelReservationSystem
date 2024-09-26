using Admin.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Admin.Application.Features.Admin.Rooms.Commands;

public record FreeRoomCommand : IRequest<bool>
{
    public FreeRoomRequest? RoomRequest { get; init; }
}