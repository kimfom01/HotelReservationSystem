using Hrs.Application.Dtos.Admin.RoomTypes;
using MediatR;

namespace Hrs.Application.Features.Admin.RoomTypes.Requests.Queries;

public record GetRoomTypeListQuery : IRequest<List<GetRoomTypeResponse>>
{
    public Guid HotelId { get; init; }
}