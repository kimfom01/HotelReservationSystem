using System.Net;
using Asp.Versioning;
using FluentValidation;
using Hrs.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReservationService.Application.Dtos.Reservations;
using ReservationService.Application.Features.Reservations.Commands;
using ReservationService.Application.Features.Reservations.Queries;

namespace ReservationService.Presentation.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("/api/v{v:apiVersion}/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:Guid}")]
    [MapToApiVersion(1)]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservation(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var reservation = await _mediator.Send(new GetReservationByIdQuery
            {
                ReservationId = id
            }, cancellationToken);

            return Ok(reservation);
        }
        catch (Exception exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpGet]
    [MapToApiVersion(1)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetReservations(CancellationToken cancellationToken)
    {
        var reservations = await _mediator.Send(new GetAllReservationsQuery(), cancellationToken);

        return Ok(reservations);
    }

    [HttpPost]
    [MapToApiVersion(1)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> PostReservation(CreateReservationRequest createReservationRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var added = await _mediator.Send(new CreateReservationCommand
            {
                CreateReservationDto = createReservationRequest
            }, cancellationToken);

            return CreatedAtAction(nameof(GetReservation), new { id = added.Id }, added);
        }
        catch (ValidationException validationException)
        {
            return BadRequest(validationException.Errors);
        }
        catch (NotFoundException notFoundException)
        {
            return BadRequest(notFoundException.Message);
        }
        catch (ReservationException reservationException)
        {
            return BadRequest(reservationException.Message);
        }
    }
}