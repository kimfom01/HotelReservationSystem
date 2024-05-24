using HotelBackend.Admin.Application.Dtos.Common;

namespace HotelBackend.Admin.Application.Dtos.Hotels;

public class GetHotelDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}