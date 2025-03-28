using BUMessenger.Domain.Models.Models.Users;

namespace BUMessenger.Domain.Interfaces.Services;

public interface IUserService
{
    /// <summary>
    /// Создаёт нового пользователя
    /// </summary>
    /// <param name="userCreate">Пользователь для добавления</param>
    /// <returns>Добавленный пользователь</returns>
    Task<User> AddUserAsync(UserCreate userCreate);
    
    /// <summary>
    /// Получение пользователя по email и паролю
    /// </summary>
    /// <param name="email">Адрес почты</param>
    /// <param name="password">Пароль</param>
    /// <returns>Пользователь</returns>
    Task<User> AuthUserByEmailPasswordAsync(string email, string password);
    
    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Пользователь</returns>
    Task<User> GetUserByIdAsync(Guid id);
}