using FluentValidation;

namespace Admin.Application.Dtos.Admin.Users.Validators;

public class UserLoginRequestValidator : AbstractValidator<LoginUserRequest>
{
    public UserLoginRequestValidator()
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