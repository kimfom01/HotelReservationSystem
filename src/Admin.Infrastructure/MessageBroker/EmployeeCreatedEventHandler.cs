using System.Text;
using Admin.Application.Contracts.Database;
using Admin.Application.Contracts.Email;
using Admin.Application.Contracts.MessageBroker;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Admin.Infrastructure.MessageBroker;

public class EmployeeCreatedEventHandler : IConsumer<EmployeeCreatedEvent>
{
    private readonly ILogger<EmployeeCreatedEventHandler> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IAdminUnitOfWork _unitOfWork;

    public EmployeeCreatedEventHandler(
        ILogger<EmployeeCreatedEventHandler> logger,
        IEmailSender emailSender,
        IAdminUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _emailSender = emailSender;
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<EmployeeCreatedEvent> context)
    {
        _logger.LogInformation("Consuming {EventName} event", nameof(EmployeeCreatedEvent));

        var hotel = await _unitOfWork.Hotels.GetEntity(hotel => hotel.Id == context.Message.HotelId,
            context.CancellationToken);

        if (hotel is null)
        {
            _logger.LogError("Hotel assigned to employee {EmployeeEmail} does not exist", context.Message.Email);
            return;
        }

        var emailTemplate = new StringBuilder()
            .Append("<h3>Hello @Model.ReceiverName,</h3>")
            .Append("<div>Your account was successfully created</div>")
            .Append("<div>and you were assigned to @Model.HotelName</div>")
            .Append("<div>Here are your login credentials</div>")
            .Append("<div>Email: @Model.Email</div>")
            .Append("<div>Password: @Model.Password</div>")
            .Append("<div>Regards, Hotel Reservation Team</div>")
            .ToString();

        var email = new EmailMessage
        {
            ReceiverName = context.Message.FullName,
            ReceiverEmail = context.Message.Email,
            Template = emailTemplate,
            Subject = "Your account was successfully created"
        };

        await _emailSender.SendEmailAsync(email, new
        {
            ReceiverName = context.Message.FullName,
            HotelName = hotel.Name,
            Email = email.ReceiverEmail,
            Password = context.Message.Password
        }, context.CancellationToken);
    }
}