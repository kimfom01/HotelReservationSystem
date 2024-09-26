using Admin.Application.Contracts.Database;
using Admin.Application.Dtos.Admin.Rooms;
using FluentValidation;
using Hrs.Common.Exceptions;
using Admin.Domain.Entities.Admin;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Admin.Application.Features.Admin.Rooms.Commands;

//TODO: Change to add rooms, creating initial number of rooms will be done when a room type is created
public class CreateManyRoomsCommandHandler : IRequestHandler<CreateManyRoomsCommand, int>
{
    private readonly ILogger<CreateManyRoomsCommandHandler> _logger;
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly IValidator<CreateManyRoomsRequest> _validator;

    public CreateManyRoomsCommandHandler(
        IAdminUnitOfWork unitOfWork,
        ILogger<CreateManyRoomsCommandHandler> logger,
        IValidator<CreateManyRoomsRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<int> Handle(CreateManyRoomsCommand command, CancellationToken cancellationToken)
    {
        if (command.RoomsRequest is null)
        {
            _logger.LogError("Error: {RoomsRequest} is null", nameof(command.RoomsRequest));
            throw new ArgumentNullException(nameof(command), $"{nameof(command.RoomsRequest)} is required");
        }

        _logger.LogInformation("Creating multiple rooms of type={RoomTypeId}", command.RoomsRequest.RoomTypeId);

        await _validator.ValidateAndThrowAsync(command.RoomsRequest, cancellationToken);

        var roomType = await _unitOfWork.RoomTypes.GetRoomType(
            command.RoomsRequest.RoomTypeId,
            command.RoomsRequest.HotelId,
            cancellationToken);

        if (roomType is null)
        {
            throw new NotFoundException($"{nameof(RoomType)} with id={command.RoomsRequest.RoomTypeId} does not exist");
        }

        roomType.CreateRooms(
            command.RoomsRequest.Count,
            command.RoomsRequest.Start,
            command.RoomsRequest.RoomTypeId,
            roomType.Type,
            command.RoomsRequest.HotelId);

        await _unitOfWork.Rooms.AddMany(roomType.Rooms, cancellationToken);
        var count = await _unitOfWork.SaveChanges(cancellationToken);

        return count;
    }
}