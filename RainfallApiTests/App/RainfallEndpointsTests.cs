using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using RainfallApi.App.DTOs;
using RainfallApi.App.Queries;
using Xunit;
using Assert = Xunit.Assert;

namespace RainfallApi.App.Tests;

public class RainfallEndpointsTests
{
    private readonly Mock<IMediator> _mediatorMock;

    public RainfallEndpointsTests()
    {
        _mediatorMock = new Mock<IMediator>();
    }

    [Fact()]
    public async void GetRainfallReadingsAsync_Ok()
    {
        // Arrange
        List<RainfallReading> expectedRainfallReadings = [new RainfallReading(DateTime.Now, 1)];
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetReadingsByStationQuery>(), CancellationToken.None))
            .ReturnsAsync(expectedRainfallReadings);

        // Act
        var response = await RainfallEndpoints.GetRainfallReadingsAsync(_mediatorMock.Object, "testStationId");

        // Assert
        Assert.IsType<Results<BadRequest<ErrorResponse>, NotFound<ErrorResponse>, Ok<RainfallReadingResponse>>>(response);

        var result = (Ok<RainfallReadingResponse>)response.Result;
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

        var value = Assert.IsType<RainfallReadingResponse>(result.Value);
        Assert.True(value.Readings.Any());
        Assert.Equal(expectedRainfallReadings, value.Readings);
    }

    [Fact()]
    public async void GetRainfallReadingsAsync_NotFound()
    {
        // Arrange
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetReadingsByStationQuery>(), CancellationToken.None))
            .ReturnsAsync([]);

        // Act
        var response = await RainfallEndpoints.GetRainfallReadingsAsync(_mediatorMock.Object, "testStationId");

        // Assert
        Assert.IsType<Results<BadRequest<ErrorResponse>, NotFound<ErrorResponse>, Ok<RainfallReadingResponse>>>(response);

        var result = (NotFound<ErrorResponse>)response.Result;
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);

        var value = Assert.IsType<ErrorResponse>(result.Value);
        Assert.Equal(Constants.ErrorMessageReadingNotFound, value.Message);
    }

    [Theory()]
    [InlineData(0)]
    [InlineData(101)]
    public async void GetRainfallReadingsAsync_InvalidCountParameter(int invalidCount)
    {
        // Arrange
        List<ErrorDetail> expectedErrorDetails = [new ErrorDetail("count", Constants.ErrorMessageReadingBadRequestInvalidCount)];
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetReadingsByStationQuery>(), CancellationToken.None))
            .ReturnsAsync([]);

        // Act
        var response = await RainfallEndpoints.GetRainfallReadingsAsync(_mediatorMock.Object, "testStationId", invalidCount);

        // Assert
        Assert.IsType<Results<BadRequest<ErrorResponse>, NotFound<ErrorResponse>, Ok<RainfallReadingResponse>>>(response);

        var result = (BadRequest<ErrorResponse>)response.Result;
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);

        var value = Assert.IsType<ErrorResponse>(result.Value);
        Assert.Equal(Constants.ErrorMessageReadingBadRequest, value.Message);
        Assert.True(value.Details.Any());
        Assert.Equal(expectedErrorDetails, value.Details);
    }
}