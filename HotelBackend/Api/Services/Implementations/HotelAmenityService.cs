using System.Linq.Expressions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Api.Services.Implementations;

public class HotelAmenityService : IHotelAmenityService
{
    private readonly IUnitOfWork _unitOfWork;

    public HotelAmenityService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> DeleteHotelAmenity(int id)
    {
        await _unitOfWork.HotelAmenities.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<HotelAmenity>?> GetHotelAmenities()
    {
        return await _unitOfWork.HotelAmenities.GetEntities(hotam => true);
    }

    public async Task<IEnumerable<HotelAmenity>?> GetHotelAmenities(Expression<Func<HotelAmenity, bool>> expression)
    {
        return await _unitOfWork.HotelAmenities.GetEntities(expression);
    }

    public async Task<HotelAmenity?> GetHotelAmenity(int id)
    {
        return await _unitOfWork.HotelAmenities.GetEntity(id);
    }

    public async Task<HotelAmenity?> GetHotelAmenity(Expression<Func<HotelAmenity, bool>> expression)
    {
        return await _unitOfWork.HotelAmenities.GetEntity(expression);
    }

    public async Task<HotelAmenity> PostHotelAmenity(HotelAmenity hotelAmenity)
    {
        var added = await _unitOfWork.HotelAmenities.Add(hotelAmenity);
        await _unitOfWork.SaveChanges();

        return added;
    }

    public async Task UpdateHotelAmenity(HotelAmenity hotelAmenity)
    {
        await _unitOfWork.HotelAmenities.Update(hotelAmenity);
        await _unitOfWork.SaveChanges();
    }
}
