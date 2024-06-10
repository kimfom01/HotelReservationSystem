using HotelBackend.Admin.Application.Dtos.RoomTypes;
using MediatR;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Requests.Queries;

public record GetRoomTypeListQuery : IRequest<List<GetRoomTypeResponse>>
{
    public Guid HotelId { get; init; }
}