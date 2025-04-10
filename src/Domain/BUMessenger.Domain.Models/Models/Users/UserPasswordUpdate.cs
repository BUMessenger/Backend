namespace BUMessenger.Domain.Models.Models.Users;

public record UserPasswordUpdate
{
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public required string OldPassword { get; init; }
    
    /// <summary>
    /// Новый пароль пользователя
    /// </summary>
    public required string NewPassword { get; init; }
}