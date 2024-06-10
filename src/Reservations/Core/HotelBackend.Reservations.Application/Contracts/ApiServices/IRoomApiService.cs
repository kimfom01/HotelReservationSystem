using HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;

namespace HotelBackend.Reservations.Application.Contracts.ApiServices;

public interface IRoomApiService
{
    Task<ReserveRoomApiResponse> PlaceOnHold(ReserveRoomApiRequest roomApiRequest);
    Task<bool> FreeUpRoom(FreeRoomApiRequest roomApiRequest);
}