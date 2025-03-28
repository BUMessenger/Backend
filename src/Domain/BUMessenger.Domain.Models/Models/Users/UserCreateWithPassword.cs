namespace BUMessenger.Domain.Models.Models.Users;

public record UserCreateWithPassword
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Захешированный пароль пользователя
    /// </summary>
    public required string PasswordHashed { get; init; }
}