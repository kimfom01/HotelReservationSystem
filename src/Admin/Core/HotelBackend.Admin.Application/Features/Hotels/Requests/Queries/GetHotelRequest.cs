using HotelBackend.Admin.Application.Dtos.Hotels;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Hotels.Requests.Queries;

public class GetHotelRequest : IRequest<GetHotelDto>
{
    public Guid HotelId { get; set; }
    public Guid AdminId { get; set; }
}