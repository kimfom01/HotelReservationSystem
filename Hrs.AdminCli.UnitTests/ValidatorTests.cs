using Hrs.AdminCli.Utilities;

namespace Hrs.AdminCli.UnitTests;

public class ValidatorTests
{
    [Theory]
    [InlineData("John")]
    [InlineData("Doe")]
    [InlineData("Hamza")]
    [InlineData("Henry")]
    [InlineData("Wolfeschlegelsteinhausenbergerdorff")]
    public void WhenValidInputStringReturnTrue(string input)
    {
        var validator = new Validator();

        Assert.True(validator.ValidateString(input));
    }

    [Theory]
    [InlineData("\n")]
    [InlineData("\r\n")]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("                         ")]
    public void WhenInvalidInputStringReturnFalse(string input)
    {
        var validator = new Validator();

        Assert.False(validator.ValidateString(input));
    }

    [Theory]
    [InlineData("email@example.com")]
    [InlineData("firstname.lastname@example.com")]
    [InlineData("email@subdomain.example.com")]
    [InlineData("firstname+lastname@example.com")]
    [InlineData("firstname-lastname@example.com")]
    [InlineData("email@example.co.jp")]
    [InlineData("email@example-one.com")]
    public void WhenValidEmailReturnTrue(string email)
    {
        var validator = new Validator();

        Assert.True(validator.ValidateEmail(email));
    }

    [Theory]
    [InlineData("plainaddress")]
    [InlineData("@example.com")]
    [InlineData("Joe Smith <email@example.com>")]
    [InlineData("email.example.com")]
    [InlineData("email@example.com (Joe Smith)")]
    [InlineData("email@example")]
    [InlineData("email@example@example.com")]
    [InlineData("あいうえお@example.com")]
    public void WhenInvalidEmailReturnFalse(string email)
    {
        var validator = new Validator();

        Assert.False(validator.ValidateEmail(email));
    }

    [Theory]
    [InlineData("12345", "12345")]
    [InlineData("P@$$w0rd", "P@$$w0rd")]
    public void WhenConfirmPasswordMatchesPasswordReturnTrue(string password, string confirmPassword)
    {
        var validator = new Validator();

        Assert.True(validator.ValidateConfirmPassword(password, confirmPassword));
    }

    [Theory]
    [InlineData("12345", "123456")]
    [InlineData("P@$$w0rd", "P@$$w0rd!")]
    public void WhenConfirmPasswordNotMatchPasswordReturnFalse(string password, string confirmPassword)
    {
        var validator = new Validator();

        Assert.False(validator.ValidateConfirmPassword(password, confirmPassword));
    }
}