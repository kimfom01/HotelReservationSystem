using System.Linq.Expressions;
using HotelBackend.Old.Remove.DataAccess.Models;

namespace HotelBackend.Old.Remove.Api.Services;

public interface IHotelAmenityService
{
    Task<HotelAmenity?> GetHotelAmenity(int id);
    Task<HotelAmenity?> GetHotelAmenity(Expression<Func<HotelAmenity, bool>> expression);
    Task<IEnumerable<HotelAmenity>?> GetHotelAmenities();
    Task<IEnumerable<HotelAmenity>?> GetHotelAmenities(Expression<Func<HotelAmenity, bool>> expression);
    Task<int> DeleteHotelAmenity(int id);
    Task UpdateHotelAmenity(HotelAmenity hotelAmenity);
    Task<HotelAmenity> PostHotelAmenity(HotelAmenity hotelAmenity);
}
