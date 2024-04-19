using HotelBackend.Application.Dtos;
using MediatR;

namespace HotelBackend.Application.Features.Rooms.Requests.Queries;

public class GetAvailableRoomsInHotelRequest : IRequest<List<RoomDto>>
{
    public Guid HotelId { get; set; }
}