using HotelBackend.Reservations.Application.Dtos.Common;

namespace HotelBackend.Reservations.Application.Dtos.Hotels;

public record GetHotelResponse : BaseDto
{
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
}