using HotelBackend.Admin.Application.Dtos.Hotels;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Hotels.Requests.Queries;

public record GetHotelListQuery : IRequest<List<GetHotelResponse>>
{
}