namespace BUMessenger.Domain.Models.Models.UnregisteredUsers;

public record UnregisteredUserForAddUser
{
    /// <summary>
    /// Идентификатор незарегистрированного пользователя
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Почта незарегистрированного пользователя
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Пароль незарегистрированного пользователя в захешированном виде
    /// </summary>
    public required string PasswordHashed { get; init; }
    
    /// <summary>
    /// Код подтверждения электронной почты
    /// </summary>
    public required string ApproveCode { get; init; }
    
    /// <summary>
    /// Время когда истекает срок действия кода подтверждения
    /// </summary>
    public required DateTime ExpiresAtUtc { get; init; }
}