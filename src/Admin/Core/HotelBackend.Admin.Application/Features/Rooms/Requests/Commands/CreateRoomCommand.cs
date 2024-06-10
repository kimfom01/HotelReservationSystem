using HotelBackend.Admin.Application.Dtos.Rooms;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;

public record CreateRoomCommand : IRequest<GetRoomResponse>
{
    public CreateRoomRequest? RoomRequest { get; init; }
}