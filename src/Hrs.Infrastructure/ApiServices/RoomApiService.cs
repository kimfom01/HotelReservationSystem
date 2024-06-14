using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Hrs.Application.Contracts.ApiServices;
using Hrs.Application.Dtos.AdminApi.RoomApi;
using Hrs.Common.Options;
using Microsoft.Extensions.Options;

namespace Hrs.Infrastructure.ApiServices;

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

    public async Task<ReserveRoomApiResponse> PlaceOnHold(ReserveRoomApiRequest roomApiRequest,
        CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/room/hold", roomApiRequest, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return new ReserveRoomApiResponse { Success = false };
        }

        var reserveRoomResponse = await JsonSerializer.DeserializeAsync<ReserveRoomApiResponse>(
            await response.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken
        );

        return new ReserveRoomApiResponse { Success = true, RoomId = reserveRoomResponse?.RoomId };
    }

    public async Task<bool> FreeUpRoom(FreeRoomApiRequest roomApiRequest, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync("api/room/free", roomApiRequest, cancellationToken);

        return response.IsSuccessStatusCode;
    }
}