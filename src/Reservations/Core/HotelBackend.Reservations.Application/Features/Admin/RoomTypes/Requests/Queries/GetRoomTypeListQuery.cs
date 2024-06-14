using HotelBackend.Reservations.Application.Dtos.Admin.RoomTypes;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.RoomTypes.Requests.Queries;

public record GetRoomTypeListQuery : IRequest<List<GetRoomTypeResponse>>
{
    public Guid HotelId { get; init; }
}