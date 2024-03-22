using System.Linq.Expressions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace HotelBackend.General.Api.Services.Implementations;

public class PricingService : IPricingService
{
    private readonly IUnitOfWork _unitOfWork;

    public PricingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Pricing?> GetPricing(int id)
    {
        return await _unitOfWork.Pricings.GetEntity(id);
    }

    public async Task<Pricing?> GetPricing(Expression<Func<Pricing, bool>> expression)
    {
        return await _unitOfWork.Pricings.GetEntity(expression);
    }

    public async Task<IEnumerable<Pricing>?> GetPricings()
    {
        return await _unitOfWork.Pricings.GetEntities(pri => true);
    }

    public async Task<IEnumerable<Pricing>?> GetPricings(Expression<Func<Pricing, bool>> expression)
    {
        return await _unitOfWork.Pricings.GetEntities(expression);
    }

    public async Task<int> DeletePricing(int id)
    {
        await _unitOfWork.Pricings.Delete(id);
        return await _unitOfWork.SaveChanges();
    }

    public async Task UpdatePricing(Pricing pricing)
    {
        await _unitOfWork.Pricings.Update(pricing);
        await _unitOfWork.SaveChanges();
    }

    public async Task<Pricing> PostPricing(Pricing pricing)
    {
        var added = await _unitOfWork.Pricings.Add(pricing);
        await _unitOfWork.SaveChanges();
        return added;
    }
}