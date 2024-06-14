using Hrs.Application.Dtos.Admin.RoomTypes;
using MediatR;

namespace Hrs.Application.Features.Admin.RoomTypes.Requests.Command;

public record CreateRoomTypeCommand : IRequest<GetRoomTypeResponse>
{
    public CreateRoomTypeRequest? RoomTypeRequest { get; init; }
}