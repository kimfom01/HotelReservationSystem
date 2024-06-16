using Hrs.Application.Contracts.Services;
using Hrs.Application.Dtos.Admin.Rooms;
using Hrs.Application.Features.Admin.Rooms.Requests.Commands;
using MediatR;

namespace Hrs.Infrastructure.Services;

public class RoomService : IRoomService
{
    private readonly IMediator _mediator;

    public RoomService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ReserveRoomResponse> PlaceOnHold(ReserveRoomRequest roomRequest,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new ReserveRoomCommand
        {
            RoomRequest = roomRequest
        }, cancellationToken);

        return response with { Success = true };
    }

    public async Task<bool> FreeUpRoom(FreeRoomRequest roomRequest, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new FreeRoomCommand
        {
            RoomRequest = roomRequest
        }, cancellationToken);

        return response;
    }
}