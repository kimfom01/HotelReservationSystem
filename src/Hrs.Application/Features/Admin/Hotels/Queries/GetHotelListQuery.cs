using Hrs.Application.Dtos.Admin.Hotels;
using MediatR;

namespace Hrs.Application.Features.Admin.Hotels.Queries;

public record GetHotelListQuery : IRequest<List<GetHotelResponse>>
{
}