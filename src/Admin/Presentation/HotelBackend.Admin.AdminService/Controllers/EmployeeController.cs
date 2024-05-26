using System.Net;
using FluentValidation;
using HotelBackend.Admin.Application.Dtos.Employees;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Employees.Requests.Commands;
using HotelBackend.Admin.Application.Features.Employees.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Admin.AdminService.Controllers;

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
    public async Task<ActionResult<EmployeeLoginResponseDto>> LoginEmployee(EmployeeLoginDto employeeLogin,
        CancellationToken cancellationToken)
    {
        try
        {
            var loginResponse = await _mediator.Send(new LoginEmployeeRequest
            {
                LoginDto = employeeLogin
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
    public async Task<ActionResult<GetEmployeeDto>> RegisterEmployee(CreateEmployeeDto employeeDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var employee = await _mediator.Send(new CreateEmployeeRequest
            {
                EmployeeDto = employeeDto
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
    public async Task<ActionResult<GetEmployeeDto>> GetEmployee(CancellationToken cancellationToken)
    {
        try
        {
            var employeeId = new Guid(User.Claims.FirstOrDefault(cl => cl.Type == "Id")?.Value ?? string.Empty);

            var employee = await _mediator.Send(new GetEmployeeRequest
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