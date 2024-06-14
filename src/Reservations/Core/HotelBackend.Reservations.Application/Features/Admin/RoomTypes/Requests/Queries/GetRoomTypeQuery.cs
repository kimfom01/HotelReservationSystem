using HotelBackend.Reservations.Application.Dtos.RoomTypes;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.RoomTypes.Requests.Queries;

public record GetRoomTypeQuery : IRequest<GetRoomTypeResponse>
{
    public Guid RoomTypeId { get; init; }
    public Guid HotelId { get; init; }
}