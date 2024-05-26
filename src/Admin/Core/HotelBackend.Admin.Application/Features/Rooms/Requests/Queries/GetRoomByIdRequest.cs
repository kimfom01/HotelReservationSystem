using HotelBackend.Admin.Application.Dtos.Rooms;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;

public class GetRoomByIdRequest : IRequest<GetRoomDto>
{
    public Guid HotelId { get; set; }
    public Guid RoomId { get; set; }
}