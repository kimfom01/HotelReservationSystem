using Hrs.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Hrs.Application.Features.Admin.Rooms.Requests.Queries;

public record GetAvailableRoomsQuery : IRequest<List<GetRoomResponse>>
{
    public Guid HotelId { get; init; }
}