using System.Net.Http.Headers;
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

    public async Task<bool> SetRoomAvailability(UpdateRoomAvailabilityDto roomAvailabilityDto)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(roomAvailabilityDto),
            Encoding.UTF8,
            "application/json");

        using var response = await _httpClient.PatchAsync("api/Room/", jsonContent);

        var success = response.IsSuccessStatusCode;

        return success;
    }
}