using MediatR;
using RainfallApi.App.ResponseModels;
using System.Text.Json;

namespace RainfallApi.App.Queries;

public record GetReadingsByStationQuery(string id, int count = 10) : IRequest<IList<RainfallReading>>;

public class GetReadingsByStationHandler : IRequestHandler<GetReadingsByStationQuery, IList<RainfallReading>>
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GetReadingsByStationHandler(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    public async Task<IList<RainfallReading>> Handle(GetReadingsByStationQuery request, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(Constants.EnvironmentAgencyRainfallAPI);
        var httpResponseMessage = await httpClient.GetAsync(
            $"id/stations/{request.id}/readings?_sorted&_limit={request.count}");
        httpResponseMessage.EnsureSuccessStatusCode();

        using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

        var stationReadingResponse = await JsonSerializer.DeserializeAsync
            <StationReadingResponse>(contentStream);

        var result = stationReadingResponse.items
            .Select(i => new RainfallReading { DateMeasured = i.dateTime, AmountMeasured = i.value })
            .ToList();

        return result;
    }
}

internal class StationReadingResponse
{
    public required List<StationReading> items { get; set; }
}

internal class StationReading
{
    public DateTime dateTime { get; set; }
    public Decimal value { get; set; }
}