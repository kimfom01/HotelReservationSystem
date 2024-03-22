using System.Linq.Expressions;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Services;

public interface IPricingService
{
    public Task<Pricing?> GetPricing(int id);
    public Task<Pricing?> GetPricing(Expression<Func<Pricing, bool>> expression);
    public Task<IEnumerable<Pricing>?> GetPricings();
    public Task<IEnumerable<Pricing>?> GetPricings(Expression<Func<Pricing, bool>> expression);
    public Task<int> DeletePricing(int id);
    public Task UpdatePricing(Pricing pricing);
    public Task<Pricing> PostPricing(Pricing pricing);
}