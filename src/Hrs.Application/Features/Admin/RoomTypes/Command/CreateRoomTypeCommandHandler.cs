using AutoMapper;
using FluentValidation;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.RoomTypes;
using Hrs.Domain.Entities.Admin;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.RoomTypes.Command;

public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, GetRoomTypeResponse>
{
    private readonly ILogger<CreateRoomTypeCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly IValidator<CreateRoomTypeRequest> _validator;

    public CreateRoomTypeCommandHandler(
        ILogger<CreateRoomTypeCommandHandler> logger,
        IMapper mapper,
        IAdminUnitOfWork unitOfWork,
        IValidator<CreateRoomTypeRequest> validator)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<GetRoomTypeResponse> Handle(CreateRoomTypeCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating room type");
        if (command.RoomTypeRequest is null)
        {
            _logger.LogError("Error: {RoomTypeDto} is null", nameof(command.RoomTypeRequest));
            throw new ArgumentNullException(nameof(command), $"{nameof(command.RoomTypeRequest)} is required.");
        }

        var validationResult = await _validator.ValidateAsync(command.RoomTypeRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error during validation: {ValidationError}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var roomType = RoomType.CreateRoomType(
            command.RoomTypeRequest.Type,
            command.RoomTypeRequest.Capacity,
            command.RoomTypeRequest.Description,
            command.RoomTypeRequest.RoomPrice,
            command.RoomTypeRequest.HotelId);

        roomType.CreateRooms(
            command.RoomTypeRequest.NumberOfRooms,
            command.RoomTypeRequest.RoomNumberStartFrom,
            roomType.Id,
            roomType.Type,
            command.RoomTypeRequest.HotelId);

        var added = await _unitOfWork.RoomTypes.Add(roomType, cancellationToken);

        await _unitOfWork.Rooms.AddMany(roomType.Rooms, cancellationToken);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<GetRoomTypeResponse>(added);
    }
}