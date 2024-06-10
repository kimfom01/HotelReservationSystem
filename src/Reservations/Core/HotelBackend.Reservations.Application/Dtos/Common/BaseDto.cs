namespace HotelBackend.Reservations.Application.Dtos.Common;

public abstract record BaseDto
{
    public Guid Id { get; init; }
}