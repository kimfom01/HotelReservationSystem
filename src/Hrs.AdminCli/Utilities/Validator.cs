using System.Text.RegularExpressions;

namespace Hrs.AdminCli.Utilities;

public class Validator
{
    public bool ValidateString(string? input)
    {
        return !string.IsNullOrWhiteSpace(input);
    }

    public bool ValidateEmail(string? email)
    {
        var regex = new Regex(@"^[a-zA-Z0-9.+\-_""]+\@[A-Za-z0-9.\-]+\.[A-Za-z0-9.*]+$");

        return ValidateString(email) && regex.IsMatch(email!);
    }

    public bool ValidateConfirmPassword(string password, string? confirmPassword)
    {
        return ValidateString(confirmPassword) && password == confirmPassword;
    }
}