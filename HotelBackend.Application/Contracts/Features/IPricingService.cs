using System.Linq.Expressions;
using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.Contracts.Features;

public interface IPricingService
{
    public Task<PriceModel?> GetPricing(Guid id);
    public Task<PriceModel?> GetPricing(Expression<Func<PriceModel, bool>> expression);
    public Task<IEnumerable<PriceModel>?> GetPrices();
    public Task<IEnumerable<PriceModel>?> GetPrices(Expression<Func<PriceModel, bool>> expression);
    public Task<int> DeletePricing(Guid id);
    public Task UpdatePricing(PriceModel priceModel);
    public Task<PriceModel?> PostPricing(PriceModel priceModel);
}