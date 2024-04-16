using RainfallApi.App;

namespace RainfallApi.Models;

public class RainfallReadingResponse
{
    public required List<RainfallReading> Readings { get; set; }
}
