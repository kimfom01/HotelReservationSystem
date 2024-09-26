using Admin.Application.Dtos.Admin.Hotels;
using MediatR;

namespace Admin.Application.Features.Admin.Hotels.Queries;

public record GetHotelQuery : IRequest<GetHotelResponse>
{
    public Guid HotelId { get; init; }
}