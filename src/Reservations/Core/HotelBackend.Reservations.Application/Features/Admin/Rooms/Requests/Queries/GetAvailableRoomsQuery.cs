using HotelBackend.Reservations.Application.Dtos.Admin.Rooms;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.Rooms.Requests.Queries;

public record GetAvailableRoomsQuery : IRequest<List<GetRoomResponse>>
{
    public Guid HotelId { get; init; }
}