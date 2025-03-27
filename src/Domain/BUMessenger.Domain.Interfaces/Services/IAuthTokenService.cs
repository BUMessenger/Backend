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
}