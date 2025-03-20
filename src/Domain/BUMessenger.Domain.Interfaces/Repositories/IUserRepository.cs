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
}