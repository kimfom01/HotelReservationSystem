using HotelBackend.ReservationService.Dtos;

namespace HotelBackend.ReservationService.Services;

public interface IHotelService
{
    Task<IEnumerable<HotelDto>?> GetHotels();
}
