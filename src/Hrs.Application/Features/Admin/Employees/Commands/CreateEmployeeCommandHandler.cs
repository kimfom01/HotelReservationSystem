using AutoMapper;
using FluentValidation;
using Hrs.Application.Contracts.Authentication;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.Employees;
using Hrs.Application.Exceptions;
using Hrs.Common;
using Hrs.Domain.Entities.Admin;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Employees.Commands;

public class
    CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, GetEmployeeResponse>
{
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateEmployeeRequest> _validator;
    private readonly IPasswordManager _passwordManager;

    public CreateEmployeeCommandHandler(
        IAdminUnitOfWork unitOfWork,
        ILogger<CreateEmployeeCommandHandler> logger,
        IMapper mapper,
        IValidator<CreateEmployeeRequest> validator,
        IPasswordManager passwordManager)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _passwordManager = passwordManager;
    }

    public async Task<GetEmployeeResponse> Handle(CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering employee");
        if (command.EmployeeRequest is null)
        {
            _logger.LogError("{EmployeeDto} is null", nameof(command.EmployeeRequest));
            throw new ArgumentNullException(nameof(command), $"{command.EmployeeRequest} is required");
        }

        await _validator.ValidateAndThrowAsync(command.EmployeeRequest, cancellationToken);

        var employeeExists = await _unitOfWork.Employees.CheckIfEmployeeExists(command.EmployeeRequest.Email);

        if (employeeExists)
        {
            _logger.LogError("Employee with email={EmailAddress} already exists", command.EmployeeRequest.Email);
            throw new EmployeeExistsException($"Employee with email={command.EmployeeRequest.Email} already exists");
        }

        var hash = _passwordManager.HashPassword(command.EmployeeRequest.Password);

        var employee = Employee.CreateEmployee(
            command.EmployeeRequest.FirstName,
            command.EmployeeRequest.LastName,
            command.EmployeeRequest.Email,
            command.EmployeeRequest.Role,
            hash);

        var added = await _unitOfWork.Employees.Add(employee, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<GetEmployeeResponse>(added);
    }
}