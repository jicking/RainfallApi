namespace RainfallApi.App;

public static class Constants
{
    public const string ErrorMessageReadingNotFound = "No readings found for the specified stationId";
    public const string ErrorMessageReadingBadRequest = "Invalid request";
    public const string ErrorMessageReadingBadRequestInvalidCount = "Parameter count must be between 1 to 100";

    public const string UKRainfallAPI = "UKRainfallAPI";

    public const string RainfallEndpointsPrefix = "/rainfall";
    public const string RainfallEndpointsTag = "Rainfall";
    
    public const string GetRainfallOperationId = "get-rainfall";


}
