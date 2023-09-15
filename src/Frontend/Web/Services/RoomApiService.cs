using System.Text.Json;
using Web.Models.Dtos;

namespace Web.Services;

public class RoomApiService : GenericApiService<Room>, IRoomApiService
{
    private readonly HttpClient _client;

    public RoomApiService(HttpClient client) : base(client)
    {
        _client = client;
    }

    public async Task<Room?> FetchRoomByHotelId(int hotelId, int capacity)
    {
        using var response = await _client.GetAsync($"{nameof(Room)}/hotel/{hotelId}/{capacity}");

        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

        var stream = await response.Content.ReadAsStreamAsync();

        var room = await JsonSerializer.DeserializeAsync<Room>(stream);

        return room;
    }
}
