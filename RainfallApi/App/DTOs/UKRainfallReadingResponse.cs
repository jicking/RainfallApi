namespace RainfallApi.App.DTOs;

public record UKRainfallReadingResponse(IReadOnlyList<StationReading> items);

public record StationReading(DateTime dateTime, Decimal value);
