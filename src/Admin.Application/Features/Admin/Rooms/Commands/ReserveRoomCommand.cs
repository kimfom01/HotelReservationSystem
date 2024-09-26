using Admin.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Admin.Application.Features.Admin.Rooms.Commands;

public record ReserveRoomCommand : IRequest<ReserveRoomResponse>
{
    public ReserveRoomRequest? RoomRequest { get; init; }
}