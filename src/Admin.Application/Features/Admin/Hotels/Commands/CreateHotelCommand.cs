using Admin.Application.Dtos.Admin.Hotels;
using MediatR;

namespace Admin.Application.Features.Admin.Hotels.Commands;

public record CreateHotelCommand : IRequest<GetHotelResponse>
{
    public CreateHotelRequest? HotelRequest { get; init; }
    public Guid AdminId { get; init; }
}