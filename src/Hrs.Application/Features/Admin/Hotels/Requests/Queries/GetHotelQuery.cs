using Hrs.Application.Dtos.Admin.Hotels;
using MediatR;

namespace Hrs.Application.Features.Admin.Hotels.Requests.Queries;

public record GetHotelQuery : IRequest<GetHotelResponse>
{
    public Guid HotelId { get; init; }
}