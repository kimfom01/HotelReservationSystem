using HotelBackend.Admin.Application.Dtos.Hotels;
using MediatR;

namespace HotelBackend.Admin.Application.Features.Hotels.Requests.Commands;

public record CreateHotelCommand : IRequest<GetHotelResponse>
{
    public CreateHotelRequest? HotelRequest { get; init; }
    public Guid AdminId { get; init; }
}