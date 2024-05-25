using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Authentication;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Employees;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Employees.Requests.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Employees.Handlers.Commands;

public class LoginEmployeeRequestHandler : IRequestHandler<LoginEmployeeRequest, EmployeeLoginResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LoginEmployeeRequestHandler> _logger;
    private readonly IValidator<EmployeeLoginDto> _validator;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordManager _passwordManager;

    public LoginEmployeeRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<LoginEmployeeRequestHandler> logger,
        IValidator<EmployeeLoginDto> validator,
        IJwtProvider jwtProvider,
        IPasswordManager passwordManager)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
        _jwtProvider = jwtProvider;
        _passwordManager = passwordManager;
    }

    public async Task<EmployeeLoginResponseDto> Handle(
        LoginEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login in user");
        if (request.LoginDto is null)
        {
            throw new ArgumentNullException(nameof(request), "LoginDto is required.");
        }

        var validationResult = await _validator.ValidateAsync(request.LoginDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var employee = await _unitOfWork.Employees.GetEntity(emp => emp.Email == request.LoginDto.Email);

        if (employee is null)
        {
            _logger.LogError("User with email={Email} does not exist", request.LoginDto.Email);
            throw new NotFoundException("Username or password is incorrect");
        }

        if (!_passwordManager.VerifyPassword(request.LoginDto.Password, employee.Password))
        {
            _logger.LogError("Invalid password was used to login to user={Email}", request.LoginDto.Email);
            throw new NotFoundException("Username or password is incorrect");
        }

        var token = _jwtProvider.Generate(employee);

        return new EmployeeLoginResponseDto
        {
            Token = token
        };
    }
}