using HotelBackend.Admin.Application.Dtos.Rooms;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;

public record GetRoomByIdQuery : IRequest<GetRoomResponse>
{
    public Guid HotelId { get; init; }
    public Guid RoomId { get; init; }
}