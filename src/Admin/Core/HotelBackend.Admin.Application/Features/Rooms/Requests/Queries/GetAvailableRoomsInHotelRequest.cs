using HotelBackend.Admin.Application.Dtos;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;

public class GetAvailableRoomsInHotelRequest : IRequest<List<RoomDto>>
{
    public Guid HotelId { get; set; }
}