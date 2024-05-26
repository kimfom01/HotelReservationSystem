using AutoMapper;
using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Hotels;
using HotelBackend.Admin.Application.Features.Hotels.Requests.Commands;
using HotelBackend.Admin.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Hotels.Handlers.Commands;

public class CreateHotelRequestHandler : IRequestHandler<CreateHotelRequest, GetHotelDto> 
{
    private readonly ILogger<CreateHotelRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateHotelDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateHotelRequestHandler(
        ILogger<CreateHotelRequestHandler> logger,
        IMapper mapper,
        IValidator<CreateHotelDto> validator,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetHotelDto> Handle(CreateHotelRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new hotel");

        if (request.HotelDto is null)
        {
            _logger.LogError("Error: HotelDto is null");
            throw new ArgumentNullException(nameof(request), "HotelDto is required.");
        }

        var validationResult = await _validator.ValidateAsync(request.HotelDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error during validation: {ValidationError}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var hotel = _mapper.Map<Hotel>(request.HotelDto);
        hotel.AdminId = request.AdminId;

        var added = await _unitOfWork.Hotels.Add(hotel);
        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully created a hotel");

        return _mapper.Map<GetHotelDto>(added);
    }
}