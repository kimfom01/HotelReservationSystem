using FluentValidation;

namespace PaymentService.Application.Dtos.Payments.Validators;

public class AddPaymentRequestValidator : AbstractValidator<AddPaymentRequest>
{
    public AddPaymentRequestValidator()
    {
        RuleFor(dto => dto.Amount)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}");

        RuleFor(dto => dto.Status)
            .IsInEnum()
            .WithMessage("Invalid payment status");

        RuleFor(dto => dto.ReservationId)
            .NotNull()
            .WithMessage("{PropertyName} is required.");
    }
}