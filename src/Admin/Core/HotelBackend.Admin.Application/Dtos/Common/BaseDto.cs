namespace HotelBackend.Admin.Application.Dtos.Common;

public abstract record BaseDto
{
    public Guid Id { get; init; }
}