using AutoMapper;
using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Rooms;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Rooms.Handlers.Commands;

public class UpdateRoomAvailabilityRequestHandler : IRequestHandler<UpdateRoomAvailabilityRequest, Unit>
{
    private readonly ILogger<UpdateRoomAvailabilityRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateRoomAvailabilityDto> _validator;

    public UpdateRoomAvailabilityRequestHandler(
        ILogger<UpdateRoomAvailabilityRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IValidator<UpdateRoomAvailabilityDto> validator)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Unit> Handle(UpdateRoomAvailabilityRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating room as unavailable");

        if (request.RoomDto is null)
        {
            _logger.LogError("{RoomDto} is required", nameof(request.RoomDto));
            throw new ArgumentNullException(nameof(request), $"{nameof(request.RoomDto)} is required");
        }

        var validationResult = await _validator.ValidateAsync(request.RoomDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error validating create room request: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var room = await _unitOfWork.Rooms.GetEntity(rom =>
            rom.Id == request.RoomDto.RoomId && rom.HotelId == request.RoomDto.HotelId);

        _mapper.Map(request.RoomDto, room);
        await _unitOfWork.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}