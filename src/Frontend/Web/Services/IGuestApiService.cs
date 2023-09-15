using Web.Models.Dtos;

namespace Web.Services;

public interface IGuestApiService : IGenericApiService<Guest>
{
    Task<Guest?> FetchGuestByEmailAddress(string emailAddress);
}