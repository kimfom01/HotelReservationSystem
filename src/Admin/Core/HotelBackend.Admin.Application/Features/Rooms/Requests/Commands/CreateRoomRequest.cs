using HotelBackend.Admin.Application.Dtos.Rooms;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;

public class CreateRoomRequest : IRequest<GetRoomDto>
{
    public CreateRoomDto? RoomDto { get; set; }
}