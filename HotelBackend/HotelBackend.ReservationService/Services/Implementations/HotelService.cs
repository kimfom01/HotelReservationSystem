using HotelBackend.ReservationService.Exceptions;
using HotelBackend.ReservationService.Models;
using HotelBackend.ReservationService.Repositories;

namespace HotelBackend.ReservationService.Services.Implementations;

public class HotelService : IHotelService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HotelService> _logger;

    public HotelService(
        IUnitOfWork unitOfWork, 
        ILogger<HotelService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<int> DeleteHotel(Guid id)
    {
        try
        {
            await _unitOfWork.Hotels.Delete(id);
            return await _unitOfWork.SaveChanges();
        }
        catch (NotFoundException exception)
        {
            _logger.LogError("Exception: {ExceptionMessage}", exception.Message);
            return -1;
        }
    }

    public async Task<Hotel?> GetHotel(Guid id)
    {
        return await _unitOfWork.Hotels.GetEntity(id);
    }

    public async Task<IEnumerable<Hotel>?> GetHotels()
    {
        return await _unitOfWork.Hotels.GetEntities(ho => true);
    }

    public async Task<Hotel?> PostHotel(Hotel? hotel)
    {
        var added = await _unitOfWork.Hotels.Add(hotel);
        await _unitOfWork.SaveChanges();

        return added;
    }

    public async Task UpdateHotel(Hotel? hotel)
    {
        await _unitOfWork.Hotels.Update(hotel);
        await _unitOfWork.SaveChanges();
    }
}