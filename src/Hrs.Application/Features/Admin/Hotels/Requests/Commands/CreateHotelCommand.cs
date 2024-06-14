using Hrs.Application.Dtos.Admin.Hotels;
using MediatR;

namespace Hrs.Application.Features.Admin.Hotels.Requests.Commands;

public record CreateHotelCommand : IRequest<GetHotelResponse>
{
    public CreateHotelRequest? HotelRequest { get; init; }
    public Guid AdminId { get; init; }
}