using RainfallApi.App.DTOs;

namespace RainfallApi.App;

public interface IUKRainfallService
{
    Task<UKRainfallReadingResponse> GetStationReadingsAsync(string id, int count);
}

public class UKRainfallService : IUKRainfallService
{
    private readonly HttpClient _httpClient;

    public UKRainfallService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UKRainfallReadingResponse> GetStationReadingsAsync(string id, int count) =>
        await _httpClient.GetFromJsonAsync<UKRainfallReadingResponse>(
            $"id/stations/{id}/readings?_sorted&_limit={count}");
}
