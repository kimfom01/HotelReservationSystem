using System.Linq.Expressions;
using HotelBackend.ReservationService.Models;
using HotelBackend.ReservationService.Repositories;

namespace HotelBackend.ReservationService.Services.Implementations;

public class PricingService : IPricingService
{
    private readonly IUnitOfWork _unitOfWork;

    public PricingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Price?> GetPricing(Guid id)
    {
        return await _unitOfWork.Prices.GetEntity(id);
    }

    public async Task<Price?> GetPricing(Expression<Func<Price, bool>> expression)
    {
        return await _unitOfWork.Prices.GetEntity(expression);
    }

    public async Task<IEnumerable<Price>?> GetPrices()
    {
        return await _unitOfWork.Prices.GetEntities(pri => true);
    }

    public async Task<IEnumerable<Price>?> GetPrices(Expression<Func<Price, bool>> expression)
    {
        return await _unitOfWork.Prices.GetEntities(expression);
    }

    public async Task<int> DeletePricing(Guid id)
    {
        await _unitOfWork.Prices.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task UpdatePricing(Price price)
    {
        await _unitOfWork.Prices.Update(price);
        await _unitOfWork.SaveChanges();
    }

    public async Task<Price?> PostPricing(Price price)
    {
        var added = await _unitOfWork.Prices.Add(price);
        await _unitOfWork.SaveChanges();
        return added;
    }
}