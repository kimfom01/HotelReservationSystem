using AutoMapper;
using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Authentication;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Employees;
using HotelBackend.Admin.Domain.Entities;
using HotelBackend.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using CreateEmployeeRequest = HotelBackend.Admin.Application.Dtos.Employees.CreateEmployeeRequest;

namespace HotelBackend.Admin.Application.Features.Employees.Handlers.Commands;

public class
    CreateEmployeeCommandHandler : IRequestHandler<Requests.Commands.CreateEmployeeCommand, GetEmployeeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateEmployeeCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateEmployeeRequest> _validator;
    private readonly IPasswordManager _passwordManager;

    public CreateEmployeeCommandHandler(
        IUnitOfWork unitOfWork,
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

    public async Task<GetEmployeeResponse> Handle(Requests.Commands.CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering employee");
        if (command.EmployeeRequest is null)
        {
            _logger.LogError("{EmployeeDto} is null", nameof(command.EmployeeRequest));
            throw new ArgumentNullException(nameof(command), $"{command.EmployeeRequest} is required");
        }

        await _validator.ValidateAndThrowAsync(command.EmployeeRequest, cancellationToken);

        var hash = _passwordManager.HashPassword(command.EmployeeRequest.Password);

        var employee = _mapper.Map<Employee>(command.EmployeeRequest);

        employee.Password = hash;

        if (command.EmployeeRequest.Role is null)
        {
            employee.Role = Roles.Admin;
        }

        var added = await _unitOfWork.Employees.Add(employee);
        await _unitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<GetEmployeeResponse>(added);
    }
}