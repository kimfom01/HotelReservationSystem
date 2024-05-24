using HotelBackend.Admin.Application.Dtos.RoomTypes;
using MediatR;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Requests.Queries;

public class GetRoomTypeRequest : IRequest<GetRoomTypeDto>
{
    public Guid RoomTypeId { get; set; }
    public Guid HotelId { get; set; }
}