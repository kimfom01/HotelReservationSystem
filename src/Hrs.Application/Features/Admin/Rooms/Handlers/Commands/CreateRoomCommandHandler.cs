using AutoMapper;
using FluentValidation;
using Hrs.Domain.Entities.Admin;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.Rooms;
using Hrs.Application.Features.Admin.Rooms.Requests.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Rooms.Handlers.Commands;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, GetRoomResponse>
{
    private readonly IMapper _mapper;
    private readonly ILogger<CreateRoomCommandHandler> _logger;
    private readonly IValidator<CreateRoomRequest> _validator;
    private readonly IAdminUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(
        IMapper mapper,
        ILogger<CreateRoomCommandHandler> logger,
        IValidator<CreateRoomRequest> validator,
        IAdminUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomResponse> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a room");

        if (command.RoomRequest is null)
        {
            _logger.LogError("Error: {RoomDto} is null", nameof(command.RoomRequest));
            throw new ArgumentNullException(nameof(command), $"{nameof(command.RoomRequest)} is required");
        }

        var validationResult = await _validator.ValidateAsync(command.RoomRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error validating create room request: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var room = _mapper.Map<Room>(command.RoomRequest);

        var added = await _unitOfWork.Rooms.Add(room, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<GetRoomResponse>(added);
    }
}