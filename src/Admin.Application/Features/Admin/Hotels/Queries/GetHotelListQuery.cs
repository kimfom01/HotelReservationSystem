using Admin.Application.Dtos.Admin.Hotels;
using MediatR;

namespace Admin.Application.Features.Admin.Hotels.Queries;

public record GetHotelListQuery : IRequest<List<GetHotelResponse>>
{
}