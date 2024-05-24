using HotelBackend.Admin.Application.Dtos.RoomTypes;
using MediatR;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Requests.Command;

public class CreateRoomTypeRequest : IRequest<GetRoomTypeDto>
{
    public CreateRoomTypeDto? RoomTypeDto { get; set; }
}