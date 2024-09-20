using AutoMapper;
using FluentValidation;
using Hrs.Application.Contracts.Authentication;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Contracts.MessageBroker;
using Hrs.Application.Dtos.Admin.Users;
using Hrs.Application.Exceptions;
using Hrs.Domain.Entities.Admin;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Users.Commands;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, GetUserResponse>
{
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPasswordManager _passwordManager;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateEmployeeCommandHandler(
        IAdminUnitOfWork unitOfWork,
        ILogger<CreateEmployeeCommandHandler> logger,
        IMapper mapper,
        IPasswordManager passwordManager,
        IPublishEndpoint publishEndpoint
    )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _passwordManager = passwordManager;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<GetUserResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering employee");

        if (request.Password != request.ConfirmPassword)
        {
            _logger.LogInformation("Password and Confirm Password are does not match");
            throw new ValidationException("Password and Confirm Password are does not match");
        }

        var hotel = await _unitOfWork.Hotels.GetEntity(hotel => hotel.Id == request.HotelId, cancellationToken);

        if (hotel is null)
        {
            _logger.LogError("Hotel with id={HotelId} does not exist", request.HotelId);
            throw new ValidationException($"Hotel with id={request.HotelId} does not exist");
        }

        var userExists = await _unitOfWork.Users.CheckIfUserExists(request.Email, cancellationToken);

        if (userExists)
        {
            _logger.LogError("User with email={EmailAddress} already exists", request.Email);
            throw new UserExistsException($"User with email={request.Email} already exists");
        }

        var hash = _passwordManager.HashPassword(request.Password);

        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            hash
        );

        user.AssignToHotel(request.HotelId);

        var roles = _unitOfWork.Roles.GetEmployeeRoles(cancellationToken);
        var employeeRoles = roles.Select(role => new UserRole(user.Id, role.Id));

        var added = await _unitOfWork.Users.Add(user, cancellationToken);
        await _unitOfWork.UserRoles.AddMany(employeeRoles, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        await _publishEndpoint.Publish(new EmployeeCreatedEvent(
            string.Join(" ", request.FirstName, request.LastName),
            request.Email,
            request.Password,
            request.HotelId
        ), cancellationToken);

        return _mapper.Map<GetUserResponse>(added);
    }
}