using HotelManagement.Web.Models.Dtos;
using System.Text.Json;

namespace HotelManagement.Web.Services;

public class GuestApiService : GenericApiService<Guest>, IGuestApiService
{
    private readonly HttpClient _client;

    public GuestApiService(HttpClient client) : base(client)
    {
        _client = client;
    }

    public async Task<Guest?> FetchGuestByEmailAddress(string emailAddress)
    {
        using var response = await _client.GetAsync($"{nameof(Guest)}/{emailAddress}");

        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var stream = await response.Content.ReadAsStreamAsync();

        var guest = await JsonSerializer.DeserializeAsync<Guest>(stream);

        return guest;
    }
}
