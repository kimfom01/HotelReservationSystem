using HotelManagement.Web.Models.Dtos;

namespace HotelManagement.Web.Services;

public interface IGuestApiService : IGenericApiService<Guest>
{
    Task<Guest?> FetchGuestByEmailAddress(string emailAddress);
}