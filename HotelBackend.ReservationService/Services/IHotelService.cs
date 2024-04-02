using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Services;

public interface IHotelService
{
    Task<Hotel?> GetHotel(Guid id);
    Task<IEnumerable<Hotel>?> GetHotels();
    Task<int> DeleteHotel(Guid id);
    Task UpdateHotel(Hotel? hotel);
    Task<Hotel?> PostHotel(Hotel? hotel);
}
