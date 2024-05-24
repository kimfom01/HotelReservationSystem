using AutoMapper;
using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.RoomTypes;
using HotelBackend.Admin.Application.Features.RoomTypes.Requests.Command;
using HotelBackend.Admin.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Handlers.Command;

public class CreateRoomTypeRequestHandler : IRequestHandler<CreateRoomTypeRequest, GetRoomTypeDto>
{
    private readonly ILogger<CreateRoomTypeRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateRoomTypeDto> _validator;

    public CreateRoomTypeRequestHandler(
        ILogger<CreateRoomTypeRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IValidator<CreateRoomTypeDto> validator)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    
    public async Task<GetRoomTypeDto> Handle(CreateRoomTypeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating room type");
        if (request.RoomTypeDto is null)
        {
            _logger.LogError("Error: RoomTypeDto is null");
            throw new ArgumentNullException(nameof(request), "RoomTypeDto is required.");
        }

        var validationResult = await _validator.ValidateAsync(request.RoomTypeDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error during validation: {ValidationError}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var roomType = _mapper.Map<RoomType>(request.RoomTypeDto);

        var added = await _unitOfWork.RoomTypes.Add(roomType);
        await _unitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<GetRoomTypeDto>(added);
    }
}