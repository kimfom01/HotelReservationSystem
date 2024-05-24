using FluentValidation;

namespace HotelBackend.Admin.Application.Dtos.Hotels.Validators;

public class CreateHotelDtoValidator : AbstractValidator<CreateHotelDto>
{
    public CreateHotelDtoValidator()
    {
        RuleFor(cr => cr.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
        
        RuleFor(cr => cr.Location)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
}