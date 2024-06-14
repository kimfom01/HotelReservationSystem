using System.Net;
using FluentValidation;
using HotelBackend.Reservations.Application.Dtos.Admin.Employees;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Admin.Employees.Requests.Commands;
using HotelBackend.Reservations.Application.Features.Admin.Employees.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Reservations.ReservationService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<LoginEmployeeResponse>> LoginEmployee(LoginEmployeeRequest loginEmployee,
        CancellationToken cancellationToken)
    {
        try
        {
            var loginResponse = await _mediator.Send(new LoginEmployeeCommand
            {
                LoginEmployee = loginEmployee
            }, cancellationToken);

            return Ok(loginResponse);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("register")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetEmployeeResponse>> RegisterEmployee(CreateEmployeeRequest employeeRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var employee = await _mediator.Send(new CreateEmployeeCommand
            {
                EmployeeRequest = employeeRequest
            }, cancellationToken);

            return CreatedAtAction(nameof(GetEmployee), new { employeeId = employee.Id, hotelId = employee.HotelId },
                employee);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GetEmployeeResponse>> GetEmployee(CancellationToken cancellationToken)
    {
        try
        {
            var employeeId = new Guid(User.Claims.FirstOrDefault(cl => cl.Type == "Id")?.Value ?? string.Empty);

            var employee = await _mediator.Send(new GetEmployeeQuery
            {
                EmployeeId = employeeId,
            }, cancellationToken);

            return Ok(employee);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}