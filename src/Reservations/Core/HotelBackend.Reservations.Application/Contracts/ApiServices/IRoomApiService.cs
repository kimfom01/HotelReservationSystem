using HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;

namespace HotelBackend.Reservations.Application.Contracts.ApiServices;

public interface IRoomApiService
{
    Task<bool> SetRoomAvailability(UpdateRoomAvailabilityDto roomAvailabilityDto);
}