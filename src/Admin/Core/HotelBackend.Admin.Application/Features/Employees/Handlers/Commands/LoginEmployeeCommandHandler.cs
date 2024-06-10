using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Authentication;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Employees;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Employees.Requests.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Employees.Handlers.Commands;

public class LoginEmployeeCommandHandler : IRequestHandler<LoginEmployeeCommand, LoginEmployeeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LoginEmployeeCommandHandler> _logger;
    private readonly IValidator<LoginEmployeeRequest> _validator;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordManager _passwordManager;

    public LoginEmployeeCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<LoginEmployeeCommandHandler> logger,
        IValidator<LoginEmployeeRequest> validator,
        IJwtProvider jwtProvider,
        IPasswordManager passwordManager)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
        _jwtProvider = jwtProvider;
        _passwordManager = passwordManager;
    }

    public async Task<LoginEmployeeResponse> Handle(
        LoginEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login in user");
        if (command.LoginEmployee is null)
        {
            throw new ArgumentNullException(nameof(command), "LoginDto is required.");
        }

        await _validator.ValidateAndThrowAsync(command.LoginEmployee, cancellationToken);

        var employee = await _unitOfWork.Employees.GetEntity(emp => emp.Email == command.LoginEmployee.Email);

        if (employee is null)
        {
            _logger.LogError("User with email={Email} does not exist", command.LoginEmployee.Email);
            throw new NotFoundException("Username or password is incorrect");
        }

        if (!_passwordManager.VerifyPassword(command.LoginEmployee.Password, employee.Password))
        {
            _logger.LogError("Invalid password was used to login to user={Email}", command.LoginEmployee.Email);
            throw new NotFoundException("Username or password is incorrect");
        }

        var token = _jwtProvider.Generate(employee);

        return new LoginEmployeeResponse
        {
            Token = token,
            TokenType = "Bearer"
        };
    }
}