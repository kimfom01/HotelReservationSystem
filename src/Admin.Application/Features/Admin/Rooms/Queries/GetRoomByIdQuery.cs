using Admin.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Admin.Application.Features.Admin.Rooms.Queries;

public record GetRoomByIdQuery : IRequest<GetRoomResponse>
{
    public Guid HotelId { get; init; }
    public Guid RoomId { get; init; }
}