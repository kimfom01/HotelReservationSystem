using ReservationService.Application.Contracts.Services;
using ReservationService.Application.Dtos.Rooms;

namespace ReservationService.Infrastructure.Services;

public class RoomService : IRoomService
{
    public Task<ReserveRoomResponse> PlaceOnHold(ReserveRoomRequest roomRequest,
        CancellationToken cancellationToken = default)
    {
        // var response = await _mediator.Send(new ReserveRoomCommand
        // {
        //     RoomRequest = roomRequest
        // }, cancellationToken);
        //
        // return response with { Success = true };


        // TODO: Replace with network request
        throw new NotImplementedException();
    }

    public Task<bool> FreeUpRoom(FreeRoomRequest roomRequest, CancellationToken cancellationToken = default)
    {
        // var response = await _mediator.Send(new FreeRoomCommand
        // {
        //     RoomRequest = roomRequest
        // }, cancellationToken);
        //
        // return response;


        // TODO: Replace with network request
        throw new NotImplementedException();
    }
}