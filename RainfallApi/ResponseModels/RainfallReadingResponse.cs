using Swashbuckle.AspNetCore.Annotations;

namespace RainfallApi.ResponseModels;

[SwaggerSchema(Title = "Rainfall reading", Description = "Details of a rainfall reading")]
public record RainfallReading
{
    public DateTime DateMeasured { get; set; }
    public decimal AmountMeasured { get; set; }
}

[SwaggerSchema(Title = "Rainfall reading response", Description = "Details of a rainfall reading")]
public class RainfallReadingResponse
{
    public required List<RainfallReading> Readings { get; set; }
}
