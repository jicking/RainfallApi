using Swashbuckle.AspNetCore.Annotations;

namespace RainfallApi.App.ResponseModels;

[SwaggerSchema(Title = "Error response", Description = "An error object returned for failed requests")]
public class ErrorResponse
{
    public required string Message { get; set; }
    public List<ErrorDetail> Details { get; set; } = new List<ErrorDetail>();
}

[SwaggerSchema(Title = "Details of invalid request property", Description = "Details of invalid request property")]
public class ErrorDetail
{
    public required string PropertyName { get; set; }
    public required string Message { get; set; }
}
