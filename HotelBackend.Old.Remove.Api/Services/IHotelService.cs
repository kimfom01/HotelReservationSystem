using System.Linq.Expressions;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.Api.Services;

public interface IHotelService
{
    Task<Hotel?> GetHotel(int id);
    Task<IEnumerable<Hotel>?> GetHotels();
    Task<int> DeleteHotel(int id);
    Task UpdateHotel(Hotel hotel);
    Task<Hotel> PostHotel(Hotel hotel);
}
