namespace RainfallApi.App;

public static class ErrorMessages
{
    public static readonly string GetRainfallReadingNotFound = "No readings found for the specified stationId";
    public static readonly string GetRainfallReadingBadRequest = "Invalid request";
    public static readonly string GetRainfallReadingBadRequestInvalidCount = "Parameter count must be between 1 to 100";
}
