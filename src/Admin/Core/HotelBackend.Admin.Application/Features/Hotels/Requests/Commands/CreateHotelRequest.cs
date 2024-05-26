using HotelBackend.Admin.Application.Dtos.Hotels;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Hotels.Requests.Commands;

public class CreateHotelRequest : IRequest<GetHotelDto>
{
    public CreateHotelDto? HotelDto { get; set; }
    public Guid AdminId { get; set; }
}