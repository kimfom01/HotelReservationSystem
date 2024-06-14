using Hrs.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Hrs.Application.Features.Admin.Rooms.Requests.Commands;

public record ReserveRoomCommand : IRequest<ReserveRoomResponse>
{
    public ReserveRoomRequest? RoomRequest { get; init; }
}