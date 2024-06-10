using HotelBackend.Admin.Application.Dtos.Common;

namespace HotelBackend.Admin.Application.Dtos.Hotels;

public record GetHotelResponse : BaseDto
{
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
}