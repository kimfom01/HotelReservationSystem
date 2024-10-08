using System.Net;
using FluentValidation;
using Admin.Application.Dtos.Admin.Users;
using Admin.Application.Features.Admin.Users.Commands;
using Admin.Application.Features.Admin.Users.Queries;
using Asp.Versioning;
using Hrs.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Presentation.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("/api/v{v:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<LoginUserResponse>> LoginUser(LoginUserRequest loginUser,
        CancellationToken cancellationToken)
    {
        try
        {
            var loginResponse = await _mediator.Send(new LoginUserCommand
            {
                LoginUser = loginUser
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

    [HttpPost("admin/register")]
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetUserResponse>> RegisterAdmin(CreateUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _mediator.Send(new CreateAdminCommand
            {
                UserRequest = userRequest
            }, cancellationToken);

            return CreatedAtAction(nameof(GetUser), user);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UserExistsException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<ActionResult<GetUserResponse>> GetUser(CancellationToken cancellationToken)
    {
        try
        {
            var userId = new Guid(User.Claims.FirstOrDefault(cl => cl.Type == "Id")?.Value ??
                                  throw new NotFoundException("Invalid credentials"));

            var user = await _mediator.Send(new GetUserQuery
            {
                UserId = userId,
            }, cancellationToken);

            return Ok(user);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("employee/register")]
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<GetUserResponse>> CreateEmployee(CreateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _mediator.Send(new CreateEmployeeCommand
            {
                ConfirmPassword = request.ConfirmPassword,
                Email = request.Email,
                FirstName = request.FirstName,
                HotelId = request.HotelId,
                LastName = request.LastName,
                Password = request.Password
            }, cancellationToken);

            return CreatedAtAction($"{nameof(GetUser)}", user);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UserExistsException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}