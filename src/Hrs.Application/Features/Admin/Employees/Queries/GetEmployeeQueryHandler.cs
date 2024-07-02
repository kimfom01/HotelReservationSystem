using AutoMapper;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.Employees;
using Hrs.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Employees.Queries;

public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, GetEmployeeResponse>
{
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetEmployeeQueryHandler> _logger;

    public GetEmployeeQueryHandler(
        IAdminUnitOfWork unitOfWork,
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

        var employee = await _unitOfWork.Employees.GetEntity(emp => emp.Id == query.EmployeeId, cancellationToken);

        if (employee is null)
        {
            _logger.LogError("Employee with id={EmployeeId} does not exist", query.EmployeeId);
            throw new NotFoundException($"Employee with id={query.EmployeeId} does not exist");
        }

        return _mapper.Map<GetEmployeeResponse>(employee);
    }
}