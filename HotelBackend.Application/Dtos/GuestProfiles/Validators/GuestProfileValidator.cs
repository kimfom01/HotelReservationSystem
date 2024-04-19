using FluentValidation;

namespace HotelBackend.Application.Dtos.GuestProfiles.Validators;

public class GuestProfileValidator : AbstractValidator<CreateGuestProfileDto>
{
    public GuestProfileValidator()
    {
        RuleFor(dto => dto.FirstName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(dto => dto.LastName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
        
        RuleFor(dto => dto.ContactEmail)
            .NotEmpty()
            .EmailAddress();
    }
}