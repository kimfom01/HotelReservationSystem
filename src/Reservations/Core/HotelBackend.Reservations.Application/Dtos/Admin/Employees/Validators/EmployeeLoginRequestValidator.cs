using FluentValidation;

namespace HotelBackend.Reservations.Application.Dtos.Admin.Employees.Validators;

public class EmployeeLoginRequestValidator : AbstractValidator<LoginEmployeeRequest>
{
    public EmployeeLoginRequestValidator()
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