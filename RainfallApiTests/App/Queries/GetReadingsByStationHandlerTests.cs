using Moq;
using RainfallApi.App.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace RainfallApi.App.Queries.Tests;

public class GetReadingsByStationHandlerTests
{
    [Fact()]
    public async void HandleTest()
    {
        const string stationId = "stationId";
        const int count = 10;
        var mockUkRainfallService = new Mock<IUKRainfallService>();
        var expectedResponse = new UKRainfallReadingResponse
        {
            items = new List<StationReading>
        {
            new StationReading { dateTime = DateTime.Now, value = 10 },
            new StationReading { dateTime = DateTime.Now, value = 15 }
        }
        };

        mockUkRainfallService.Setup(s => s.GetStationReadingsAsync(stationId, count))
                             .ReturnsAsync(expectedResponse);

        var handler = new GetReadingsByStationHandler(mockUkRainfallService.Object);
        var query = new GetReadingsByStationQuery(stationId, count);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(expectedResponse.items[0].dateTime, result[0].DateMeasured);
        Assert.Equal(expectedResponse.items[0].value, result[0].AmountMeasured);
        Assert.Equal(expectedResponse.items[1].dateTime, result[1].DateMeasured);
        Assert.Equal(expectedResponse.items[1].value, result[1].AmountMeasured);
    }
}