using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HotelManagement.Web.Services;

public class GenericApiService<TEntity> : IGenericApiService<TEntity>
{
    private readonly HttpClient _client;

    public GenericApiService(HttpClient client)
    {
        _client = client;
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<TEntity?> AddEntity(TEntity entity)
    {
        var jsonString = JsonSerializer.Serialize(entity);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        var result = await _client.PostAsync(typeof(TEntity).Name, content);

        if (!result.IsSuccessStatusCode)
        {
            return default;
        }

        var stream = await result.Content.ReadAsStreamAsync();

        var entityResult = await JsonSerializer.DeserializeAsync<TEntity?>(stream);

        return entityResult;
    }

    public async Task<IEnumerable<TEntity>> FetchEntities()
    {
        using var response = await _client.GetAsync(typeof(TEntity).Name);

        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<TEntity>();
        }

        var stream = await response.Content.ReadAsStreamAsync();

        var guestList = await JsonSerializer.DeserializeAsync<IEnumerable<TEntity>>(stream);

        return guestList ?? Enumerable.Empty<TEntity>();
    }
}
