using HotelBackend.Admin.Application.Dtos.RoomTypes;
using MediatR;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Requests.Command;

public record CreateRoomTypeCommand : IRequest<GetRoomTypeResponse>
{
    public CreateRoomTypeRequest? RoomTypeRequest { get; init; }
}