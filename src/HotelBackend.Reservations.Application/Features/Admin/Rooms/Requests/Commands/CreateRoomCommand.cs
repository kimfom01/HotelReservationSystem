using HotelBackend.Reservations.Application.Dtos.Admin.Rooms;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.Rooms.Requests.Commands;

public record CreateRoomCommand : IRequest<GetRoomResponse>
{
    public CreateRoomRequest? RoomRequest { get; init; }
}