using HotelBackend.Admin.Application.Dtos.RoomTypes;
using MediatR;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Requests.Queries;

public class GetRoomTypeListRequest : IRequest<List<GetRoomTypeDto>>
{
    public Guid HotelId { get; set; }
}