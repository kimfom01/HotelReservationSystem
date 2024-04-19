using HotelBackend.Application.Dtos;
using MediatR;

namespace HotelBackend.Application.Features.Hotels.Requests.Queries;

public class GetHotelListRequest : IRequest<List<HotelDto>>
{
}