using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Services;

public interface IHotelService
{
    Task<Hotel?> GetHotel(int id);
    Task<IEnumerable<Hotel>?> GetHotels();
    Task<int> DeleteHotel(int id);
    Task UpdateHotel(Hotel hotel);
    Task<Hotel> PostHotel(Hotel hotel);
}
