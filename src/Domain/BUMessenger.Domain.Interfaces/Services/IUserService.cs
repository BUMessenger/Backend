using BUMessenger.Domain.Models.Models.Users;

namespace BUMessenger.Domain.Interfaces.Services;

public interface IUserService
{
    /// <summary>
    /// Создаёт нового незарегистрированного пользователя
    /// </summary>
    /// <param name="userCreate">Незарегистрированный пользователь для добавления</param>
    /// <returns>Добавленный пользователь</returns>
    Task<User> AddUserAsync(UserCreate userCreate);
}