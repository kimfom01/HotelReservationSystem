using System.Net.Http.Json;
using Hrs.AdminCli;
using Hrs.AdminCli.Utilities;

var validator = new Validator();
var input = new Input(validator);

var firstName = input.GetName();
var lastName = input.GetName("last");
var email = input.GetEmail();
var password = input.GetPassword();
var confirmPassword = input.GetConfirmPassword(password);

var admin = new Admin(
    firstName,
    lastName,
    email,
    password,
    confirmPassword);

var httpClient = new HttpClient();

var response = await httpClient.PostAsJsonAsync("http://localhost:5230/api/user/admin/register", admin);

Console.WriteLine(await response.Content.ReadAsStringAsync());