using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Employees;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Employees.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Employees.Handlers.Queries;

public class GetEmployeeRequestHandler : IRequestHandler<GetEmployeeRequest, GetEmployeeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetEmployeeRequestHandler> _logger;

    public GetEmployeeRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GetEmployeeRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetEmployeeDto> Handle(GetEmployeeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting employee with id={EmployeeId}", request.EmployeeId);

        var employee = await _unitOfWork.Employees.GetEntity(emp => emp.Id == request.EmployeeId);

        if (employee is null)
        {
            _logger.LogError("Employee with id={EmployeeId} does not exist", request.EmployeeId);
            throw new NotFoundException($"Employee with id={request.EmployeeId} does not exist");
        }

        return _mapper.Map<GetEmployeeDto>(employee);
    }
}