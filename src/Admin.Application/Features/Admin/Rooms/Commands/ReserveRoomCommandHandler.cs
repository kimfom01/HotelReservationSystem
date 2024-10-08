using Admin.Application.Contracts.Database;
using Admin.Application.Dtos.Admin.Rooms;
using FluentValidation;
using Hrs.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Admin.Application.Features.Admin.Rooms.Commands;

public class ReserveRoomCommandHandler : IRequestHandler<ReserveRoomCommand, ReserveRoomResponse>
{
    private readonly ILogger<ReserveRoomCommandHandler> _logger;
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly IValidator<ReserveRoomRequest> _validator;

    public ReserveRoomCommandHandler(
        ILogger<ReserveRoomCommandHandler> logger,
        IAdminUnitOfWork unitOfWork,
        IValidator<ReserveRoomRequest> validator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<ReserveRoomResponse> Handle(ReserveRoomCommand command, CancellationToken cancellationToken)
    {
        if (command.RoomRequest is null)
        {
            _logger.LogError("{RoomDto} is required", nameof(command.RoomRequest));
            throw new ArgumentNullException(nameof(command), $"{nameof(command.RoomRequest)} is required");
        }

        _logger.LogInformation("Placing a room on hold");

        await _validator.ValidateAndThrowAsync(command.RoomRequest, cancellationToken);

        var room = await _unitOfWork.Rooms
            .GetRoomOfType(command.RoomRequest.HotelId, command.RoomRequest.RoomTypeId);

        if (room is null)
        {
            _logger.LogError("No available room of type={RoomTypeId} in hotel={HotelId}",
                command.RoomRequest.RoomTypeId, command.RoomRequest.HotelId);
            throw new NotFoundException(
                $"No available room of type={command.RoomRequest.RoomTypeId} in hotel={command.RoomRequest.HotelId}");
        }

        room.SetReserved();

        await _unitOfWork.SaveChanges(cancellationToken);

        return new ReserveRoomResponse
        {
            RoomId = room.Id
        };
    }
}