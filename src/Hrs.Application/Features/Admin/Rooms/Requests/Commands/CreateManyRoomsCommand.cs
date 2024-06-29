using Hrs.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Hrs.Application.Features.Admin.Rooms.Requests.Commands;

public record CreateManyRoomsCommand : IRequest<int>
{
    public CreateManyRoomsRequest RoomsRequest { get; set; }
}