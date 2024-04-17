using Swashbuckle.AspNetCore.Annotations;

namespace RainfallApi.App.ResponseModels;

[SwaggerSchema(Title = "Rainfall reading", Description = "Details of a rainfall reading")]
public record RainfallReading (DateTime DateMeasured, decimal AmountMeasured);

[SwaggerSchema(Title = "Rainfall reading response", Description = "Details of a rainfall reading")]
public record RainfallReadingResponse(List<RainfallReading> Readings);
