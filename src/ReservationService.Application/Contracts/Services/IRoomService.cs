using ReservationService.Application.Dtos.Rooms;

namespace ReservationService.Application.Contracts.Services;

public interface IRoomService
{
    Task<ReserveRoomResponse> PlaceOnHold(ReserveRoomRequest roomRequest, CancellationToken cancellationToken = default);
    Task<bool> FreeUpRoom(FreeRoomRequest roomRequest, CancellationToken cancellationToken = default);
}