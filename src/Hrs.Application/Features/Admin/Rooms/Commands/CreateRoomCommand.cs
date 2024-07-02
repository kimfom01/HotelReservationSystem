using Hrs.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Hrs.Application.Features.Admin.Rooms.Commands;

public record CreateRoomCommand : IRequest<GetRoomResponse>
{
    public CreateRoomRequest? RoomRequest { get; init; }
}