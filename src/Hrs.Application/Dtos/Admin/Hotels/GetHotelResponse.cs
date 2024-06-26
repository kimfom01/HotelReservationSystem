﻿using Hrs.Application.Dtos.Common;

namespace Hrs.Application.Dtos.Admin.Hotels;

public record GetHotelResponse : BaseDto
{
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
}