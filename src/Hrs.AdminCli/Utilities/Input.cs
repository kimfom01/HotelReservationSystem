namespace Hrs.AdminCli.Utilities;

public class Input
{
    private readonly Validator _validator;

    public Input(Validator validator)
    {
        _validator = validator;
    }

    public string GetName(string type = "first")
    {
        Console.Write($"Enter {type} name: ");
        var name = Console.ReadLine();
        while (!_validator.ValidateString(name))
        {
            Console.Write($"Error!\nEnter {type} name: ");
            name = Console.ReadLine();
        }

        return name!;
    }

    public string GetEmail()
    {
        Console.Write("Enter email: ");
        var email = Console.ReadLine();
        while (!_validator.ValidateEmail(email))
        {
            Console.Write("Error!\nEnter email: ");
            email = Console.ReadLine();
        }

        return email!;
    }

    public string GetPassword()
    {
        Console.Write("Enter password: ");
        var password = Console.ReadLine();
        while (!_validator.ValidateString(password))
        {
            Console.Write("Error!\nEnter password: ");
            password = Console.ReadLine();
        }

        return password!;
    }

    public string GetConfirmPassword(string password)
    {
        Console.Write("Enter password again to confirm: ");
        var confirmPassword = Console.ReadLine();
        while (!_validator.ValidateConfirmPassword(password, confirmPassword))
        {
            Console.Write("Error!\npassword again to confirm: ");
            confirmPassword = Console.ReadLine();
        }

        return confirmPassword!;
    }
}