﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.App.Queries;
using RainfallApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RainfallApi.Controllers;

[ApiController]
[Route("rainfall")]
[SwaggerTag("Rainfall")]
public class RainfallController : Controller
{
    private readonly ILogger<RainfallController> _logger;
    private readonly IMediator _mediator;

    public RainfallController(ILogger<RainfallController> logger, IMediator mediator)
    {
        this._logger = logger;
        this._mediator = mediator;
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
    public async Task<IActionResult> GetRainfallReadingsAsync(string stationId, [FromQuery, Range(1, 100)] int count = 10)
    {
        try
        {
            if (count < 0 && count > 100)
            {
                return BadRequest();
            }

            var readings = await _mediator.Send(new GetReadingsByStationQuery(stationId, count));

            if (!readings.Any())
            {
                var errorResponse = new ErrorResponse()
                {
                    Details = new List<ErrorDetail>(),
                    Message = "No readings found for the specified stationId"
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
            var errorResponse = new ErrorResponse() { 
                Details = new List<ErrorDetail>(),
                Message = ex.Message
            };

            return StatusCode(500, errorResponse);
        }
    }
}
