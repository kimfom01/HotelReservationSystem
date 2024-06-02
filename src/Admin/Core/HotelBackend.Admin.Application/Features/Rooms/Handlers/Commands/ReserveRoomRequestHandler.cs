using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Rooms;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Rooms.Handlers.Commands;

public class ReserveRoomRequestHandler : IRequestHandler<ReserveRoomRequest, ReserveRoomResponse>
{
    private readonly ILogger<ReserveRoomRequestHandler> _logger;
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ReserveRoomRequestDto> _validator;

    public ReserveRoomRequestHandler(
        ILogger<ReserveRoomRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<ReserveRoomRequestDto> validator)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<ReserveRoomResponse> Handle(ReserveRoomRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Placing a room on hold");

        if (request.RoomRequestDto is null)
        {
            _logger.LogError("{RoomDto} is required", nameof(request.RoomRequestDto));
            throw new ArgumentNullException(nameof(request), $"{nameof(request.RoomRequestDto)} is required");
        }

        var validationResult = await _validator.ValidateAsync(request.RoomRequestDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error validating create room request: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var room = await _unitOfWork.Rooms
            .GetRoomOfType(request.RoomRequestDto.HotelId,
                request.RoomRequestDto.RoomTypeId);

        if (room is null)
        {
            _logger.LogError("No available room of type={RoomTypeId} in hotel={HotelId}",
                request.RoomRequestDto.RoomTypeId, request.RoomRequestDto.HotelId);
            throw new NotFoundException(
                $"No available room of type={request.RoomRequestDto.RoomTypeId} in hotel={request.RoomRequestDto.HotelId}");
        }

        room.Availability = false;

        await _unitOfWork.SaveChanges(cancellationToken);

        return new ReserveRoomResponse
        {
            RoomId = room.Id
        };
    }
}