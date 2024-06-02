using HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;

namespace HotelBackend.Reservations.Application.Contracts.ApiServices;

public interface IRoomApiService
{
    Task<ReserveRoomResponse> PlaceOnHold(ReserveRoomRequestDto roomRequestDto);
    Task<bool> FreeUpRoom(FreeRoomRequestDto roomRequestDto);
}