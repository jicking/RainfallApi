//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging.Abstractions;
//using Moq;
//using RainfallApi.App.Queries;
//using RainfallApi.App.ResponseModels;
//using Xunit;
//using Assert = Xunit.Assert;

//namespace RainfallApi.App.Tests;

//public class RainfallControllerTests
//{
//    private readonly Mock<IMediator> _mediatorMock;
//    private readonly RainfallController _sut;

//    public RainfallControllerTests()
//    {
//        var logger = NullLogger<RainfallController>.Instance;
//        _mediatorMock = new Mock<IMediator>();
//        _sut = new RainfallController(_mediatorMock.Object, logger);
//    }

//    [Fact()]
//    public async void GetRainfallReadingsAsync_OkTest()
//    {
//        // Arrange
//        _mediatorMock.Setup(m => m.Send(It.IsAny<GetReadingsByStationQuery>(), CancellationToken.None))
//                     .ReturnsAsync(new List<RainfallReading>() {
//                         new RainfallReading(DateTime.Now, 1)
//                     });

//        // Act
//        var result = await _sut.GetRainfallReadingsAsync("1", 1);

//        // Assert
//        var objectResult = Assert.IsType<OkObjectResult>(result);
//        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
//        var model = Assert.IsType<RainfallReadingResponse>(objectResult.Value);
//        Assert.True(model.Readings.Any());
//    }

//    [Fact]
//    public async Task GetRainfallReadingsAsync_NotFoundTestx()
//    {
//        // Arrange
//        _mediatorMock.Setup(m => m.Send(It.IsAny<GetReadingsByStationQuery>(), CancellationToken.None))
//                     .ReturnsAsync(new List<RainfallReading>());

//        // Act
//        var result = await _sut.GetRainfallReadingsAsync("1", 1);

//        // Assert
//        var objectResult = Assert.IsType<NotFoundObjectResult>(result);
//        Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
//        var model = Assert.IsType<ErrorResponse>(objectResult.Value);
//        Assert.Equal(Constants.ErrorMessageReadingNotFound, model.Message);
//    }

//    [Theory()]
//    [InlineData(0)]
//    [InlineData(101)]
//    public async void GetRainfallReadingsAsync_InvalidCountParameterTest(int count)
//    {
//        // Arrange
//        _mediatorMock.Setup(m => m.Send(It.IsAny<GetReadingsByStationQuery>(), CancellationToken.None))
//                     .ReturnsAsync(new List<RainfallReading>());

//        // Act
//        var result = await _sut.GetRainfallReadingsAsync("1", count);

//        // Assert
//        var objectResult = Assert.IsType<BadRequestObjectResult>(result);
//        Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
//        var model = Assert.IsType<ErrorResponse>(objectResult.Value);
//        Assert.Equal(Constants.ErrorMessageReadingBadRequest, model.Message);
//    }

//    [Fact()]
//    public async void GetRainfallReadingsAsync_ExceptionTest()
//    {
//        // Arrange
//        const string exceptionMessage = "Test Exception Message";
//        _mediatorMock.Setup(m => m.Send(It.IsAny<GetReadingsByStationQuery>(), CancellationToken.None))
//                     .ThrowsAsync(new Exception(exceptionMessage));

//        // Act
//        var result = await _sut.GetRainfallReadingsAsync("1", 1);

//        // Assert
//        var objectResult = Assert.IsType<ObjectResult>(result);
//        Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
//        var model = Assert.IsType<ErrorResponse>(objectResult.Value);
//        Assert.Equal(exceptionMessage, model.Message);
//    }
//}