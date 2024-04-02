using System.Linq.Expressions;
using HotelBackend.ReservationService.Models;

namespace HotelBackend.ReservationService.Services;

public interface IPricingService
{
    public Task<Price?> GetPricing(Guid id);
    public Task<Price?> GetPricing(Expression<Func<Price, bool>> expression);
    public Task<IEnumerable<Price>?> GetPrices();
    public Task<IEnumerable<Price>?> GetPrices(Expression<Func<Price, bool>> expression);
    public Task<int> DeletePricing(Guid id);
    public Task UpdatePricing(Price price);
    public Task<Price?> PostPricing(Price price);
}