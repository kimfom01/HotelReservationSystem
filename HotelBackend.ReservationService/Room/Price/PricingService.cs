using System.Linq.Expressions;
using HotelBackend.ReservationService.Data;

namespace HotelBackend.ReservationService.Room.Price;

public class PricingService : IPricingService
{
    private readonly IUnitOfWork _unitOfWork;

    public PricingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PriceModel?> GetPricing(Guid id)
    {
        return await _unitOfWork.Prices.GetEntity(id);
    }

    public async Task<PriceModel?> GetPricing(Expression<Func<PriceModel, bool>> expression)
    {
        return await _unitOfWork.Prices.GetEntity(expression);
    }

    public async Task<IEnumerable<PriceModel>?> GetPrices()
    {
        return await _unitOfWork.Prices.GetEntities(pri => true);
    }

    public async Task<IEnumerable<PriceModel>?> GetPrices(Expression<Func<PriceModel, bool>> expression)
    {
        return await _unitOfWork.Prices.GetEntities(expression);
    }

    public async Task<int> DeletePricing(Guid id)
    {
        await _unitOfWork.Prices.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task UpdatePricing(PriceModel priceModel)
    {
        await _unitOfWork.Prices.Update(priceModel);
        await _unitOfWork.SaveChanges();
    }

    public async Task<PriceModel?> PostPricing(PriceModel priceModel)
    {
        var added = await _unitOfWork.Prices.Add(priceModel);
        await _unitOfWork.SaveChanges();
        return added;
    }
}