using Application.Models;
using FluentValidation;

namespace Application.Validators;
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        ValidateEmail();
        ValidateUsername();
        ValidateDateBirth();
    }


    private void ValidateEmail()
    {
        const int emailMinLength = 12;
        const int emailMaxLength = 32;

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email field cannot be empty")
            .MinimumLength(emailMinLength)
            .MaximumLength(emailMaxLength)
            .EmailAddress()
            .WithMessage("Enter a valid email address.");
    }

    private void ValidateUsername()
    {
        const int usernameMinLength = 4;
        const int usernameMaxLength = 15;

        RuleFor(u => u.Username)
            .NotEmpty()
            .MinimumLength(usernameMinLength)
            .MaximumLength(usernameMaxLength)
            .Must(c => c.All(char.IsLetterOrDigit))
            .WithMessage("Username cannot contain special characters.")
            .Must(c => c.Count(char.IsLetter) > c.Count(char.IsDigit))
            .WithMessage("Username must contain more alphabetical letters than numerical characters.");

    }

    #region DateBirthValidation
    private void ValidateDateBirth()
    {
        RuleFor(u => u.DateBirth)
            .NotEmpty()
            .WithMessage("Enter your date of birth.")
            .Must(BeValidDate)
            .WithMessage("Invalid date of birth.")
            .Must(BeOver13).WithMessage("You must be over 13 years old to use this app.")
            .Must(BeValidDayAndMonth)
            .WithMessage("Date of birth contains invalid day or month.");
    }

    private static bool BeValidDate(DateTime date)
    {
        var isValid = true;
        try
        {
            DateTime d = Convert.ToDateTime(date);
        }
        catch (Exception e)
        {
            isValid = false;
        }

        return isValid;
    }

    private static bool BeOver13(DateTime date)
    {
        return date.AddYears(13) <= DateTime.Today;
    }

    private static bool BeValidDayAndMonth(DateTime date)
    {
        var day = date.Day;
        var month = date.Month;
        var year = date.Year;

        if (day is > 31 or < 1) return false;
        if (month is > 12 or < 1) return false;

        var daysInMonth = DateTime.DaysInMonth(year, month);
        return day <= daysInMonth;
    }
    #endregion

}
