namespace RainfallApi.App;

public static class ErrorMessages
{
    public const string GetRainfallReadingNotFound = "No readings found for the specified stationId";
    public const string GetRainfallReadingBadRequest = "Invalid request";
    public const string GetRainfallReadingBadRequestInvalidCount = "Parameter count must be between 1 to 100";
}
