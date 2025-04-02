namespace BUMessenger.Domain.Models.Models.AuthTokens;

public record AuthToken
{
    /// <summary>
    /// Идентификатор токена
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required Guid UserId { get; init; }
    
    /// <summary>
    /// Значение токена
    /// </summary>
    public required string RefreshToken { get; init; }
    
    /// <summary>
    /// Время когда истекает срок действия токена
    /// </summary>
    public required DateTime ExpiresAtUtc { get; init; }
}