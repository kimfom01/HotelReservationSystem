using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HotelBackend.Common.Models.Options;
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

    public async Task<ReserveRoomResponse> PlaceOnHold(ReserveRoomRequestDto roomRequestDto)
    {
        using var response = await _httpClient.PostAsJsonAsync("api/Room/hold", roomRequestDto);

        var success = response.IsSuccessStatusCode;

        var reserveRoomResponse =
            await JsonSerializer.DeserializeAsync<ReserveRoomResponse>(await response.Content.ReadAsStreamAsync());

        return new ReserveRoomResponse
        {
            Success = success,
            RoomId = reserveRoomResponse?.RoomId
        };
    }

    public async Task<bool> FreeUpRoom(FreeRoomRequestDto roomRequestDto)
    {
        // TODO: Implement new free room request

        throw new NotImplementedException();
    }
}