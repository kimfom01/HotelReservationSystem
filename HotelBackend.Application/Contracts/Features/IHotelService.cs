using HotelBackend.Domain.Entities;

namespace HotelBackend.Application.Contracts.Features;

public interface IHotelService
{
    Task<IEnumerable<Hotel>?> GetHotels();
}
