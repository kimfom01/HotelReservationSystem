using Admin.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Admin.Application.Features.Admin.Rooms.Commands;

public record CreateRoomCommand : IRequest<GetRoomResponse>
{
    public CreateRoomRequest? RoomRequest { get; init; }
}