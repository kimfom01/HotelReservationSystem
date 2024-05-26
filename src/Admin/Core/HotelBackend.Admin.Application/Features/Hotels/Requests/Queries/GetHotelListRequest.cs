using HotelBackend.Admin.Application.Dtos.Hotels;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Hotels.Requests.Queries;

public class GetHotelListRequest : IRequest<List<GetHotelDto>>
{
}