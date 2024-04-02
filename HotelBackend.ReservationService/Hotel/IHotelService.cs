namespace HotelBackend.ReservationService.Hotel;

public interface IHotelService
{
    Task<IEnumerable<HotelDto>?> GetHotels();
}
