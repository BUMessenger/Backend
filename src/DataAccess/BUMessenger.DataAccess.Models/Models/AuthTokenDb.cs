namespace BUMessenger.DataAccess.Models.Models;

public class AuthTokenDb
{
    /// <summary>
    /// Идентификатор авторизационного токена
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Значение токена
    /// </summary>
    public string RefreshToken { get; set; }
    
    /// <summary>
    /// Время когда истекает срок действия токена
    /// </summary>
    public DateTime ExpiresAtUtc { get; set; }
    
    /// <summary>
    /// Навигационное свойство для связи с таблицей пользователей
    /// </summary>
    public UserDb User { get; set; }

    public AuthTokenDb(Guid id, 
        Guid userId, 
        string refreshToken, 
        DateTime expiresAtUtc)
    {
        Id = id;
        UserId = userId;
        RefreshToken = refreshToken;
        ExpiresAtUtc = expiresAtUtc;
    }
}