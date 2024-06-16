using Hrs.Application.Dtos.Admin.Rooms;

namespace Hrs.Application.Contracts.Services;

public interface IRoomService
{
    Task<ReserveRoomResponse> PlaceOnHold(ReserveRoomRequest roomRequest, CancellationToken cancellationToken);
    Task<bool> FreeUpRoom(FreeRoomRequest roomRequest, CancellationToken cancellationToken);
}