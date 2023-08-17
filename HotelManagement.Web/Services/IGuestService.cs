using HotelManagement.Web.Models.Dtos;

namespace HotelManagement.Web.Services;

public interface IGuestService
{
    Task<IEnumerable<Guest>> FetchGuests();
    Task<Guest> AddGuest(Guest guest);
}
