using HotelBackend.Reservations.Application.Dtos.Hotels;
using MediatR;

namespace HotelBackend.Reservations.Application.Features.Admin.Hotels.Requests.Queries;

public record GetHotelQuery : IRequest<GetHotelResponse>
{
    public Guid HotelId { get; init; }
}