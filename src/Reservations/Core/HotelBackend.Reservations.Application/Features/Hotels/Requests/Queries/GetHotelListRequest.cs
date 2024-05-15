using HotelBackend.Reservations.Application.Dtos;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Hotels.Requests.Queries;

public class GetHotelListRequest : IRequest<List<HotelDto>>
{
}