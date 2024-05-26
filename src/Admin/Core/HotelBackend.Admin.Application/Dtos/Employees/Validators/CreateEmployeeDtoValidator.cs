using FluentValidation;

namespace HotelBackend.Admin.Application.Dtos.Employees.Validators;

public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeDtoValidator()
    {
        RuleFor(emp => emp.FirstName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

        RuleFor(emp => emp.LastName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");

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

        RuleFor(emp => emp.ConfirmPassword)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Must((emp, x) => x == emp.Password);
    }
}