using Hrs.Application.Dtos.Admin.RoomTypes;
using MediatR;

namespace Hrs.Application.Features.Admin.RoomTypes.Command;

public record CreateRoomTypeCommand(CreateRoomTypeRequest RoomTypeRequest) : IRequest<GetRoomTypeResponse>;