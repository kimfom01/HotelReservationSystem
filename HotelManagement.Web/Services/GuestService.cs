using HotelManagement.Web.Models.Dtos;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HotelManagement.Web.Services;

public class GuestService : IGuestService
{
    private readonly HttpClient _client;

    public GuestService(HttpClient client)
    {
        _client = client;
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.BaseAddress = new Uri("https://localhost:7214/api/Guest");
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public Task<Guest> AddGuest(Guest guest)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Guest>> FetchGuests()
    {
        using var response = await _client.GetAsync("");

        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<Guest>();
        }

        var stream = await response.Content.ReadAsStreamAsync();

        var guestList = await JsonSerializer.DeserializeAsync<IEnumerable<Guest>>(stream);

        return guestList ?? Enumerable.Empty<Guest>();
    }
}
