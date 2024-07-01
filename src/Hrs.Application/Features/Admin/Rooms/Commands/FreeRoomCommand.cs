using Hrs.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Hrs.Application.Features.Admin.Rooms.Commands;

public record FreeRoomCommand : IRequest<bool>
{
    public FreeRoomRequest? RoomRequest { get; init; }
}