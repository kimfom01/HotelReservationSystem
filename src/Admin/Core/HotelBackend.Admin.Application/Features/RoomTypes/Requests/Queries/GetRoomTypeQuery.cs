using HotelBackend.Admin.Application.Dtos.RoomTypes;
using MediatR;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Requests.Queries;

public record GetRoomTypeQuery : IRequest<GetRoomTypeResponse>
{
    public Guid RoomTypeId { get; init; }
    public Guid HotelId { get; init; }
}