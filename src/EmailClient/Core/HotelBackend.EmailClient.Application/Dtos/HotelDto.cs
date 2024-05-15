using HotelBackend.EmailClient.Application.Dtos.Common;

namespace HotelBackend.EmailClient.Application.Dtos;

public class HotelDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}