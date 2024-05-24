using HotelBackend.Admin.Application.Dtos.Rooms;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;

public class GetAvailableRoomsInHotelRequest : IRequest<List<GetRoomDto>>
{
    public Guid HotelId { get; set; }
}