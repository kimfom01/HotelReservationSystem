using FluentValidation;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Dtos.Rooms;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Admin.Rooms.Requests.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Admin.Rooms.Handlers.Commands;

public class FreeRoomCommandHandler : IRequestHandler<FreeRoomCommand, Unit>
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

    public async Task<Unit> Handle(FreeRoomCommand command, CancellationToken cancellationToken)
    {
        if (command.RoomRequest is null)
        {
            _logger.LogError("{RoomRequest} is required", nameof(command.RoomRequest));
            throw new ArgumentNullException(nameof(command), $"{nameof(command.RoomRequest)} is required");
        }

        _logger.LogInformation("Attempting to free room={RoomId}", command.RoomRequest.RoomId);

        await _validator.ValidateAndThrowAsync(command.RoomRequest, cancellationToken);

        var room = await _unitOfWork.Rooms.GetEntity(rom =>
            rom.Id == command.RoomRequest.RoomId
            && rom.HotelId == command.RoomRequest.HotelId, cancellationToken);

        if (room is null)
        {
            _logger.LogError("Attempted to free room that does not exist for hotel={HotelId}",
                command.RoomRequest.HotelId);
            throw new NotFoundException(
                $"Attempted to free room that does not exist for hotel={command.RoomRequest.HotelId}");
        }

        room.Availability = true;

        await _unitOfWork.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}