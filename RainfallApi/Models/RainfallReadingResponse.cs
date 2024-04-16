namespace RainfallApi.Models;

public class RainfallReadingResponse
{
    public required List<RainfallReading> Readings { get; set; }
}

public class RainfallReading
{
    public DateTime DateMeasured { get; set; }
    public decimal AmountMeasured { get; set; }
}
