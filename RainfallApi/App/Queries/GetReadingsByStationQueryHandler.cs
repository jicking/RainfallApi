using MediatR;
using RainfallApi.App.DTOs;

namespace RainfallApi.App.Queries;

public record GetReadingsByStationQuery(string id, int count = 10) : IRequest<IReadOnlyList<RainfallReading>>;

public class GetReadingsByStationQueryHandler : IRequestHandler<GetReadingsByStationQuery, IReadOnlyList<RainfallReading>>
{
    private readonly IUKRainfallService _ukRainfallService;

    public GetReadingsByStationQueryHandler(IUKRainfallService ukRainfallService) =>
        _ukRainfallService = ukRainfallService;

    public async Task<IReadOnlyList<RainfallReading>> Handle(GetReadingsByStationQuery request, CancellationToken cancellationToken)
    {
        var stationReadingResponse = await _ukRainfallService.GetStationReadingsAsync(request.id, request.count);

        var result = stationReadingResponse.items
            .Select(i => new RainfallReading(i.dateTime, i.value))
            .ToList();

        return result;
    }
}
