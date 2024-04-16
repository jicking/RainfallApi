using Microsoft.AspNetCore.Mvc;
using RainfallApi.Models;

namespace RainfallApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RainfallController : Controller
{
    [HttpGet("id/{stationId}/readings")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RainfallReadingResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public IActionResult GetRainfallReadings(string stationId, [FromQuery] int count = 10)
    {
        var readings = new List<RainfallReading>();
        readings.Add(new RainfallReading
        {
            DateMeasured = DateTime.UtcNow,
            AmountMeasured = 1
        });

        var response = new RainfallReadingResponse
        {
            Readings = readings
        };
        return Ok(response);
    }
}
