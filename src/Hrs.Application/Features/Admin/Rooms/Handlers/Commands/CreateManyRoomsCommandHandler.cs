using FluentValidation;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.Rooms;
using Hrs.Application.Features.Admin.Rooms.Requests.Commands;
using Hrs.Domain.Entities.Admin;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Rooms.Handlers.Commands;

public class CreateManyRoomsCommandHandler : IRequestHandler<CreateManyRoomsCommand, int>
{
    private readonly ILogger<CreateManyRoomsCommandHandler> _logger;
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly IValidator<CreateManyRoomsRequest> _validator;

    public CreateManyRoomsCommandHandler(
        IAdminUnitOfWork unitOfWork,
        ILogger<CreateManyRoomsCommandHandler> logger,
        IValidator<CreateManyRoomsRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }

    public async Task<int> Handle(CreateManyRoomsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating multiple rooms of type={RoomTypeId}", request.RoomsRequest.RoomTypeId);

        await _validator.ValidateAndThrowAsync(request.RoomsRequest, cancellationToken);

        List<Room> rooms = new(request.RoomsRequest.Count);

        for (int i = request.RoomsRequest.Start; i < request.RoomsRequest.Start + request.RoomsRequest.Count; i++)
        {
            rooms.Add(new Room
            {
                RoomNumber = $"{i}",
                HotelId = request.RoomsRequest.HotelId,
                RoomTypeId = request.RoomsRequest.RoomTypeId
            });
        }

        await _unitOfWork.Rooms.AddMany(rooms, cancellationToken);
        var count = await _unitOfWork.SaveChanges(cancellationToken);

        return count;
    }
}