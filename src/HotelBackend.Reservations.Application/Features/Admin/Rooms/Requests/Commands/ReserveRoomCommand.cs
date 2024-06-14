using HotelBackend.Reservations.Application.Dtos.Admin.Rooms;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.Rooms.Requests.Commands;

public record ReserveRoomCommand : IRequest<ReserveRoomResponse>
{
    public ReserveRoomRequest? RoomRequest { get; init; }
}