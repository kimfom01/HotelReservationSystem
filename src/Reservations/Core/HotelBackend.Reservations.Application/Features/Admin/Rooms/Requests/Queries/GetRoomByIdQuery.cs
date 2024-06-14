using HotelBackend.Reservations.Application.Dtos.Rooms;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.Rooms.Requests.Queries;

public record GetRoomByIdQuery : IRequest<GetRoomResponse>
{
    public Guid HotelId { get; init; }
    public Guid RoomId { get; init; }
}