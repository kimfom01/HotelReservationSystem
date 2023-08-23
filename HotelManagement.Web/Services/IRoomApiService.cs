using HotelManagement.Web.Models.Dtos;

namespace HotelManagement.Web.Services;
public interface IRoomApiService : IGenericApiService<Room>
{
    Task<Room?> FetchRoomByHotelId(int hotelId, int capacity);
}