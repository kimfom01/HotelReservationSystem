using AutoMapper;
using FluentValidation;
using HotelBackend.Admin.Application.Contracts.Authentication;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Employees;
using HotelBackend.Admin.Application.Features.Employees.Requests.Commands;
using HotelBackend.Admin.Domain.Entities;
using HotelBackend.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Employees.Handlers.Commands;

public class CreateEmployeeRequestHandler : IRequestHandler<CreateEmployeeRequest, GetEmployeeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateEmployeeRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateEmployeeDto> _validator;
    private readonly IPasswordManager _passwordManager;

    public CreateEmployeeRequestHandler(
        IUnitOfWork unitOfWork,
        ILogger<CreateEmployeeRequestHandler> logger,
        IMapper mapper,
        IValidator<CreateEmployeeDto> validator,
        IPasswordManager passwordManager)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _passwordManager = passwordManager;
    }

    public async Task<GetEmployeeDto> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering employee");
        if (request.EmployeeDto is null)
        {
            _logger.LogError("{EmployeeDto} is null", nameof(request.EmployeeDto));
            throw new ArgumentNullException(nameof(request), $"{request.EmployeeDto} is required");
        }

        var validationResult = await _validator.ValidateAsync(request.EmployeeDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            _logger.LogError("Error validating registration request: {Errors}", validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }

        var hash = _passwordManager.HashPassword(request.EmployeeDto.Password);

        var employee = _mapper.Map<Employee>(request.EmployeeDto);

        employee.Password = hash;

        if (request.EmployeeDto.Role is null)
        {
            employee.Role = Roles.Admin;
        }

        var added = await _unitOfWork.Employees.Add(employee);
        await _unitOfWork.SaveChanges(cancellationToken);

        return _mapper.Map<GetEmployeeDto>(added);
    }
}