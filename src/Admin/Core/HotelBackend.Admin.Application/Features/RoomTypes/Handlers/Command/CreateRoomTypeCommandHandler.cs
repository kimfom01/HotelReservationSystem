using AutoMapper;
using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.RoomTypes;
using HotelBackend.Admin.Application.Features.RoomTypes.Requests.Command;
using HotelBackend.Admin.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using CreateRoomTypeRequest = HotelBackend.Admin.Application.Dtos.RoomTypes.CreateRoomTypeRequest;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Handlers.Command;

public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, GetRoomTypeResponse>
{
    private readonly ILogger<CreateRoomTypeCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateRoomTypeRequest> _validator;

    public CreateRoomTypeCommandHandler(
        ILogger<CreateRoomTypeCommandHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork,
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

        var added = await _unitOfWork.RoomTypes.Add(roomType);
        await _unitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<GetRoomTypeResponse>(added);
    }
}