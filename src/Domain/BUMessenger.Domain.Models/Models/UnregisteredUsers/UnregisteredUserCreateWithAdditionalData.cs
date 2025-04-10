namespace BUMessenger.Domain.Models.Models.UnregisteredUsers;

public record UnregisteredUserCreateWithAdditionalData
{
    /// <summary>
    /// Почта незарегистрированного пользователя
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Пароль незарегистрированного пользователя в захешированном виде
    /// </summary>
    public required string PasswordHashed { get; set; }
    
    /// <summary>
    /// Код подтверждения электронной почты
    /// </summary>
    public required string ApproveCode { get; set; }
    
    /// <summary>
    /// Время когда истекает срок действия кода подтверждения
    /// </summary>
    public required DateTime ExpiresAtUtc { get; set; }
}