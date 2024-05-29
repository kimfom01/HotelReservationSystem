using FluentValidation;

namespace HotelBackend.Admin.Application.Dtos.Employees.Validators;

public class EmployeeLoginDtoValidator : AbstractValidator<EmployeeLoginDto>
{
    public EmployeeLoginDtoValidator()
    {
        RuleFor(emp => emp.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .EmailAddress()
            .WithMessage("{PropertyName} must be a valid email address.");

        RuleFor(emp => emp.Password)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .MinimumLength(5)
            .WithMessage("{PropertyValue} or more required for {PropertyName}.");
    }
}