namespace Admin.Application.Contracts.Email;

public class EmailMessage
{
    public string? ReceiverName { get; init; } = string.Empty;
    public string ReceiverEmail { get; init; } = string.Empty;
    public string Subject { get; init; } = string.Empty;
    public string Template { get; init; } = string.Empty;
}