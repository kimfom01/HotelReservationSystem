using AutoMapper;
using HotelBackend.Reservations.Application.Contracts.Database;
using HotelBackend.Reservations.Application.Dtos.Admin.Employees;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Admin.Employees.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Admin.Employees.Handlers.Queries;

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