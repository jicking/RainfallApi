﻿using Swashbuckle.AspNetCore.Annotations;

namespace RainfallApi.App.DTOs;

[SwaggerSchema(Title = "Rainfall reading", Description = "Details of a rainfall reading")]
public record RainfallReading(DateTime DateMeasured, decimal AmountMeasured);

[SwaggerSchema(Title = "Rainfall reading response", Description = "Details of a rainfall reading")]
public record RainfallReadingResponse(IReadOnlyList<RainfallReading> Readings);
