using MediatR;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.App.Queries;
using RainfallApi.App.ResponseModels;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace RainfallApi.App;

[ApiController]
[Route("rainfall")]
[SwaggerTag("Rainfall")]
public class RainfallController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<RainfallController> _logger;

    public RainfallController(IMediator mediator, ILogger<RainfallController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get rainfall readings by station Id
    /// </summary>
    /// <param name="stationId">The id of the reading station</param>
    /// <param name="count">The number of readings to return</param>
    /// <returns>A list of rainfall readings successfully retrieved </returns>
    /// <response code="200">A list of rainfall readings successfully retrieved</response>
    /// <response code="400">Invalid request</response>
    /// <response code="404">No readings found for the specified stationId</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("id/{stationId}/readings")]
    [SwaggerOperation(OperationId = "get-rainfall")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RainfallReadingResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [Produces("application/json")]
    public async Task<IActionResult> GetRainfallReadingsAsync(string stationId, [FromQuery] int count = 10)
    {
        try
        {
            if (count <= 0 || count > 100)
            {
                var errorResponse = new ErrorResponse()
                {
                    Details = new List<ErrorDetail>()
                {
                    new ErrorDetail()
                    {
                        PropertyName = "count",
                        Message = Constants.ErrorMessageReadingBadRequestInvalidCount
                    }
                },
                    Message = Constants.ErrorMessageReadingBadRequest
                };
                return BadRequest(errorResponse);
            }

            var readings = await _mediator.Send(new GetReadingsByStationQuery(stationId, count));

            if (!readings.Any())
            {
                var errorResponse = new ErrorResponse()
                {
                    Message = Constants.ErrorMessageReadingNotFound
                };
                return NotFound(errorResponse);
            }

            var response = new RainfallReadingResponse
            {
                Readings = readings.ToList(),
            };
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception occurred: {ex.Message}");
            var errorResponse = new ErrorResponse()
            {
                Message = ex.Message
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }
}
