using Admin.Application.Dtos.Admin.RoomTypes;
using MediatR;

namespace Admin.Application.Features.Admin.RoomTypes.Queries;

public record GetRoomTypeQuery : IRequest<GetRoomTypeResponse>
{
    public Guid RoomTypeId { get; init; }
    public Guid HotelId { get; init; }
}