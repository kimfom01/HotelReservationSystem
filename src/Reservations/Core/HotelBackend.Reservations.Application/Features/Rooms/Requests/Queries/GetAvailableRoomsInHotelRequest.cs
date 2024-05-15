using HotelBackend.Reservations.Application.Dtos;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Rooms.Requests.Queries;

public class GetAvailableRoomsInHotelRequest : IRequest<List<RoomDto>>
{
    public Guid HotelId { get; set; }
}