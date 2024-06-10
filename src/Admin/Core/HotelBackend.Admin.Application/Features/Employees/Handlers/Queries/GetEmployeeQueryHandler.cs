using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Employees;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Employees.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Employees.Handlers.Queries;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, GetEmployeeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetEmployeeQueryHandler> _logger;

    public GetEmployeeQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GetEmployeeQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetEmployeeResponse> Handle(GetEmployeeQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting employee with id={EmployeeId}", query.EmployeeId);

        var employee = await _unitOfWork.Employees.GetEntity(emp => emp.Id == query.EmployeeId);

        if (employee is null)
        {
            _logger.LogError("Employee with id={EmployeeId} does not exist", query.EmployeeId);
            throw new NotFoundException($"Employee with id={query.EmployeeId} does not exist");
        }

        return _mapper.Map<GetEmployeeResponse>(employee);
    }
}