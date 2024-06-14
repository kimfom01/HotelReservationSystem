using FluentValidation;

namespace Hrs.Application.Dtos.GuestProfiles.Validators;

public class CreateGuestProfileRequestValidator : AbstractValidator<CreateGuestProfileRequest>
{
    public CreateGuestProfileRequestValidator()
    {
        RuleFor(dto => dto.FirstName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(dto => dto.LastName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(dto => dto.ContactEmail)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("{PropertyName} is required and must be a valid email address.");
    }
}