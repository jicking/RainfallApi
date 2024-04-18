using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RainfallApi.App.Queries;
using RainfallApi.App.DTOs;

namespace RainfallApi.App;

public static class RainfallEndpoints
{
    public static void MapRainfallEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(Constants.RainfallEndpointsPrefix)
            .WithOpenApi(o => new OpenApiOperation(o)
            {
                Tags = new List<OpenApiTag> {
                    new() { Name = Constants.RainfallEndpointsTag }
                },
            });

        group.MapGet("id/{stationId}/readings", GetRainfallReadingsAsync)
            .Produces<RainfallReadingResponse>(StatusCodes.Status200OK)
            .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .WithOpenApi(o => new OpenApiOperation(o)
            {
                OperationId = Constants.GetRainfallOperationId,
            });
    }

    /// <summary>
    /// Get rainfall readings by station Id
    /// </summary>
    /// <param name="stationId">The id of the reading station</param>
    /// <param name="count">The number of readings to return</param>
    /// <param name="mediator"></param>
    /// <returns>A list of rainfall readings successfully retrieved </returns>
    /// <response code="200">A list of rainfall readings successfully retrieved</response>
    /// <response code="400">Invalid request</response>
    /// <response code="404">No readings found for the specified stationId</response>
    /// <response code="500">Internal server error</response>
    public static async Task<Results<BadRequest<ErrorResponse>, NotFound<ErrorResponse>, Ok<RainfallReadingResponse>>> GetRainfallReadingsAsync(
        IMediator mediator, string stationId, [FromQuery] int count = 10)
    {
        if (count <= 0 || count > 100)
        {
            return TypedResults.BadRequest(
                new ErrorResponse(
                    Constants.ErrorMessageReadingBadRequest,
                    [new ErrorDetail("count", Constants.ErrorMessageReadingBadRequestInvalidCount)]
                )
            );
        }

        var readings = await mediator.Send(new GetReadingsByStationQuery(stationId, count));

        if (!readings.Any())
        {
            var errorResponse = new ErrorResponse(Constants.ErrorMessageReadingNotFound);
            return TypedResults.NotFound(errorResponse);
        }

        var response = new RainfallReadingResponse(readings.ToList());
        return TypedResults.Ok(response);
    }
}
