namespace BUMessenger.Domain.Models.Models.Users;

public record UserPasswordRecovery
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    public required string Email { get; init; }
}