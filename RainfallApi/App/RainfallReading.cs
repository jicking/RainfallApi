namespace RainfallApi.App;

public record RainfallReading
{
    public DateTime DateMeasured { get; set; }
    public decimal AmountMeasured { get; set; }
}
