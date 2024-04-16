namespace RainfallApi.App.Models;

public class UKRainfallReadingResponse
{
    public required List<StationReading> items { get; set; }
}

public class StationReading
{
    public DateTime dateTime { get; set; }
    public Decimal value { get; set; }
}
