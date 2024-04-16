namespace RainfallApi.Models;

public class ErrorResponse
{
    public required string Message { get; set; }
    public List<ErrorDetail> Details { get; set; }
}

public class ErrorDetail
{
    public required string PropertyName { get; set; }
    public required string Message { get; set; }
}
