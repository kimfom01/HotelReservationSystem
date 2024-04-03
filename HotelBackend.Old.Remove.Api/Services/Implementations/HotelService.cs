using HotelBackend.Old.Remove.DataAccess.Models;
using HotelBackend.Old.Remove.DataAccess.Repositories;

namespace HotelBackend.Old.Remove.Api.Services.Implementations;

public class HotelService : IHotelService
{
    private readonly IUnitOfWork _unitOfWork;

    public HotelService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> DeleteHotel(int id)
    {
        await _unitOfWork.Hotels.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task<Hotel?> GetHotel(int id)
    {
        return await _unitOfWork.Hotels.GetEntity(id);
    }

    public async Task<IEnumerable<Hotel>?> GetHotels()
    {
        return await _unitOfWork.Hotels.GetEntities(ho => true);
    }

    public async Task<Hotel> PostHotel(Hotel hotel)
    {
        var added = await _unitOfWork.Hotels.Add(hotel);
        await _unitOfWork.SaveChanges();

        return added;
    }

    public async Task UpdateHotel(Hotel hotel)
    {
        await _unitOfWork.Hotels.Update(hotel);
        await _unitOfWork.SaveChanges();
    }
}
