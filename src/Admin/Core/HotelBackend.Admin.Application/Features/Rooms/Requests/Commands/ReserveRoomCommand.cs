using HotelBackend.Admin.Application.Dtos.Rooms;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;

public record ReserveRoomCommand : IRequest<ReserveRoomResponse>
{
    public ReserveRoomRequest? RoomRequest { get; init; }
}