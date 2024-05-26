using AutoMapper;
using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Rooms;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;
using HotelBackend.Admin.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Rooms.Handlers.Commands;

public class CreateRoomRequestHandler : IRequestHandler<CreateRoomRequest, GetRoomDto>
{
    private readonly IMapper _mapper;
    private readonly ILogger<CreateRoomRequestHandler> _logger;
    private readonly IValidator<CreateRoomDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomRequestHandler(
        IMapper mapper,
        ILogger<CreateRoomRequestHandler> logger,
        IValidator<CreateRoomDto> validator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomDto> Handle(CreateRoomRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a room");

        if (request.RoomDto is null)
        {
            _logger.LogError("Error: {RoomDto} is null", nameof(request.RoomDto));
            throw new ArgumentNullException(nameof(request), $"{nameof(request.RoomDto)} is required");
        }

        var validationResult = await _validator.ValidateAsync(request.RoomDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error validating create room request: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var room = _mapper.Map<Room>(request.RoomDto);

        var added = await _unitOfWork.Rooms.Add(room);
        await _unitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<GetRoomDto>(added);
    }
}