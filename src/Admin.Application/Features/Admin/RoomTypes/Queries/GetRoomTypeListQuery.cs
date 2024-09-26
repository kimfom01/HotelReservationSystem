using Admin.Application.Dtos.Admin.RoomTypes;
using MediatR;

namespace Admin.Application.Features.Admin.RoomTypes.Queries;

public record GetRoomTypeListQuery : IRequest<IReadOnlyList<GetRoomTypeResponse>>
{
    public Guid HotelId { get; init; }
}