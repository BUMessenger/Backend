namespace BUMessenger.Domain.Models.Models.UnregisteredUsers;

public record UnregisteredUserCreate
{
    /// <summary>
    /// Почта незарегистрированного пользователя
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Пароль незарегистрированного пользователя
    /// </summary>
    public required string Password { get; init; }
}