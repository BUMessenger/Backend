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
}