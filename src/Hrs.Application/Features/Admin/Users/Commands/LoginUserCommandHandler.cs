using FluentValidation;
using Hrs.Application.Contracts.Authentication;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.Users;
using Hrs.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Users.Commands;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly ILogger<LoginUserCommandHandler> _logger;
    private readonly IValidator<LoginUserRequest> _validator;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordManager _passwordManager;

    public LoginUserCommandHandler(
        IAdminUnitOfWork unitOfWork,
        ILogger<LoginUserCommandHandler> logger,
        IValidator<LoginUserRequest> validator,
        IJwtProvider jwtProvider,
        IPasswordManager passwordManager)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
        _jwtProvider = jwtProvider;
        _passwordManager = passwordManager;
    }

    public async Task<LoginUserResponse> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login in user");
        if (command.LoginUser is null)
        {
            throw new ArgumentNullException(nameof(command), "LoginDto is required.");
        }

        await _validator.ValidateAndThrowAsync(command.LoginUser, cancellationToken);

        var user = await _unitOfWork.Users.GetUser(command.LoginUser.Email, cancellationToken);

        if (user is null)
        {
            _logger.LogError("User with email={Email} does not exist", command.LoginUser.Email);
            throw new NotFoundException("Username or password is incorrect");
        }

        if (!_passwordManager.VerifyPassword(command.LoginUser.Password, user.Password))
        {
            _logger.LogError("Invalid password was used to login to user={Email}", command.LoginUser.Email);
            throw new NotFoundException("Username or password is incorrect");
        }

        var token = _jwtProvider.Generate(user);

        return new LoginUserResponse
        {
            Token = token,
            TokenType = "Bearer"
        };
    }
}