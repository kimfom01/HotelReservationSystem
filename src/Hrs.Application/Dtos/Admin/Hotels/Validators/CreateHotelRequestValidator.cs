using FluentValidation;

namespace Hrs.Application.Dtos.Admin.Hotels.Validators;

public class CreateHotelRequestValidator : AbstractValidator<CreateHotelRequest>
{
    public CreateHotelRequestValidator()
    {
        RuleFor(cr => cr.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
        
        RuleFor(cr => cr.Location)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
}