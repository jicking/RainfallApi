﻿using Microsoft.AspNetCore.Mvc;
using RainfallApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace RainfallApi.Controllers;

[ApiController]
[Route("rainfall")]
[SwaggerTag("Rainfall")]
public class RainfallController : Controller
{
    /// <summary>
    /// Get rainfall readings by station Id
    /// </summary>
    /// <param name="stationId">The id of the reading station</param>
    /// <param name="count">The number of readings to return</param>
    /// <returns> A list of rainfall readings successfully retrieved </returns>
    [HttpGet("id/{stationId}/readings")]
    [SwaggerOperation(OperationId = "get-rainfall")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RainfallReadingResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public IActionResult GetRainfallReadings(string stationId, [FromQuery] int count = 10)
    {
        var readings = new List<RainfallReading>
        {
            new RainfallReading
            {
                DateMeasured = DateTime.UtcNow,
                AmountMeasured = 1
            }
        };

        var response = new RainfallReadingResponse
        {
            Readings = readings
        };
        return Ok(response);
    }
}