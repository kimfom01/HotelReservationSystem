using HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;

namespace HotelBackend.Reservations.Application.Contracts.ApiServices;

public interface IRoomApiService
{
    Task<ReserveRoomApiResponse> PlaceOnHold(ReserveRoomApiRequest roomApiRequest, CancellationToken cancellationToken);
    Task<bool> FreeUpRoom(FreeRoomApiRequest roomApiRequest, CancellationToken cancellationToken);
}