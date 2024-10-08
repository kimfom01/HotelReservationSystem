using Admin.Application.Dtos.Admin.Rooms;
using MediatR;

namespace Admin.Application.Features.Admin.Rooms.Commands;

public record CreateManyRoomsCommand : IRequest<int>
{
    public CreateManyRoomsRequest? RoomsRequest { get; init; }
}