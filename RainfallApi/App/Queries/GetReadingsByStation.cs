using MediatR;
using RainfallApi.App.ResponseModels;

namespace RainfallApi.App.Queries;

public record GetReadingsByStationQuery(string Id, int count = 10) : IRequest<IList<RainfallReading>>;

public class GetReadingsByStationHandler : IRequestHandler<GetReadingsByStationQuery, IList<RainfallReading>>
{
    public Task<IList<RainfallReading>> Handle(GetReadingsByStationQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
