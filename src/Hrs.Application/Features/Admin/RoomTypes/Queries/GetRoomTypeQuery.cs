using Hrs.Application.Dtos.Admin.RoomTypes;
using MediatR;

namespace Hrs.Application.Features.Admin.RoomTypes.Queries;

public record GetRoomTypeQuery : IRequest<GetRoomTypeResponse>
{
    public Guid RoomTypeId { get; init; }
    public Guid HotelId { get; init; }
}