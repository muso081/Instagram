using FluentValidation;
using Instagram.Application.DTOs.UserDtos;
using System.Text.RegularExpressions;

namespace Instagram.Application.Validators;

public class UserValidators : AbstractValidator<CreateUserDto>
{
    public UserValidators()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Must(UserNameCheck)
            .Length(1, 20).WithMessage("Username must be between 1 and 20 characters long.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Must(EmailCheck)
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Must(PasswordCheck)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
    private bool EmailCheck(string email)
    {
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        var isValid = Regex.IsMatch(email, pattern);

        return isValid;
    }

    private bool PasswordCheck(string password)
    {
        var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,}$";

        var isValid = Regex.IsMatch(password, pattern);

        return isValid;
    }

    private bool UserNameCheck(string userName)
    {
        var pattern = @"^[a-zA-Z0-9_]{3,20}$";

        var isValid = Regex.IsMatch(userName, pattern);

        return isValid;
    }
}
