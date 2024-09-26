using Admin.Application.Dtos.Admin.RoomTypes;
using MediatR;

namespace Admin.Application.Features.Admin.RoomTypes.Command;

public record CreateRoomTypeCommand(CreateRoomTypeRequest RoomTypeRequest) : IRequest<GetRoomTypeResponse>;