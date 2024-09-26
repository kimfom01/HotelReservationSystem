using Admin.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Admin.Application.Features.Admin.Rooms.Queries;

public record GetAvailableRoomsQuery : IRequest<List<GetRoomResponse>>
{
    public Guid HotelId { get; init; }
}