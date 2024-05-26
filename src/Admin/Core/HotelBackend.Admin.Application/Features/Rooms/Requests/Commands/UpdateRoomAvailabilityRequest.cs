using HotelBackend.Admin.Application.Dtos.Rooms;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;

public class UpdateRoomAvailabilityRequest : IRequest<Unit>
{
    public UpdateRoomAvailabilityDto? RoomDto { get; set; }
}