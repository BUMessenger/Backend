namespace BUMessenger.Domain.Models.Models.UnregisteredUsers;

public record UnregisteredUser
{
    /// <summary>
    /// Идентификатор незарегистрированного пользователя
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Почта незарегистрированного пользователя
    /// </summary>
    public required string Email { get; init; }
}