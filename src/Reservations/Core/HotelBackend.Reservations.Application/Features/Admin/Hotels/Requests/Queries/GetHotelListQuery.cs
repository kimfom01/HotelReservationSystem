using HotelBackend.Reservations.Application.Dtos.Admin.Hotels;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.Hotels.Requests.Queries;

public record GetHotelListQuery : IRequest<List<GetHotelResponse>>
{
}