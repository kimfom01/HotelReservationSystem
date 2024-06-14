using AutoMapper;
using FluentValidation;
using HotelBackend.Reservations.Application.Contracts.Infrastructure.Database;
using HotelBackend.Reservations.Application.Dtos.Hotels;
using HotelBackend.Reservations.Application.Features.Admin.Hotels.Requests.Commands;
using HotelBackend.Reservations.Domain.Entities.Admin;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Admin.Hotels.Handlers.Commands;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, GetHotelResponse>
{
    private readonly ILogger<CreateHotelCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateHotelRequest> _validator;
    private readonly IAdminUnitOfWork _unitOfWork;

    public CreateHotelCommandHandler(
        ILogger<CreateHotelCommandHandler> logger,
        IMapper mapper,
        IValidator<CreateHotelRequest> validator,
        IAdminUnitOfWork unitOfWork)
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

        var added = await _unitOfWork.Hotels.Add(hotel, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Successfully created a hotel");

        return _mapper.Map<GetHotelResponse>(added);
    }
}