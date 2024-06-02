using HotelBackend.Admin.Application.Dtos.Rooms;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;

public class ReserveRoomRequest : IRequest<ReserveRoomResponse>
{
    public ReserveRoomRequestDto? RoomRequestDto { get; set; }
}