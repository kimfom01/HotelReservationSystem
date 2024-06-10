using HotelBackend.Admin.Application.Dtos.Hotels;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Hotels.Requests.Queries;

public record GetHotelQuery : IRequest<GetHotelResponse>
{
    public Guid HotelId { get; init; }
}