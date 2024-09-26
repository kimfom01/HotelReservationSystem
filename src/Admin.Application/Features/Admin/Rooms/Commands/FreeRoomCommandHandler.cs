using Admin.Application.Contracts.Database;
using Admin.Application.Dtos.Admin.Rooms;
using FluentValidation;
using Hrs.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Admin.Application.Features.Admin.Rooms.Commands;

public class FreeRoomCommandHandler : IRequestHandler<FreeRoomCommand, bool>
{
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly ILogger<FreeRoomCommandHandler> _logger;
    private readonly IValidator<FreeRoomRequest> _validator;

    public FreeRoomCommandHandler(
        IAdminUnitOfWork unitOfWork,
        ILogger<FreeRoomCommandHandler> logger,
        IValidator<FreeRoomRequest> validator
    )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<bool> Handle(FreeRoomCommand command, CancellationToken cancellationToken)
    {
        if (command.RoomRequest is null)
        {
            _logger.LogError("{RoomRequest} is required", nameof(command.RoomRequest));
            throw new ArgumentNullException(nameof(command), $"{nameof(command.RoomRequest)} is required");
        }

        _logger.LogInformation("Attempting to free room={RoomId}", command.RoomRequest.RoomId);

        await _validator.ValidateAndThrowAsync(command.RoomRequest, cancellationToken);

        var room = await _unitOfWork.Rooms.GetRoom(
            command.RoomRequest.RoomId,
            command.RoomRequest.HotelId,
            cancellationToken);

        if (room is null)
        {
            _logger.LogError("Attempted to free room that does not exist for hotel={HotelId}",
                command.RoomRequest.HotelId);
            throw new NotFoundException(
                $"Attempted to free room that does not exist for hotel={command.RoomRequest.HotelId}");
        }

        room.SetFree();

        await _unitOfWork.SaveChanges(cancellationToken);

        return true;
    }
}