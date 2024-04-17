using Swashbuckle.AspNetCore.Annotations;

namespace RainfallApi.App.ResponseModels;

[SwaggerSchema(Title = "Error response", Description = "An error object returned for failed requests")]
public record ErrorResponse(string Message, List<ErrorDetail> Details = default);

[SwaggerSchema(Title = "Details of invalid request property", Description = "Details of invalid request property")]
public record ErrorDetail(string PropertyName, string Message);
