using Hrs.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Hrs.Application.Features.Admin.Rooms.Requests.Commands;

public record FreeRoomCommand : IRequest<bool>
{
    public FreeRoomRequest? RoomRequest { get; init; }
}