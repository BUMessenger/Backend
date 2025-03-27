using BUMessenger.Domain.Models.Models.AuthTokens;

namespace BUMessenger.Domain.Interfaces.Repositories;

public interface IAuthTokenRepository
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
    /// <returns>Токен, если найден, null -- иначе</returns>
    Task<AuthToken?> FindAuthTokenByRefreshTokenAsync(string refreshToken);
    
    /// <summary>
    /// Удаляет из базы токен по его значению
    /// </summary>
    /// <param name="refreshToken">Значение токена</param>
    /// <returns></returns>
    Task DeleteAuthTokenByRefreshTokenAsync(string refreshToken);
}