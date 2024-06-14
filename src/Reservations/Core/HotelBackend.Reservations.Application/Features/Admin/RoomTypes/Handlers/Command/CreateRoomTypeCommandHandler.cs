using AutoMapper;
using FluentValidation;
using HotelBackend.Reservations.Application.Contracts.Database;
using HotelBackend.Reservations.Application.Dtos.Admin.RoomTypes;
using HotelBackend.Reservations.Application.Features.Admin.RoomTypes.Requests.Command;
using HotelBackend.Reservations.Domain.Entities.Admin;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Admin.RoomTypes.Handlers.Command;

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

        var roomType = _mapper.Map<RoomType>(command.RoomTypeRequest);

        var added = await _unitOfWork.RoomTypes.Add(roomType, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<GetRoomTypeResponse>(added);
    }
}