using Microsoft.AspNetCore.Diagnostics;
using RainfallApi.App.ResponseModels;

namespace RainfallApi;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, $"Exception occurred: { exception.Message}");

        var errorResponse = new ErrorResponse()
        {
            Message = exception.Message
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response
            .WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }
}
