using Hrs.Application.Dtos.AdminApi.RoomApi;

namespace Hrs.Application.Contracts.ApiServices;

public interface IRoomApiService
{
    Task<ReserveRoomApiResponse> PlaceOnHold(ReserveRoomApiRequest roomApiRequest, CancellationToken cancellationToken);
    Task<bool> FreeUpRoom(FreeRoomApiRequest roomApiRequest, CancellationToken cancellationToken);
}