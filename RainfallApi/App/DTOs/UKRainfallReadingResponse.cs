namespace RainfallApi.App.Models;

public record UKRainfallReadingResponse(List<StationReading> items);

public record StationReading(DateTime dateTime, Decimal value);
