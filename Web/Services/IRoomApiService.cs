using Web.Models.Dtos;

namespace Web.Services;
public interface IRoomApiService : IGenericApiService<Room>
{
    Task<Room?> FetchRoomByHotelId(int hotelId, int capacity);
}