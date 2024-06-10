using AutoMapper;
using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Hotels;
using HotelBackend.Admin.Application.Features.Hotels.Requests.Commands;
using HotelBackend.Admin.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using CreateHotelRequest = HotelBackend.Admin.Application.Dtos.Hotels.CreateHotelRequest;

namespace HotelBackend.Admin.Application.Features.Hotels.Handlers.Commands;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, GetHotelResponse>
{
    private readonly ILogger<CreateHotelCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateHotelRequest> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateHotelCommandHandler(
        ILogger<CreateHotelCommandHandler> logger,
        IMapper mapper,
        IValidator<CreateHotelRequest> validator,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetHotelResponse> Handle(CreateHotelCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new hotel");

        if (command.HotelRequest is null)
        {
            _logger.LogError("Error: {HotelDto} is null", nameof(command.HotelRequest));
            throw new ArgumentNullException(nameof(command), $"{nameof(command.HotelRequest)} is required.");
        }

        var validationResult = await _validator.ValidateAsync(command.HotelRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error during validation: {ValidationError}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var hotel = _mapper.Map<Hotel>(command.HotelRequest);
        hotel.AdminId = command.AdminId;

        var added = await _unitOfWork.Hotels.Add(hotel);
        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully created a hotel");

        return _mapper.Map<GetHotelResponse>(added);
    }
}