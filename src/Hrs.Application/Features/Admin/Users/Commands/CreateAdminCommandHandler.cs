using AutoMapper;
using FluentValidation;
using Hrs.Application.Contracts.Authentication;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.Users;
using Hrs.Application.Exceptions;
using Hrs.Domain.Entities.Admin;
using Hrs.Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Users.Commands;

public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, GetUserResponse>
{
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly ILogger<CreateAdminCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateUserRequest> _validator;
    private readonly IPasswordManager _passwordManager;

    public CreateAdminCommandHandler(
        IAdminUnitOfWork unitOfWork,
        ILogger<CreateAdminCommandHandler> logger,
        IMapper mapper,
        IValidator<CreateUserRequest> validator,
        IPasswordManager passwordManager)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _passwordManager = passwordManager;
    }

    public async Task<GetUserResponse> Handle(
        CreateAdminCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering user");
        if (command.UserRequest is null)
        {
            _logger.LogError("{UserDto} is null", nameof(command.UserRequest));
            throw new ArgumentNullException(nameof(command), $"{command.UserRequest} is required");
        }

        await _validator.ValidateAndThrowAsync(command.UserRequest, cancellationToken);

        var userExists = await _unitOfWork.Users.CheckIfUserExists(command.UserRequest.Email, cancellationToken);

        if (userExists)
        {
            _logger.LogError("User with email={EmailAddress} already exists", command.UserRequest.Email);
            throw new UserExistsException($"User with email={command.UserRequest.Email} already exists");
        }

        var role = await _unitOfWork.Roles.GetAdminRole(cancellationToken);

        var hash = _passwordManager.HashPassword(command.UserRequest.Password);

        var user = User.CreateUser(
            command.UserRequest.FirstName,
            command.UserRequest.LastName,
            command.UserRequest.Email,
            hash);

        var userRole = new UserRole(user.Id, role!.Id);

        var added = await _unitOfWork.Users.Add(user, cancellationToken);
        await _unitOfWork.UserRoles.Add(userRole, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<GetUserResponse>(added);
    }
}