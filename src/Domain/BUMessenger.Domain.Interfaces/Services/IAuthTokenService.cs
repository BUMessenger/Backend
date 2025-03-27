using BUMessenger.Domain.Models.Models.AuthTokens;

namespace BUMessenger.Domain.Interfaces.Services;

public interface IAuthTokenService
{
    /// <summary>
    /// Добавление токена
    /// </summary>
    /// <param name="authTokenCreate">Токен</param>
    /// <returns>Созданный токен</returns>
    Task<AuthToken> AddAuthTokenAsync(AuthTokenCreate authTokenCreate);
    
    /// <summary>
    /// Получение сущности токена по его значению
    /// </summary>
    /// <param name="refreshToken">Значение токена</param>
    /// <returns>Токен</returns>
    Task<AuthToken> GetAuthTokenByRefreshTokenAsync(string refreshToken);
    
    /// <summary>
    /// Отзывает токен по его значению
    /// </summary>
    /// <param name="refreshToken">Значение токена</param>
    /// <returns></returns>
    Task RevokeRefreshTokenByRefreshTokenAsync(string refreshToken);
}