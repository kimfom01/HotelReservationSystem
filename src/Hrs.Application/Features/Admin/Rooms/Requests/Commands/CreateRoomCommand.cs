using Hrs.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Hrs.Application.Features.Admin.Rooms.Requests.Commands;

public record CreateRoomCommand : IRequest<GetRoomResponse>
{
    public CreateRoomRequest? RoomRequest { get; init; }
}