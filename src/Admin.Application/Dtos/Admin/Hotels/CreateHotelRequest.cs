namespace Admin.Application.Dtos.Admin.Hotels;

public record CreateHotelRequest
{
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
}