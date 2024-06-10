using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using HotelBackend.Common.Options;
using HotelBackend.Reservations.Application.Contracts.ApiServices;
using HotelBackend.Reservations.Application.Dtos.AdminApi.RoomApi;
using Microsoft.Extensions.Options;

namespace HotelBackend.Reservations.Infrastructure.ApiServices;

public class RoomApiService : IRoomApiService
{
    private readonly HttpClient _httpClient;

    public RoomApiService(HttpClient httpClient, IOptions<RoomApiOptions> roomApiOptions)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(roomApiOptions.Value.BaseUrl);
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<ReserveRoomApiResponse> PlaceOnHold(ReserveRoomApiRequest roomApiRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("api/room/hold", roomApiRequest);

        if (!response.IsSuccessStatusCode)
        {
            return new ReserveRoomApiResponse { Success = false };
        }

        var reserveRoomResponse = await JsonSerializer.DeserializeAsync<ReserveRoomApiResponse>(
            await response.Content.ReadAsStreamAsync()
        );

        return new ReserveRoomApiResponse { Success = true, RoomId = reserveRoomResponse?.RoomId };
    }

    public async Task<bool> FreeUpRoom(FreeRoomApiRequest roomApiRequest)
    {
        var response = await _httpClient.PostAsJsonAsync("api/room/free", roomApiRequest);

        return response.IsSuccessStatusCode;
    }
}