using BUMessenger.Domain.Models.Models.Users;

namespace BUMessenger.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Создание нового пользователя
    /// </summary>
    /// <param name="userCreate">Начальные данные о пользователе для создания</param>
    /// <returns>Новый пользователь</returns>
    Task<User> AddUserAsync(UserCreateWithPassword userCreate);
    
    /// <summary>
    /// True -- если пользователь с таким email существует, false -- иначе
    /// </summary>
    /// <param name="email">Email пользователя</param>
    /// <returns>Флаг существования пользователя</returns>
    Task<bool> IsUserExistByEmailAsync(string email);
    
    /// <summary>
    /// Получение пользователя по email и захешированному паролю
    /// </summary>
    /// <param name="email">Email</param>
    /// <param name="passwordHashed">Захешированный пароль</param>
    /// <returns>Пользователь, если найден, null -- иначе</returns>
    Task<User?> FindUserByEmailPasswordHashedAsync(string email, string passwordHashed);
    
    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Пользователь, если найден, null -- иначе</returns>
    Task<User?> FindUserByIdAsync(Guid id);
}