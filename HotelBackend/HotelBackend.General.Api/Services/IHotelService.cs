using System.Linq.Expressions;
using DataAccess.Models;

namespace HotelBackend.General.Api.Services;

public interface IHotelService
{
    Task<Hotel?> GetHotel(int id);
    Task<IEnumerable<Hotel>?> GetHotels();
    Task<int> DeleteHotel(int id);
    Task UpdateHotel(Hotel hotel);
    Task<Hotel> PostHotel(Hotel hotel);
}
