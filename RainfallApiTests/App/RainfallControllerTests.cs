using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RainfallApi.App.Queries;
using RainfallApi.App.ResponseModels;
using Xunit;
using Assert = Xunit.Assert;

namespace RainfallApi.App.Tests;

public class RainfallControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly RainfallController _sut;

    public RainfallControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _sut = new RainfallController(_mediatorMock.Object);
    }

    [Fact()]
    public async void GetRainfallReadingsAsync_OkTest()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetReadingsByStationQuery>(), CancellationToken.None))
                     .ReturnsAsync(new List<RainfallReading>() {
                         new RainfallReading() { DateMeasured = DateTime.Now, AmountMeasured = 1 }
                     });

        // Act
        var result = await _sut.GetRainfallReadingsAsync("1", 1);

        // Assert
        var objectResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<RainfallReadingResponse>(objectResult.Value);
        Assert.True(model.Readings.Any());
    }

    [Fact]
    public async Task GetRainfallReadingsAsync_NotFoundTestx()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetReadingsByStationQuery>(), CancellationToken.None))
                     .ReturnsAsync(new List<RainfallReading>());

        // Act
        var result = await _sut.GetRainfallReadingsAsync("1", 1);

        // Assert
        var objectResult = Assert.IsType<NotFoundObjectResult>(result);
        var model = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(ErrorMessages.GetRainfallReadingNotFound, model.Message);
    }

    [Theory()]
    [InlineData(0)]
    [InlineData(101)]
    public async void GetRainfallReadingsAsync_InvalidCountParameterTest(int count)
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetReadingsByStationQuery>(), CancellationToken.None))
                     .ReturnsAsync(new List<RainfallReading>());

        // Act
        var result = await _sut.GetRainfallReadingsAsync("1", count);

        // Assert
        var objectResult = Assert.IsType<BadRequestObjectResult>(result);
        var model = Assert.IsType<ErrorResponse>(objectResult.Value);
        Assert.Equal(ErrorMessages.GetRainfallReadingBadRequest, model.Message);
    }

    [Fact()]
    public void GetRainfallReadingsAsync_ExceptionTest()
    {
        Xunit.Assert.Fail("This test needs an implementation");
    }
}