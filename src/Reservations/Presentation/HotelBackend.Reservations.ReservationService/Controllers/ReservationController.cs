using System.Net;
using AutoMapper;
using FluentValidation;
using HotelBackend.Reservations.Application.Dtos.Reservations;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Reservations.Requests.Commands;
using HotelBackend.Reservations.Application.Features.Reservations.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Reservations.ReservationService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReservationController> _logger;

    public ReservationController(
        IMediator mediator,
        IMapper mapper,
        ILogger<ReservationController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetReservation(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var reservation = await _mediator.Send(new GetReservationByIdRequest
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
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetReservations(CancellationToken cancellationToken)
    {
        var reservations = await _mediator.Send(new GetAllReservationsRequest(), cancellationToken);

        return Ok(reservations);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> PostReservation(CreateReservationDto createReservationDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var added = await _mediator.Send(new CreateReservationRequest
            {
                CreateReservationDto = createReservationDto
            }, cancellationToken);

            _logger.LogInformation("Successfully made reservation with id={reservationId}", added.Id);
            return CreatedAtAction(nameof(GetReservation), new { id = added.Id }, added);
        }
        catch (ValidationException validationException)
        {
            _logger.LogError("{Exception} occured: {exceptionMessage}", nameof(ValidationException),
                validationException.Message);
            return BadRequest(validationException.Errors);
        }
        catch (NotFoundException notFoundException)
        {
            _logger.LogError("{Exception} occured: {exceptionMessage}", nameof(NotFoundException),
                notFoundException.Message);
            return BadRequest(notFoundException.Message);
        }
        catch (NotAvailableException notAvailableException)
        {
            _logger.LogError("{Exception} occured: {exceptionMessage}", nameof(NotAvailableException),
                notAvailableException.Message);
            return BadRequest(notAvailableException.Message);
        }
        catch (ReservationException reservationException)
        {
            _logger.LogError("{Exception} occured: {exceptionMessage}", nameof(ReservationException),
                reservationException.Message);
            return BadRequest(reservationException.Message);
        }
    }
}